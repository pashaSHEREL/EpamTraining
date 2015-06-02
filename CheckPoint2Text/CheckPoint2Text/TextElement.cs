using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint2Text
{
    public abstract class TextElement:ITextElement
    {
        protected List<Symbol> chars = new List<Symbol>();

        public ReadOnlyCollection<Symbol> Chars
        {
            get
            {
                return new ReadOnlyCollection<Symbol>(chars); 
            }
        }

        public int Lenght
        {
            get { return chars.Count; }
        }

        public void Clear()
        {
            this.chars.Clear();
        }

        public void Add(Symbol symbol)
        {
            this.chars.Add(symbol);
        }
 
        public override string ToString()
        {
            string str = "";
            foreach (var item in chars)
            {
                str += item.Value.ToString();
            }
            return str;
        }
    }
}
