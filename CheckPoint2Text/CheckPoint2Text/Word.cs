using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Word:TextElement,IWord
    {
        public int NumberOfPage
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Value;
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
