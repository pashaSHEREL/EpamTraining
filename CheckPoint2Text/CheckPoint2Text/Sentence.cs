using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint2Text
{
    public enum TypeSent
    {
        Voprositelnoe,
        Voskli,
        Pobudit,
        Povest
    }

    public class Sentence
    {
        private List<ITextElement> elementsOfText = new List<ITextElement>();

        public Sentence()
        { }

        public Sentence(List<ITextElement> elementsOfText)
        {
            this.elementsOfText = elementsOfText;
        }

        public ReadOnlyCollection<ITextElement> ElementsOfText
        {
            get
            {
                return new ReadOnlyCollection<ITextElement>(elementsOfText);
            }
        }

        public void Remove(int i)
        {
            elementsOfText.RemoveAt(i);
        }

        public TypeSent TypeOfSentence
        {
            get
            {
                switch (elementsOfText.Last().ToString())
                {
                    case ".": return TypeSent.Povest;
                    case "!": return TypeSent.Voskli;
                    case "?": return TypeSent.Voprositelnoe;
                    default: return TypeSent.Povest;
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
                s.Append(item.ToString());
            }
            return s.ToString();
        }
    }
}
