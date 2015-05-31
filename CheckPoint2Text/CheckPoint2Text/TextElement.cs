using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public abstract class TextElement:ITextElement
    {
        public List<Symbol> Chars
        {
            get;
            set;
        }

        public string Value
        {
            get
            {
                string str = "";
                foreach (var item in Chars)
                {
                    str += item.Value.ToString();
                }
                return str;
            }
        }

        public int Lenght
        {
            get { return Value.Length; }
        }
      
    }
}
