using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class FactoryForSentences
    {
        public Sentence Create(List<ITextElement> elementsOfText)
        {
            Sentence sent;
            PunctuationMark punct = elementsOfText.Last() as PunctuationMark;

            if (punct != null && punct.SymbolEndOfSent)
            {
                sent = new Sentence(elementsOfText);
            }
            else
            {
                throw new Exception("Error in a sentece");
            }

            return sent;
        }
    }
}
