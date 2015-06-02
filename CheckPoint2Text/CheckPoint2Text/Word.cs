using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Word : TextElement, IWord
    {
        public Word()
        { }

        public Word(List<Symbol> symbols)
        {
            if (!SymbolHelp.SymbolsEndOfSent.Contains(symbols.Last().Value.ToString())
                   && !SymbolHelp.SymbolsInSent.Contains(symbols.Last().Value.ToString()))
            {
                base.chars = symbols;
            }
            else
            {
                throw new Exception("Incorrect date");
            }
        }

        public int NumberOfPage
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
