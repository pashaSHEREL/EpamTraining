using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Symbol
    {
        protected string value;

        public virtual string Value 
        {
            get { return this.value; }
            set
            {
                if (value.Length <= 3)
                {
                    this.value = value;
                }
                else
                {
                    throw new Exception("Maximum length of 3 characters.");
                }
            }
        }

        public int Length
        {
            get { return value.Length; }
        }

        public Symbol()
        { 
            
        }

        public Symbol(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
