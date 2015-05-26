using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Sentence //: //ICollection<ITextElement>
    {
        private List<Symbol> elementsOfText=new List<Symbol>();

        public List<Symbol> ElementsOfText
        {
            get { return elementsOfText; }
            set
            {
                if (SymbolHelp.symbolsEndOfSent.Contains(value.Last().Value))
                {
                    elementsOfText = value;
                }
            }
        }

        public int NumberOfWords
        {
            get
            {
                int i = 0;
                foreach (var item in elementsOfText)
                {
                    if (item is Word)
                    {
                        i++;
                    }
                }
                return i;
            }
        }

        public override string ToString()
        {
            StringBuilder s=new StringBuilder();
            foreach (var item in elementsOfText)
            {
                s.Append(item.Value);
            }
            return s.ToString();
        }
    }
}
