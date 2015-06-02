using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class PunctuationMark:TextElement,IPunctuationMark
    {
        public PunctuationMark()
        { }

        public PunctuationMark(List<Symbol> symbols)
        {
            if (SymbolHelp.SymbolsInSent.Contains(symbols.Last().Value.ToString()) || SymbolHelp.SymbolsEndOfSent.Contains(symbols.Last().Value.ToString()))
            {
                base.chars = symbols;
               
            }
            else
            {
                throw new Exception("Incorrect date");
            }
        }

        public bool SymbolEndOfSent
        {
            get { return SymbolHelp.SymbolsEndOfSent.Contains(this.ToString()); }
        }
    }
}
