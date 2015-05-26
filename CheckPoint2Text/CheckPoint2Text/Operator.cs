using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    class Operator:Symbol
    {
         public override string Value
        {
            get
            {
                return base.value;
            }
            set
            {
                if (SymbolHelp.operators.Contains(value) || SymbolHelp.symbolsEndOfSent.Contains(value))
                {
                    base.value = value;
                }
                else
                {
                    throw new Exception("This is not a operator.");
                }
            }
        }

        public Operator(string value)
        {
                Value = value;
        }

        public Operator()
        { 
        }
     }
}

