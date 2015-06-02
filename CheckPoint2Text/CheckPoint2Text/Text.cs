using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint2Text
{
    public class Text
    { 
        List<Sentence> sentences = new List<Sentence>();

        public Text()
        { 
        }

        public Text(List<Sentence> sentences)
        {
            this.sentences = sentences;
        }
        
        public ReadOnlyCollection<Sentence> Sentences
        {
            get
            { 
                return new ReadOnlyCollection<Sentence>(sentences); 
            }
        }

        public List<Sentence> GetListOfSentByNumOfWord()
        {
            List<Sentence> sent = new List<Sentence>();

            return sent = sentences.OrderBy(x => x.NumberOfWords).Select(x => x).ToList();
        }

        public List<ITextElement> GetListWordsInSent(int numOfLetter, TypeSent type)
        {
            IEnumerable<ITextElement> buffOfSymbols = new List<ITextElement>();

            foreach (var item in sentences)
            {
                if (item.TypeOfSentence == type)
                {
                    foreach (var item2 in item.ElementsOfText)
                    {
                        buffOfSymbols = buffOfSymbols.Concat(item.ElementsOfText
                               .Where(x => x is Word && x.Lenght == numOfLetter)
                               .Select(x => x));
                    }
                }

            }

            return buffOfSymbols.Distinct().ToList();
        }


        public void DelWordsWithConsonants(int numOfLetter)
        {
            foreach (var item in sentences)
            {
                for (int i = 0; i < item.ElementsOfText.Count; i++)
                {
                    if (item.ElementsOfText[i] is Word && item.ElementsOfText[i].Lenght == numOfLetter
                        && SymbolHelp.Consonants.Contains(item.ElementsOfText[i].ToString()[0].ToString()))
                    {
                        item.Remove(i);
                    }
                }
            }
        }

        public void ReplacementWordsInSent(int numOfSent, int lenghtWord, string str)
        {
            if (numOfSent > sentences.Count)
            {
                numOfSent = sentences.Count;
            }
            if (numOfSent > 0)
            {
                foreach (var item in sentences[numOfSent - 1].ElementsOfText)
                {
                    if (item.Lenght == lenghtWord && item is Word)
                    {
                        item.Clear();
                        for (int i = 0; i < str.Length; i++)
                        {
                            item.Add(new Symbol() { Value = str[i] });
                        }
                    }
                }
            }
        }

        public Dictionary<string, List<int>> GetConcordance()
        {
            IEnumerable<Word> buffWords = new List<Word>();

            foreach (var item in sentences)
            {
                buffWords = buffWords.Concat(item.ElementsOfText.Where(x => x is Word).Select(x => x as Word));
            }

            List<Word> buffOrderedWords = buffWords.OrderBy(x => x.ToString()).ToList();
            List<int> listOfPages = new List<int>();
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();

            for (int i = 0; i < buffOrderedWords.Count - 1; i++)
            {
                if (buffOrderedWords[i].ToString().ToLower() == buffOrderedWords[i + 1].ToString().ToLower())
                {
                    listOfPages.Add((buffOrderedWords[i]).NumberOfPage);

                    if (i + 1 == buffOrderedWords.Count - 1)
                    {
                        listOfPages.Add((buffOrderedWords[i + 1]).NumberOfPage);
                        dict.Add((buffOrderedWords[i + 1]).ToString().ToLower(), listOfPages.ToList());
                    }
                }
                else
                {
                    listOfPages.Add((buffOrderedWords[i]).NumberOfPage);
                    dict.Add((buffOrderedWords[i]).ToString().ToLower(), listOfPages.ToList());
                    listOfPages.Clear();

                    if (i + 1 == buffOrderedWords.Count - 1)
                    {
                        listOfPages.Add((buffOrderedWords[i + 1]).NumberOfPage);
                        dict.Add((buffOrderedWords[i + 1]).ToString().ToLower(), listOfPages.ToList());
                    }
                }
            }

            return dict;
        }
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            foreach (var sent in sentences)
            {
                foreach (var textElements in sent.ElementsOfText)
                {
                    s.Append(textElements.ToString());
                }
            }
            return s.ToString();
        }
    }
}
