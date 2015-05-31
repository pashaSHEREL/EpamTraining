using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class FactoryForTextElements
    {
        public ITextElement Create(List<Symbol> symbols, int numOfPage)
        {
            ITextElement textElement;
      
            if (!SymbolHelp.SymbolsEndOfSent.Contains(symbols.Last().Value.ToString())
                    && !SymbolHelp.SymbolsInSent.Contains(symbols.Last().Value.ToString()))
            {
                textElement = new Word();
                textElement.Chars = symbols;
                ((Word)textElement).NumberOfPage = numOfPage;
            }
            else
            {
                if (SymbolHelp.SymbolsInSent.Contains(symbols.Last().Value.ToString()) 
                    || SymbolHelp.SymbolsEndOfSent.Contains(symbols.Last().Value.ToString()))
                {
                    textElement = new PunctuationMark();
                    textElement.Chars = symbols;
                }
                else
                {
                    throw new Exception("Error in a textElement");
                }
             
            }

            return textElement;
        }
    }
}
