using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace CheckPoint2Text
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Text.txt";
            string str = "";

            if (File.Exists(path))
            {
                str = File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine("Error file not found");
            }

            StringBuilder f = new StringBuilder(str, 1000);
            Console.WriteLine("{0}\n\n", f);
            Parser parser = new Parser();
            Text text = parser.Parse(f);
            
            Console.WriteLine("----------GetListOfSentByNumOfWord------------\n\n");

            foreach (var item in text.GetListOfSentByNumOfWord())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------GetListWordsInSent-------------\n\n");

            foreach (var item in text.GetListWordsInSent(2, TypeSent.Voprositelnoe))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------DelWordsWithConsonants-------------\n\n");

            Console.WriteLine(text);
            text.DelWordsWithConsonants(3);
            Console.WriteLine("\n\n");
            Console.WriteLine(text);

            Console.WriteLine("----------ReplacementWordsInSent------------\n\n");

            text.ReplacementWordsInSent(1, 3, "XAXAXA");
            Console.WriteLine(text);

            Console.WriteLine("----------GetConcordance------------\n\n");
            string nameFileForWrite=@"C:\Concordance.txt";
            Concordance(text, nameFileForWrite);

            Console.ReadLine();
        }

        private static void Concordance(Text text, string nameFileForWrite)
        {
            StringBuilder concordance = new StringBuilder(1000);
            var listOfKeys = text.GetConcordance().Keys.ToList();
            int i = 1;

            foreach (var item in text.GetConcordance())
            {
                if (i == 1)
                {
                    concordance.Append("      ");
                    concordance.Append(item.Key[0].ToString().ToUpper());
                    concordance.Append("\n\r");
                }

                concordance.Append(item.Key);
                concordance.Append("........");
                concordance.Append(item.Value.Count);
                concordance.Append(": ");

                foreach (var item2 in item.Value.Distinct())
                {
                    concordance.Append(item2);
                    concordance.Append(" ");
                }

                concordance.Append("\n\r");

                if (i < listOfKeys.Count)
                {
                    if (listOfKeys[i][0] != item.Key[0])
                    {
                        concordance.Append("      ");
                        concordance.Append(listOfKeys[i][0].ToString().ToUpper());
                        concordance.Append("\n\r");
                    }
                }
                i++;
            }

            string path2 = nameFileForWrite;
            File.WriteAllText(path2, concordance.ToString());
            Console.WriteLine(concordance);
        }
    }
}
