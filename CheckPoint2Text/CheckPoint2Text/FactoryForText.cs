using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class FactoryForText
    {
        public Text Create(List<Sentence> sentences)
        {
            return new Text(sentences);
        }
    }
}
