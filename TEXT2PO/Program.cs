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
        static void Main(string[] args)
        {
            {
                Console.WriteLine("TEXT2PO 1.2 - A simple converter for the text from the games Lord of Magna and Rune Factory 4 by Darkmet98.");
                Console.WriteLine("Massive thanks to Pleonex, Leeg and Megaflan for all.");
                if (args.Length != 3 && args.Length != 2)
                {
                    System.Console.WriteLine("Usage: TEXT2PO <mode> <file1> <file2>");
                    System.Console.WriteLine("Mode for Rune Factory 4: -exportrune (export to po)/-importrune (import po)/-transrune(import the translation from another file)/-exportrunefix(export and fix bad newlines from another programs)");
                    System.Console.WriteLine("Mode for Lord Of Magna Maiden Heaven: -exportlord (export to po)/-importlord (import po)/-translord(import the translation from another file)/-exportlordfix(export and fix bad newlines from another programs)");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Example 1: TEXT2PO.exe -exportlord msg.nxtxt");
                    System.Console.WriteLine("Example 2: TEXT2PO.exe -importlord msg.nxtxt.po");
                    System.Console.WriteLine("Example 3: TEXT2PO.exe -translord msg.nxtxt.po msgESP.nxtxt");
                    System.Console.WriteLine("Example 4: TEXT2PO.exe -exportlordfix msgESP.nxtxt");
                    return;
                }
                List<String> Filestrings = new List<String>();
                List<String> Postrings = new List<String>();
                List<int> stringsizes = new List<int>();
                List<int> stringpositions = new List<int>();
                string result = "";
                Int32 magic;
                Int32 count;
                Int32 size;
                Int32 position;
                Int32 textsize;
                int i = 0;
                switch (args[0])
                {
                    case "-exportlord":
                        using (BinaryReader reader = new BinaryReader(File.Open(args[1], FileMode.Open)))
                        {
                            Po poexport = new Po
                            {
                                Header = new PoHeader("Lord Of Magna Maiden Heaven", "glowtranslations@gmail.com", "es")
                                {
                                    LanguageTeam = "Glowtranslations",
                                }
                            };
                            magic = reader.ReadInt32();
                            count = reader.ReadInt32();
                            Console.WriteLine("Exporting...");
                            for (i = 0; i < count; i++)
                            {
                                size = reader.ReadInt32();
                                position = reader.ReadInt32();
                                stringsizes.Add(size);
                                stringpositions.Add(position);
                            }
                            for (i = 0; i < count; i++)
                            {
                                reader.BaseStream.Position = stringpositions[i]; //El puto flan
                                byte[] array = reader.ReadBytes(stringsizes[i]);
                                result = Encoding.Unicode.GetString(array);
                                if (string.IsNullOrEmpty(result))
                                    result = "<!empty>";
                                poexport.Add(new PoEntry(result) { Context = i.ToString() });
                            }
                            poexport.ConvertTo<BinaryFormat>().Stream.WriteTo(args[1] + ".po");
                            Console.WriteLine("The file is exported.");
                        }
                        break;
                    case "-exportlordfix":
                        using (BinaryReader reader = new BinaryReader(File.Open(args[1], FileMode.Open)))
                        {
                            Po poexport = new Po
                            {
                                Header = new PoHeader("Lord Of Magna Maiden Heaven", "glowtranslations@gmail.com", "es")
                                {
                                    LanguageTeam = "Glowtranslations",
                                }
                            };
                            magic = reader.ReadInt32();
                            count = reader.ReadInt32();
                            Console.WriteLine("Exporting...");
                            for (i = 0; i < count; i++)
                            {
                                size = reader.ReadInt32();
                                position = reader.ReadInt32();
                                stringsizes.Add(size);
                                stringpositions.Add(position);
                            }
                            for (i = 0; i < count; i++)
                            {
                                reader.BaseStream.Position = stringpositions[i]; //El puto flan
                                byte[] array = reader.ReadBytes(stringsizes[i]);
                                result = Encoding.Unicode.GetString(array);
                                result = result.Replace("\r", "");
                                if (string.IsNullOrEmpty(result))
                                    result = "<!empty>";
                                poexport.Add(new PoEntry(result) { Context = i.ToString() });
                            }
                            poexport.ConvertTo<BinaryFormat>().Stream.WriteTo(args[1] + ".po");
                            Console.WriteLine("The file is exported.");
                        }
                        break;
                    case "-exportrune":
                        using (BinaryReader reader = new BinaryReader(File.Open(args[1], FileMode.Open)))
                        {
                            Po poexport = new Po
                            {
                                Header = new PoHeader("Rune Factory 4", "glowtranslations@gmail.com", "es")
                                {
                                    LanguageTeam = "Glowtranslations",
                                }
                            };
                            magic = reader.ReadInt32();
                            count = reader.ReadInt32();
                            Console.WriteLine("Exporting...");
                            for (i = 0; i < count; i++)
                            {
                                size = reader.ReadInt32();
                                position = reader.ReadInt32();
                                stringsizes.Add(size);
                                stringpositions.Add(position);
                            }
                            for (i = 0; i < count; i++)
                            {
                                reader.BaseStream.Position = stringpositions[i]; //El puto flan
                                byte[] array = reader.ReadBytes(stringsizes[i]);
                                result = Encoding.UTF8.GetString(array);
                                if (string.IsNullOrEmpty(result))
                                    result = "<!empty>";
                                poexport.Add(new PoEntry(result) { Context = i.ToString() });
                            }
                            poexport.ConvertTo<BinaryFormat>().Stream.WriteTo(args[1] + ".po");
                            Console.WriteLine("The file is exported.");
                        }
                        break;
                    case "-exportrunefix":
                        using (BinaryReader reader = new BinaryReader(File.Open(args[1], FileMode.Open)))
                        {
                            Po poexport = new Po
                            {
                                Header = new PoHeader("Rune Factory 4", "glowtranslations@gmail.com", "es")
                                {
                                    LanguageTeam = "Glowtranslations",
                                }
                            };
                            magic = reader.ReadInt32();
                            count = reader.ReadInt32();
                            Console.WriteLine("Exporting...");
                            for (i = 0; i < count; i++)
                            {
                                size = reader.ReadInt32();
                                position = reader.ReadInt32();
                                stringsizes.Add(size);
                                stringpositions.Add(position);
                            }
                            for (i = 0; i < count; i++)
                            {
                                reader.BaseStream.Position = stringpositions[i]; //El puto flan
                                byte[] array = reader.ReadBytes(stringsizes[i]);
                                result = Encoding.UTF8.GetString(array);
                                result = result.Replace("\r", "");
                                if (string.IsNullOrEmpty(result))
                                    result = "<!empty>";
                                poexport.Add(new PoEntry(result) { Context = i.ToString() });
                            }
                            poexport.ConvertTo<BinaryFormat>().Stream.WriteTo(args[1] + ".po");
                            Console.WriteLine("The file is exported.");
                        }
                        break;
                    case "-importlord":
                        DataStream inputLord = new DataStream(args[1], FileOpenMode.Read);
                        BinaryFormat binaryLord = new BinaryFormat(inputLord);
                        Po poLord = binaryLord.ConvertTo<Po>();
                        inputLord.Dispose();
                        Console.WriteLine("Importing...");
                        using (BinaryWriter writer = new BinaryWriter(File.Open(args[1] + ".exported", FileMode.Create)))
                        {
                            writer.Write(0x54584554);
                            writer.Write(poLord.Entries.Count);
                            for (i = 0; i < poLord.Entries.Count * 2; i++)
                            {
                                writer.Write(0x00000000);
                            }
                            foreach (var entry in poLord.Entries)
                            {
                                string potext = string.IsNullOrEmpty(entry.Translated) ?
                                    entry.Original : entry.Translated;
                                if (potext == "<!empty>")
                                    potext = string.Empty;
                                stringpositions.Add((int)writer.BaseStream.Position);
                                byte[] stringtext = Encoding.Unicode.GetBytes(potext += "\0");
                                textsize = stringtext.Length;
                                stringsizes.Add(textsize);
                                writer.Write(stringtext);
                            }
                            writer.BaseStream.Position = 0x8;
                            int countposition = 0x8 * poLord.Entries.Count + 1;
                            for (i = 0; i < poLord.Entries.Count; i++)
                            {
                                writer.Write(stringsizes[i] - 2);
                                writer.Write(stringpositions[i]);
                            }
                            Console.WriteLine("The file is imported.");
                        }
                        break;
                    case "-importrune":
                        Console.WriteLine("Importing...");
                        DataStream input = new DataStream(args[1], FileOpenMode.Read);
                        BinaryFormat binary = new BinaryFormat(input);
                        Po po = binary.ConvertTo<Po>();
                        input.Dispose();
                        using (BinaryWriter writer = new BinaryWriter(File.Open(args[1] + ".exported", FileMode.Create)))
                        {
                            writer.Write(0x54584554);
                            writer.Write(po.Entries.Count);
                            for (i = 0; i < po.Entries.Count * 2; i++)
                            {
                                writer.Write(0x00000000);
                            }
                            foreach (var entry in po.Entries)
                            {
                                string potext = string.IsNullOrEmpty(entry.Translated) ?
                                    entry.Original : entry.Translated;
                                if (potext == "<!empty>")
                                    potext = string.Empty;
                                stringpositions.Add((int)writer.BaseStream.Position);
                                byte[] stringtext = Encoding.UTF8.GetBytes(potext += "\0");
                                textsize = stringtext.Length;
                                stringsizes.Add(textsize);
                                writer.Write(stringtext);
                            }
                            writer.BaseStream.Position = 0x8;
                            int countposition = 0x8 * po.Entries.Count + 1;
                            for (i = 0; i < po.Entries.Count; i++)
                            {
                                writer.Write(stringsizes[i] - 1);
                                writer.Write(stringpositions[i]);
                            }
                            Console.WriteLine("The file is imported.");
                        }
                        break;
                    case "-transrune":
                        using (BinaryReader reader = new BinaryReader(File.Open(args[2], FileMode.Open)))
                        {
                            Console.WriteLine("Importing old translation...");
                            magic = reader.ReadInt32();
                            count = reader.ReadInt32();
                            for (i = 0; i < count; i++)
                            {
                                size = reader.ReadInt32();
                                position = reader.ReadInt32();
                                stringsizes.Add(size);
                                stringpositions.Add(position);
                            }
                            for (i = 0; i < count; i++)
                            {
                                reader.BaseStream.Position = stringpositions[i]; //El puto flan
                                byte[] array = reader.ReadBytes(stringsizes[i]);
                                result = Encoding.UTF8.GetString(array);
                                Filestrings.Add(result);
                            }
                            
                            }
                        Console.WriteLine("Old translation preloaded.");
                        DataStream inputimp = new DataStream(args[1], FileOpenMode.Read);
                        BinaryFormat binaryimp = new BinaryFormat(inputimp);
                        Po poimp = binaryimp.ConvertTo<Po>();
                        inputimp.Dispose();
                        Console.WriteLine("Importing original translation...");
                        foreach (var entry in poimp.Entries)
                        {
                            Postrings.Add(entry.Original);
                        }
                        Console.WriteLine("Original text preloaded.");
                        Po poexport1 = new Po
                        {
                            Header = new PoHeader("Rune Factory 4", "glowtranslations@gmail.com", "es")
                            {
                                LanguageTeam = "Glowtranslations",
                            }
                        };
                        for (i = 0; i < count; i++)
                        {
                            PoEntry entry = new PoEntry();
                            Console.WriteLine("Checking and comparing line " + i + " from " + count + " lines");
                            if (string.IsNullOrEmpty(Filestrings[i]))
                                Filestrings[i] = "<!empty>";
                            if (Filestrings[i] == Postrings[i])
                            {
                                entry.Context = i.ToString();
                                entry.Original = Postrings[i];
                            }
                            else
                            {
                                entry.Context = i.ToString();
                                entry.Translated = Filestrings[i];
                                entry.Original = Postrings[i];
                            }
                            poexport1.Add(entry);
                            //poexport1.ConvertTo<BinaryFormat>().Stream.WriteTo(args[1] + "(exported)" + ".po"); //Pasta code
                        }
                        using (var poStream = poexport1.ConvertTo<BinaryFormat>())
                            poStream.Stream.WriteTo(args[1] + "(exported).po"); //Thanks pleonex
                        Console.WriteLine("Finished.");
                        break;
                    case "-translord":
                        using (BinaryReader reader = new BinaryReader(File.Open(args[2], FileMode.Open)))
                        {
                            Console.WriteLine("Importing old translation...");
                            magic = reader.ReadInt32();
                            count = reader.ReadInt32();
                            for (i = 0; i < count; i++)
                            {
                                size = reader.ReadInt32();
                                position = reader.ReadInt32();
                                stringsizes.Add(size);
                                stringpositions.Add(position);
                            }
                            for (i = 0; i < count; i++)
                            {
                                reader.BaseStream.Position = stringpositions[i]; //El puto flan
                                byte[] array = reader.ReadBytes(stringsizes[i]);
                                result = Encoding.Unicode.GetString(array);
                                Filestrings.Add(result);
                            }

                        }
                        Console.WriteLine("Old translation preloaded.");
                        DataStream inputimp1 = new DataStream(args[1], FileOpenMode.Read);
                        BinaryFormat binaryimp1 = new BinaryFormat(inputimp1);
                        Po poimp1 = binaryimp1.ConvertTo<Po>();
                        inputimp1.Dispose();
                        Console.WriteLine("Importing original translation...");
                        foreach (var entry in poimp1.Entries)
                        {
                            Postrings.Add(entry.Original);
                        }
                        Console.WriteLine("Original text preloaded.");
                        Po poexport2 = new Po
                        {
                            Header = new PoHeader("Rune Factory 4", "glowtranslations@gmail.com", "es")
                            {
                                LanguageTeam = "Glowtranslations",
                            }
                        };
                        for (i = 0; i < count; i++)
                        {
                            PoEntry entrada = new PoEntry();
                            Console.WriteLine("Checking and comparing line " + i + " from " + count + " lines");
                            if (string.IsNullOrEmpty(Filestrings[i]))
                                Filestrings[i] = "<!empty>";
                            if (Filestrings[i] == Postrings[i])
                            {
                                entrada.Context = i.ToString();
                                entrada.Original = Postrings[i];
                            }
                            else
                            {
                                entrada.Context = i.ToString();
                                entrada.Translated = Filestrings[i];
                                entrada.Original = Postrings[i];
                            }
                            poexport2.Add(entrada);
                        }
                        //poexport2.ConvertTo<BinaryFormat>().Stream.WriteTo(args[1] + "(exported)" + ".po"); //Pasta code
                        using (var poStream = poexport2.ConvertTo<BinaryFormat>())
                            poStream.Stream.WriteTo(args[1] + "(exported).po"); //Thanks pleonex
                        Console.WriteLine("Finished.");
                        break;
                }
            }
        }
    }
}
