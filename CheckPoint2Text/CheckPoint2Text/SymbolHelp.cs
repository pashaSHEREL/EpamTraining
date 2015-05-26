using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPoint2Text
{
    public class SymbolHelp
    {
        public static readonly string[] symbolsInSent = {",",";",":"," ","    ","-","\'","\"","(",")","\r"};
        public static readonly string[] symbolsEndOfSent = { "!", "?", ".", "!?","..." };
        public static readonly string[] operators = { "+", "-", "*", "/", ">>", "<<", "++", "--", "%", "", "=", "==", "<", ">",
                                                        ">=", "<=", "!", "!=", "+=", "-=", "=+", "=-", "=>", "&&", "||", "??", "?:"};
        public static readonly string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m",
                                                         "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
        public static readonly string[] vowels = { "a", "e", "i", "o", "u", "y" };
        public static readonly string[] digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    }
}
