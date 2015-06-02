using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Parser
    {
        private FactoryForSentences factoryForSentences = new FactoryForSentences();
        private FactoryForText factoryForText = new FactoryForText();
        private FactoryForWords factoryForWords = new FactoryForWords();
        private const int Page = 1;

        public Text Parse(StringBuilder text)
        {
            List<ITextElement> buffTextElements = new List<ITextElement>();
            List<Sentence> buffSentences = new List<Sentence>();
            List<Symbol> buffWordPunctuationMark = new List<Symbol>();
            int indexOfCaret = 1;
            DelExtraSymbols(text);

            for (int i = 0; i < text.Length - 1; i++)
            {
                buffWordPunctuationMark.Add(new Symbol() { Value = text[i] });
             
                if ((!SymbolHelp.SymbolsInSent.Contains(text[i].ToString()) && !SymbolHelp.SymbolsEndOfSent.Contains(text[i].ToString()))
                        && (SymbolHelp.SymbolsInSent.Contains(text[i + 1].ToString())
                        || SymbolHelp.SymbolsEndOfSent.Contains(text[i + 1].ToString())))
                {
                    buffTextElements.Add(factoryForWords.Create(buffWordPunctuationMark.ToList(), indexOfCaret / Page));
                    buffWordPunctuationMark.Clear();

                    if (text[i + 1].ToString() == "\r")
                    {
                        text.Remove(i + 1, 1);
                        indexOfCaret++;
                    }
                }
                else
                {
                    if (SymbolHelp.SymbolsInSent.Contains(text[i].ToString()))
                    {
                        buffTextElements.Add(factoryForWords.Create(buffWordPunctuationMark.ToList(), indexOfCaret / Page));
                        buffWordPunctuationMark.Clear();
                    }

                    if ((SymbolHelp.SymbolsEndOfSent.Contains(text[i].ToString()) && text[i + 1] == ' ')
                        || (SymbolHelp.SymbolsEndOfSent.Contains(text[i].ToString()) && text[i + 1] == '\r'))
                    {
                        buffTextElements.Add(factoryForWords.Create(buffWordPunctuationMark.ToList(), indexOfCaret / Page));
                        buffWordPunctuationMark.Clear();
                        buffSentences.Add(factoryForSentences.Create(buffTextElements.ToList()));
                        buffTextElements.Clear();
                    }

                    if (text[i].ToString() == "\r")
                    {
                        text.Remove(i, 1);
                        indexOfCaret++;
                    }
                }

                if (i + 1 == text.Length - 1)
                {
                    buffWordPunctuationMark.Add(new Symbol() { Value = text[i + 1] });
                    buffTextElements.Add(factoryForWords.Create(buffWordPunctuationMark.ToList(), indexOfCaret / Page));
                   
                    if (SymbolHelp.SymbolsEndOfSent.Contains(buffTextElements.Last().ToString()))
                    {
                        buffSentences.Add(factoryForSentences.Create(buffTextElements.ToList()));
                    }
                }
            }

            return factoryForText.Create(buffSentences);
        }

        private void DelExtraSymbols(StringBuilder text)
        {
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == ' ' && text[i + 1] == ' ' || text[i] == '\n'||text[i]=='\t')
                {
                    text.Remove(i, 1);
                    i--;
                }
            }
        }

    }
}
