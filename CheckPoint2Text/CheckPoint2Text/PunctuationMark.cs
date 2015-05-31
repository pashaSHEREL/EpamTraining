using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class PunctuationMark:TextElement,IPunctuationMark
    {
        public bool SymbolEndOfSent
        {
            get { return SymbolHelp.SymbolsEndOfSent.Contains(this.Value); }
        }
    }
}
