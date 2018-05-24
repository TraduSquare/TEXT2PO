using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Yarhl.IO;
using Yarhl.Media.Text;
using Yarhl.FileFormat;

namespace TEXT2PO
{
    class TEXT2PO
    {
        static bool exportrune;
        static bool exportlord;
        static bool importrune;
        static bool importlord;
        static void Main(string[] args)
        {
            {
                Console.WriteLine("TEXT2PO V1 - A simple converter for the text from the games Lord of Magna and Rune Factory 4 by Darkmet98.");
                Console.WriteLine("Massive thanks to Pleonex, Leeg and Megaflan for all.");
                if (args.Length < 2)
                {
                    System.Console.WriteLine("Usage: TEXT2PO <file> <po file> <game (-exportrune, -exportlord, importrune or importlord)>");
                    System.Console.WriteLine("Example: TEXT2PO.exe msg.nxtxt -exportlord");
                    System.Console.WriteLine("Example: TEXT2PO.exe msg.nxtxt msg.nxtxt.po -importlord");
                    return;
                }
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-exportrune") exportrune = true;
                    if (args[i] == "-exportlord") exportlord = true;
                    if (args[i] == "-importrune") importrune = true;
                    if (args[i] == "-importlord") importlord = true;
                }
                List<int> size2 = new List<int>();
                List<int> text = new List<int>();
                using (BinaryReader reader = new BinaryReader(File.Open(args[0], FileMode.Open)))
                    {
                    if (exportlord == true)
                        {
                            Po po = new Po
                             {
                                    Header = new PoHeader("Lord Of Magna Maiden Heaven", "glowtranslations <glowtranslations@gmail.com>")
                                {
                                    Language = "es-ES",
                                    LanguageTeam = "Glowtranslations",
                                }
                            };
                        string result = "";
                        Int32 magic = reader.ReadInt32();
                        Int32 count = reader.ReadInt32();
                        Console.WriteLine("Exporting...");
                        for (int i = 0; i < count; i++)
                        {
                            Int32 size = reader.ReadInt32();
                            Int32 position = reader.ReadInt32();
                            size2.Add(size);
                            text.Add(position);
                        }
                        for (int i = 0; i < count; i++)
                        {
                            reader.BaseStream.Position = text[i]; //El puto flan
                            byte[] array = reader.ReadBytes(size2[i]);
                            result = Encoding.Unicode.GetString(array);
                            if (string.IsNullOrEmpty(result))
                                result = "<!empty>";
                            po.Add(new PoEntry(result) { Context = i.ToString() });
                        }
                        po.ConvertTo<BinaryFormat>().Stream.WriteTo(args[0] + ".po");
                        Console.WriteLine("The file is exported.");
                    }
                    if (exportrune == true)
                    {
                        Po po = new Po
                        {
                            Header = new PoHeader("Rune Factory 4", "glowtranslations <glowtranslations@gmail.com>")
                            {
                                Language = "es-ES",
                                LanguageTeam = "Glowtranslations",
                            }
                        };
                        string result = "";
                        Int32 magic = reader.ReadInt32();
                        Int32 count = reader.ReadInt32();
                        Console.WriteLine("Exporting...");
                        for (int i = 0; i < count; i++)
                        {
                            Int32 size = reader.ReadInt32();
                            Int32 position = reader.ReadInt32();
                            size2.Add(size);
                            text.Add(position);
                        }
                        for (int i = 0; i < count; i++)
                        {
                            reader.BaseStream.Position = text[i]; //El puto flan
                            byte[] array = reader.ReadBytes(size2[i]);
                            result = Encoding.UTF8.GetString(array);
                            if (string.IsNullOrEmpty(result))
                                result = "<!empty>";
                            po.Add(new PoEntry(result) { Context = i.ToString() });
                        }
                        po.ConvertTo<BinaryFormat>().Stream.WriteTo(args[0] + ".po");
                        Console.WriteLine("The file is exported.");
                    }
                    if (importrune == true)
                    {
                        Console.WriteLine("Importing...");
                        DataStream input = new DataStream(args[1], FileOpenMode.Read);
                        BinaryFormat binary = new BinaryFormat(input);
                        Po po = binary.ConvertTo<Po>();
                        input.Dispose();
                        List<int> size = new List<int>();
                        List<int> position = new List<int>();
                        var stream = reader.BaseStream;

                        using (BinaryWriter writer = new BinaryWriter(File.Open(args[0] + ".exported", FileMode.Create)))
                        {
                            writer.Write(0x54584554);
                            writer.Write(po.Entries.Count);
                            for (int i = 0; i < po.Entries.Count * 2; i++)
                            {
                                writer.Write(0x00000000);
                            }
                            foreach (var entry in po.Entries)
                            {
                                string potext = string.IsNullOrEmpty(entry.Translated) ?
                                    entry.Original : entry.Translated;
                                if (potext == "<!empty>")
                                    potext = string.Empty;
                                position.Add((int)writer.BaseStream.Position);
                                byte[] stringtext = Encoding.UTF8.GetBytes(potext += "\0");
                                writer.Write(stringtext);
                                int textsize = stringtext.Length;
                                size.Add(textsize);
                                

                            }
                            writer.BaseStream.Position = 0x8;
                            int countposition = 0x8 * po.Entries.Count + 1;
                            for (int i = 0; i < po.Entries.Count; i++)
                            {
                                writer.Write(size[i] - 1);
                                writer.Write(position[i]);
                            }
                            Console.WriteLine("The file is imported.");
                        }

                    }
                    if (importlord == true)
                    {
                        Console.WriteLine("Importing...");
                        DataStream input = new DataStream(args[1], FileOpenMode.Read);
                        BinaryFormat binary = new BinaryFormat(input);
                        Po po = binary.ConvertTo<Po>();
                        input.Dispose();
                        List<int> size = new List<int>();
                        List<int> position = new List<int>();
                        var stream = reader.BaseStream;

                        using (BinaryWriter writer = new BinaryWriter(File.Open(args[0] + ".exported", FileMode.Create)))
                        {
                            writer.Write(0x54584554);
                            writer.Write(po.Entries.Count);
                            for (int i = 0; i < po.Entries.Count * 3; i++)
                            {
                                writer.Write(0x00000000);
                            }
                            foreach (var entry in po.Entries)
                            {
                                string potext = string.IsNullOrEmpty(entry.Translated) ?
                                    entry.Original : entry.Translated;
                                if (potext == "<!empty>")
                                    potext = string.Empty;
                                position.Add((int)writer.BaseStream.Position);
                                byte[] stringtext = Encoding.Unicode.GetBytes(potext += "\0");
                                writer.Write(stringtext);
                                int textsize = stringtext.Length;
                                size.Add(textsize);


                            }
                            writer.BaseStream.Position = 0x8;
                            int countposition = 0x8 * po.Entries.Count + 1;
                            for (int i = 0; i < po.Entries.Count; i++)
                            {
                                writer.Write(size[i] - 1);
                                writer.Write(position[i]);
                            }
                            Console.WriteLine("The file is imported.");
                        }
                    }
                }
            }
        }
    }
}
