using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Yarhl.Media.Text;
using Yarhl.FileFormat;

namespace TEXT2PO
{
    class TEXT2PO
    {
        static bool rune;
        static bool lord;
        static void Main(string[] args)
        {
            {
                Console.WriteLine("TEXT2PO - A simple converter for the text from the games Lord of Magna and Rune Factory 4 by Darkmet98.");
                Console.WriteLine("Massive thanks to Pleonex, Leeg and Megaflan for all.");
                if (args.Length < 2)
                {
                    System.Console.WriteLine("Usage: TEXT2PO <file> <game (-rune or -lord)>");
                    System.Console.WriteLine("Example: TEXT2PO.exe msg.nxtxt -lord");
                    return;
                }
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-rune") rune = true;
                    if (args[i] == "-lord") lord = true;
                }
                using (BinaryReader reader = new BinaryReader(File.Open(args[0], FileMode.Open)))
                    {
                    if (lord == true)
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
                        Int32 size = reader.ReadInt32();
                        Int32 position = reader.ReadInt32();
                        List<int> size2 = new List<int>();
                        List<int> text = new List<int>();
                        for (int i = 0; i < count - 1; i++)
                        {
                            size = reader.ReadInt32();
                            position = reader.ReadInt32();
                            size2.Add(size);
                            text.Add(position);
                        }
                        for (int i = 0; i < count - 1; i++)
                        {
                            reader.BaseStream.Position = text[i]; //El puto flan
                            byte[] array = reader.ReadBytes(size2[i]);
                            result = Encoding.Unicode.GetString(array);
                            po.Add(new PoEntry(result) { Context = i.ToString() });
                        }
                        po.ConvertTo<BinaryFormat>().Stream.WriteTo(args[0] + ".po");
                        Console.WriteLine("The file is converted.");
                    }
                    if (rune == true)
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
                        Int32 size = reader.ReadInt32();
                        Int32 position = reader.ReadInt32();
                        List<int> size2 = new List<int>();
                        List<int> text = new List<int>();
                        for (int i = 0; i < count - 1; i++)
                        {
                            size = reader.ReadInt32();
                            position = reader.ReadInt32();
                            size2.Add(size);
                            text.Add(position);
                        }
                        for (int i = 0; i < count - 1; i++)
                        {
                            reader.BaseStream.Position = text[i]; //El puto flan
                            byte[] array = reader.ReadBytes(size2[i]);
                            result = Encoding.UTF8.GetString(array);
                            po.Add(new PoEntry(result) { Context = i.ToString() });
                        }
                        po.ConvertTo<BinaryFormat>().Stream.WriteTo(args[0] + ".po");
                        Console.WriteLine("The file is converted.");
                    }
                }
            }
        }
    }
}
