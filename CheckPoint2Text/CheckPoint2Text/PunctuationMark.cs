using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class PunctuationMark:Symbol
    {
        public override string Value
        {
            get
            {
                return base.value;
            }
            set
            {
                if (SymbolHelp.symbolsInSent.Contains(value) || SymbolHelp.symbolsEndOfSent.Contains(value))  
                {
                    base.value = value;
                }
                else
                {
                    throw new Exception("This is not a punctuation mark.");
                }
            }
        }

        public PunctuationMark(string value)
        {
                Value = value;
        }

        public PunctuationMark()
        { 
        }
    }
}
