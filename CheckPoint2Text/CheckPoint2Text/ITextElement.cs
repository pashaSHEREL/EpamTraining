using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace CheckPoint2Text
{
    public interface ITextElement
    {
        ReadOnlyCollection<Symbol>Chars { get; }
        int Lenght { get;}
        void Clear();
        void Add(Symbol symbol);
    }
}
