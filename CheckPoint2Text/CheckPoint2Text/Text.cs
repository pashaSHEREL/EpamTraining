using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class Text
    { 
        List<Sentence> sentences = new List<Sentence>();
        
        public List<Sentence> Sentences
        {
            get { return sentences; }
            set { sentences = value; }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder(1000);

            foreach (var sent in sentences)
            {
                foreach (var textElements in sent.ElementsOfText)
                {
                    s.Append(textElements.Value);
                }
            }

            return s.ToString();
        }
    }
}
