using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Text
    {
        private const int page=1;

        List<Sentence> sentences = new List<Sentence>();

        public List<Sentence> Sentences
        {
            get { return sentences; }
            set { sentences = value; }
        }

        public Text()
        { 
        }

        public Text(StringBuilder text)
        {
            DelExtraSymbols(text);
            GetSenteces(text.ToString());
        }

        private void DelExtraSymbols(StringBuilder text)
        {
            for (int i = 0; i < text.Length-1; i++)
            {
                if (text[i] == ' ' && text[i + 1] == ' ' || text[i] == '\n')
                    {
                        text.Remove(i, 1);
                        i--;
                    }
            }
        }

        private void GetSenteces(string text)
        {
            List<Symbol> buffTextElements = new List<Symbol>();

            int startindex = 0;
            int indexOfCaret = 0;
            bool endFile = false;
            for (int i = 0; i < text.Length - 1; i++)
            {
                if ((!SymbolHelp.symbolsInSent.Contains(text[i].ToString()) && !SymbolHelp.symbolsEndOfSent.Contains(text[i].ToString()))
                    && (SymbolHelp.symbolsInSent.Contains(text[i + 1].ToString()) || SymbolHelp.symbolsEndOfSent.Contains(text[i + 1].ToString())))
                {
                    buffTextElements.Add(new Word() { Value = text.Substring(startindex, i - startindex + 1), NumberOfPage = (indexOfCaret + 1) / page });

                    if (text[i + 1].ToString() == "\r")
                    {
                        text.Remove(i + 1, 1);
                        indexOfCaret++;
                    }
                    else
                    {
                        buffTextElements.Add(new PunctuationMark() { Value = text[i + 1].ToString() });
                        if (i + 2 == text.Length)
                        {
                            endFile = true;
                        }
                    }

                    i++;

                    startindex = i + 1;

                    if (SymbolHelp.symbolsEndOfSent.Contains(text[i].ToString()))
                    {
                        this.sentences.Add(new Sentence() { ElementsOfText = buffTextElements.Select(x => x).ToList() });

                        buffTextElements.Clear();
                    }
                }
                else
                {
                    if (SymbolHelp.symbolsInSent.Contains(text[i].ToString()) || SymbolHelp.symbolsEndOfSent.Contains(text[i].ToString()))
                    {
                        if (text[i].ToString() == "\r")
                        {
                            text.Remove(i, 1);
                            indexOfCaret++;
                        }
                        else
                        {
                            buffTextElements.Add(new PunctuationMark() { Value = text[i].ToString() });
                        }

                        if (SymbolHelp.symbolsEndOfSent.Contains(text[i].ToString()))
                        {
                            this.sentences.Add(new Sentence() { ElementsOfText = buffTextElements.Select(x => x).ToList() });

                            buffTextElements.Clear();
                        }

                        startindex = i + 1;
                    }
                }
            }

            if (!endFile)
            {
                buffTextElements.Add(new PunctuationMark() { Value = text.Last().ToString() });
                this.sentences.Add(new Sentence() { ElementsOfText = buffTextElements.Select(x => x).ToList() });
            }
        }

        public Dictionary<string,List<int>> GetConcordance()
        {
            IEnumerable<Symbol> buffWords = new List<Symbol>();

            foreach (var item in sentences)
	        {
                 buffWords = buffWords.Concat(item.ElementsOfText.Where(x => x is Word).Select(x => x));
	        }

            List<Symbol> buffOrderedWords = buffWords.OrderBy(x=>x.Value).ToList();

            List<int> listOfPages = new List<int>();

            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();

            for (int i = 0; i < buffOrderedWords.Count-1; i++)
            {
                if (buffOrderedWords[i].Value.ToLower() == buffOrderedWords[i + 1].Value.ToLower())
                {
                    listOfPages.Add(((Word)buffOrderedWords[i]).NumberOfPage);

                    if (i+1==buffOrderedWords.Count-1)
                    {
                        listOfPages.Add(((Word)buffOrderedWords[i + 1]).NumberOfPage);
                        dict.Add(((Word)buffOrderedWords[i + 1]).Value.ToLower(), listOfPages.ToList());
                    }
                }
                else
                {
                    listOfPages.Add(((Word)buffOrderedWords[i]).NumberOfPage);

                    dict.Add(((Word)buffOrderedWords[i]).Value.ToLower(), listOfPages.ToList());

                    listOfPages.Clear();

                    if (i + 1 == buffOrderedWords.Count - 1)
                    {
                        listOfPages.Add(((Word)buffOrderedWords[i + 1]).NumberOfPage);
                        dict.Add(((Word)buffOrderedWords[i + 1]).Value, listOfPages.ToList());
                    }
                }
            }

            if ( buffOrderedWords.Count==1)
            {
                listOfPages.Add(1);
                dict.Add(((Word)buffOrderedWords[0]).Value, listOfPages.ToList());
            }

            return dict;       
        }

        public List<Sentence> GetListOFSentByNumOfWord()
        {
               List<Sentence> r=new List<Sentence>();
               return  r = sentences.OrderBy(x=>x.NumberOfWords).Select(x=>x).ToList();
        }

        public List<Symbol> GetListWordsInSent(int numOfLetter, PunctuationMark endOfSent)
        {
            var r = sentences.Where(x => x.ElementsOfText.Last().Value == endOfSent.Value).Select(x => x);

            IEnumerable<Symbol> buffOfSymbols = new List<Symbol>();

            foreach (var item in r)
            {
                buffOfSymbols = buffOfSymbols.Concat(item.ElementsOfText
                       .Where(x => x is Word && x.Length == numOfLetter)
                       .Select(x => x));
            }

            return buffOfSymbols.Distinct().ToList();
        }

        public void DelWordsWithConsonants(int numOfLetter)
        {
            IEnumerable<Symbol> buffOfSymbols1 = new List<Symbol>();
            
            foreach (var item in sentences)
            {
                buffOfSymbols1 = buffOfSymbols1.Concat(item.ElementsOfText.Where(x => (x is Word && x.Length == numOfLetter 
                    && SymbolHelp.consonants.Contains(x.Value.First().ToString().ToLower()))).Select(x => x));
            }

            foreach (var item in sentences)
            {
                item.ElementsOfText = item.ElementsOfText.Except(buffOfSymbols1.Distinct()).ToList();
            }
        }

        public void ReplacementWordsInSent(int numOfSent, int lenghtWord, string str )
        {
            if (numOfSent>sentences.Count)
            {
                numOfSent = sentences.Count;
            }
            if (numOfSent > 0)
            {
                foreach (var item in sentences[numOfSent - 1].ElementsOfText)
                {
                    if (item.Length == lenghtWord)
                    {
                        item.Value = str;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            foreach (var sent in sentences)
            {
                foreach (var textElements in sent.ElementsOfText)
                {
                    s.Append(textElements.Value);
                }
            }
            return s.ToString();
        }
    }
}
