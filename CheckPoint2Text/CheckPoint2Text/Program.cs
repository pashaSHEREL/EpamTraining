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
            Parser parser = new Parser();
            Text text = parser.Parse(f);
            TextHandler textHandler = new TextHandler(text);
            Console.WriteLine("{0}\n\n", f);

            Console.WriteLine("----------GetListOfSentByNumOfWord------------\n\n");

            foreach (var item in textHandler.GetListOfSentByNumOfWord())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------GetListWordsInSent-------------\n\n");

            foreach (var item in textHandler.GetListWordsInSent(2, "?"))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------DelWordsWithConsonants-------------\n\n");

            Console.WriteLine(text);
            textHandler.DelWordsWithConsonants(3);
            Console.WriteLine("\n\n");
            Console.WriteLine(text);

            Console.WriteLine("----------ReplacementWordsInSent------------\n\n");

            textHandler.ReplacementWordsInSent(1, 3, "XAXAXA");
            Console.WriteLine(text);

            Console.WriteLine("----------GetConcordance------------\n\n");

            Concordance(textHandler);

            Console.ReadLine();
        }

        private static void Concordance(TextHandler textHandle)
        {
            StringBuilder concordance = new StringBuilder(1000);
            var listOfKeys = textHandle.GetConcordance().Keys.ToList();
            int i = 1;

            foreach (var item in textHandle.GetConcordance())
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

            string path2 = @"C:\Concordance.txt";
            File.WriteAllText(path2, concordance.ToString());
            Console.WriteLine(concordance);
        }
    }
}
