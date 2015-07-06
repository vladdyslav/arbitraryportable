using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ArbitraryPortable.Parsers
{
    public static class IntParsers
    {
        public static int ToInt(this Char ch) 
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

        public static int ToInt(this string str)
        {
            return Int32.Parse(str);

            //var result = 0; 
            //var negative = 1;
            //if (str[0] == '-') { negative = -1; str = str.Substring(1); }
            //for (int i = 0; i < str.Length; i++)
            //{
            //    var addon = str[i].ToInt();
            //    if (addon > 0) { addon = addon * iexp(10, (str.Length - i - 1)); }
            //    result += addon;
            //}
            //return result * negative;
        }

        public static int iexp(int a, int b)
        {
            int y = 1;

            while (true)
            {
                if ((b & 1) != 0) y = a * y;
                b = b >> 1;
                if (b == 0) return y;
                a *= a;
            }
        }
    }
}
