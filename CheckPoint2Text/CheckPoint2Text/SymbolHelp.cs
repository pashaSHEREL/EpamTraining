using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class SymbolHelp
    {
        public static readonly string[] SymbolsInSent = {",",";",":"," ","    ","-","\'","\"","(",")","\r"};
        public static readonly string[] SymbolsEndOfSent = { "!", "?", ".", "!?","..." };
        public static readonly string[] Operators = { "+", "-", "*", "/", ">>", "<<", "++", "--", "%", "", "=", "==", "<", ">",
                                                        ">=", "<=", "!", "!=", "+=", "-=", "=+", "=-", "=>", "&&", "||", "??", "?:"};
        public static readonly string[] Consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m",
                                                         "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
        public static readonly string[] Vowels = { "a", "e", "i", "o", "u", "y" };
        public static readonly string[] Digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    }
}
