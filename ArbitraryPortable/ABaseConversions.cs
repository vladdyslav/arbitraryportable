using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArbitraryPortable.Parsers;

namespace ArbitraryPortable
{
    /// <summary>
    /// ALong numbers base conversions extensions
    /// </summary>
    public static class ABaseConversions
    {
        /// <summary>
        /// Converts string representation of a number in a given base to ALong number.
        /// Extension does not check if you provide invalid numbers, e.g. '1010102' in base 2 - you will just get incorrect answer.
        /// </summary>
        /// <param name="number">String representation of a number</param>
        /// <param name="aBase">Base number is presented in</param>
        /// <param name="sym">String of symbols used to represent a number.</param>
        /// <returns>ALong number</returns>
        public static ALong FromArbitraryBase(this string number, int aBase, string sym = null)
        {
            if (String.IsNullOrEmpty(sym)) { sym = GetSymbols(aBase); }
            if (aBase < 37) { number = number.ToLower(); } // Ignore case if base <= 36

            var r = number.Reverse();
            var resp = new ALong(0);
            var i = 0;
            foreach (var c in r)
            {
                var index = sym.IndexOf(c);
                resp = resp + AMath.Pow(new ALong(aBase), i) * index;
                i++;
            }
            return resp;
        }

        /// <summary>
        /// Converts ALong number into a string representation number using provided base.
        /// </summary>
        /// <param name="number">ALong number.</param>
        /// <param name="aBase">Base to convert ALong number to.</param>
        /// <param name="sym">String of symbols used to represent a number.</param>
        /// <returns>String represetantion of a numer in a given base.</returns>
        public static string ToArbitraryBase(this ALong number, int aBase, string sym = null)
        {
            if (String.IsNullOrEmpty(sym)) { sym = GetSymbols(aBase); }
            var res = String.Empty;
            do
            {
                res = sym[(new ALong(number) % aBase).ToString().ToInt()] + res;
                number /= aBase;

            } while (number > 0);

            return res;
        }

        /// <summary>
        /// Provides default symbols string.
        /// </summary>
        /// <param name="aBase">Base number to get symbols for.</param>
        /// <returns>String of symbols.</returns>
        private static string GetSymbols(int aBase)
        {
            var defData = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (aBase < 63) { return defData.Substring(0, aBase); }
            for (int i = 63; i < aBase + 1; i++)
            {
                defData += (char)(123 + i - 63);
            }
            return defData;
        }

        /// <summary>
        /// Reverses a string.
        /// </summary>
        /// <param name="str">String to reverse.</param>
        /// <returns>Reversed string.</returns>
        private static string Reverse(this string str)
        {
            var res = String.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                res = str[i] + res;
            }
            return res;
        }
    }
}
