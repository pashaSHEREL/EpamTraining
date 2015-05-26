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
            
            
            StringBuilder f = new StringBuilder(str);
         
            Text g = new Text(f);

            foreach (var item in g.Sentences)
            {
               Console.WriteLine(item);
            }

            Console.WriteLine("---------------------\n\n");

            StringBuilder concordance=new StringBuilder();

            var listOfKeys= g.GetConcordance().Keys.ToList();

            int i = 1;

            foreach (var item in g.GetConcordance())
            {
                
                if (i == 1)
                {
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
                        concordance.Append(listOfKeys[i][0].ToString().ToUpper());
                        concordance.Append("\n\r");
                    }
                }
                i++;
            }

            string path2 = @"C:\Concordance.txt";

            File.WriteAllText(path2,concordance.ToString());

            Console.WriteLine(concordance);

            Console.WriteLine("----------------------\n\n");

            foreach (var item in g.GetListOFSentByNumOfWord())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------------------\n\n");

            foreach (var item in g.GetListWordsInSent(2,new PunctuationMark("?")))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------------------\n\n");

            Console.WriteLine(g);

            g.DelWordsWithConsonants(3);

            Console.WriteLine("----------------------\n\n");

            Console.WriteLine(g);

            g.ReplacementWordsInSent( 15, 4, "XAXAXA");

            Console.WriteLine("----------------------\n\n");

            Console.WriteLine(g);

            Console.ReadLine();
        }
    }
}
