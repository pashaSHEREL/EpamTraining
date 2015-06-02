using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class SymbolHelp
    {
        public static readonly string[] SymbolsInSent = { ",", ";", ":", " ", "    ", "-", "\'", "\"", "(", ")", "\r" };
        public static readonly string[] SymbolsEndOfSent = { "!", "?", ".", "!?", "..." };
        public static readonly string[] Consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m",
                                                         "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

    }
}