using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ArbitraryPortable.Parsers
{
    public static class LongParsers
    {
        public static long ToLong(this Char ch) 
        { 
            switch (ch) 
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
            }
            throw new FormatException("Parameter must only contain numbers");
        }

        public static long ToLong(this string str)
        {
            return long.Parse(str);
        }
    }
}
