using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public interface ITextElement
    {
        List<Symbol> Chars { get; set; }
        string Value { get;  }
        int Lenght   { get;}
    }
}
