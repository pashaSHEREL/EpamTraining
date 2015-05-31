using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class TextHandler
    {
        private Text text = new Text();

        public TextHandler()
        {
        }

        public TextHandler(Text text)
        {
            this.text.Sentences = text.Sentences;
        }

        public List<Sentence> GetListOfSentByNumOfWord()
        {
            List<Sentence> sent = new List<Sentence>();
            return sent = text.Sentences.OrderBy(x => x.NumberOfWords).Select(x => x).ToList();
        }

        public List<ITextElement> GetListWordsInSent(int numOfLetter, string endOfSent)
        {
            var sent = text.Sentences.Where(x => x.ElementsOfText.Last().Value == endOfSent).Select(x => x);
            IEnumerable<ITextElement> buffOfSymbols = new List<ITextElement>();

            foreach (var item in sent)
            {
                buffOfSymbols = buffOfSymbols.Concat(item.ElementsOfText
                       .Where(x => x is Word && x.Lenght == numOfLetter)
                       .Select(x => x));
            }

            return buffOfSymbols.Distinct().ToList();
        }


        public void DelWordsWithConsonants(int numOfLetter)
        {
            foreach (var item in text.Sentences)
            {
                for (int i = 0; i < item.ElementsOfText.Count; i++)
                {
                    if (item.ElementsOfText[i] is Word && item.ElementsOfText[i].Lenght == numOfLetter 
                        && SymbolHelp.Consonants.Contains(item.ElementsOfText[i].Value[0].ToString()))
                    {
                        item.ElementsOfText.Remove(item.ElementsOfText[i]);
                    }
                }
            }
            //IEnumerable<ITextElement> buffOfTextElements = new List<ITextElement>();
            /*foreach (var item in text.Sentences)
            {
                buffOfTextElements = buffOfTextElements.Concat(item.ElementsOfText.Where(x => (x is Word && x.Lenght == numOfLetter
                    && SymbolHelp.consonants.Contains(x.Value.First().ToString().ToLower()))).Select(x => x)).ToList();
            }
           
            foreach (var item in text.Sentences)
            {
                item.ElementsOfText = item.ElementsOfText.Except(buffOfTextElements.Distinct()).ToList();
            }*/
        }

        public void ReplacementWordsInSent(int numOfSent, int lenghtWord, string str)
        {
            if (numOfSent > text.Sentences.Count)
            {
                numOfSent = text.Sentences.Count;
            }

            if (numOfSent > 0)
            {
                foreach (var item in text.Sentences[numOfSent - 1].ElementsOfText)
                {
                    if (item.Lenght == lenghtWord && item is Word)
                    {
                        item.Chars.Clear();

                        for (int i = 0; i < str.Length; i++)
                        {
                            item.Chars.Add(new Symbol() { Value = str[i] });
                        }
                    }
                }
            }
        }

        public Dictionary<string, List<int>> GetConcordance()
        {
            IEnumerable<Word> buffWords = new List<Word>();

            foreach (var item in text.Sentences)
            {
                buffWords = buffWords.Concat(item.ElementsOfText.Where(x => x is Word).Select(x => x as Word));
            }

            List<Word> buffOrderedWords = buffWords.OrderBy(x => x.Value).ToList();
            List<int> listOfPages = new List<int>();
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();

            for (int i = 0; i < buffOrderedWords.Count - 1; i++)
            {
                if (buffOrderedWords[i].Value.ToLower() == buffOrderedWords[i + 1].Value.ToLower())
                {
                    listOfPages.Add((buffOrderedWords[i]).NumberOfPage);

                    if (i + 1 == buffOrderedWords.Count - 1)
                    {
                        listOfPages.Add((buffOrderedWords[i + 1]).NumberOfPage);
                        dict.Add((buffOrderedWords[i + 1]).Value.ToLower(), listOfPages.ToList());
                    }
                }
                else
                {
                    listOfPages.Add((buffOrderedWords[i]).NumberOfPage);
                    dict.Add((buffOrderedWords[i]).Value.ToLower(), listOfPages.ToList());
                    listOfPages.Clear();

                    if (i + 1 == buffOrderedWords.Count - 1)
                    {
                        listOfPages.Add((buffOrderedWords[i + 1]).NumberOfPage);
                        dict.Add((buffOrderedWords[i + 1]).Value.ToLower(), listOfPages.ToList());
                    }
                }
            }
            return dict;
        }
    }
}
