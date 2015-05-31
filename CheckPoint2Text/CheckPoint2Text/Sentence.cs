using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Sentence
    {
        private List<ITextElement> elementsOfText = new List<ITextElement>();

        public List<ITextElement> ElementsOfText
        {
            get
            {
                return elementsOfText;
            }
            set
            {
                elementsOfText = value;
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
            StringBuilder s=new StringBuilder(1000);

            foreach (var item in elementsOfText)
            {
                s.Append(item.Value);
            }
            return s.ToString();
        }
    }
}
