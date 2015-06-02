using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class FactoryForWords
    {
        public ITextElement Create(List<Symbol> symbols, int numOfPage)
        {
            ITextElement textElement;
      
            if (!SymbolHelp.SymbolsEndOfSent.Contains(symbols.Last().Value.ToString())
                    && !SymbolHelp.SymbolsInSent.Contains(symbols.Last().Value.ToString()))
            {
                textElement = new Word(symbols);
                ((Word)textElement).NumberOfPage = numOfPage;
            }
            else
            {
                if (SymbolHelp.SymbolsInSent.Contains(symbols.Last().Value.ToString()) || SymbolHelp.SymbolsEndOfSent.Contains(symbols.Last().Value.ToString()))
                {
                    textElement = new PunctuationMark(symbols);
                }
                else
                {
                    throw new Exception("Incorrect date");
                }
             
            }

            return textElement;
        }
    }
}
