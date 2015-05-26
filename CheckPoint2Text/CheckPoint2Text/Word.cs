using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    
    public class Word:Symbol
    {
        public override string  Value 
        { 
            get
            {
                return base.value;
            }
            set
            {
                if (!SymbolHelp.symbolsEndOfSent.Contains(value.Last().ToString())
                    && !SymbolHelp.symbolsInSent.Contains(value.Last().ToString()))
                {
                    base.value = value;
                }
                else
                {
                    throw new Exception("The word must end in the letter.");
                }
            }
        }
        public List<int> k = new List<int>();

        public int NumberOfPage
        {
            get;
            set;
        }

        public Word()
        { 
        }

        public Word(string value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj.ToString()==this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
