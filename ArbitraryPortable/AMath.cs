using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArbitraryPortable.Parsers;

namespace ArbitraryPortable
{
    /// <summary>
    /// Basic Implementation of an Arbitrary length Math.
    /// </summary>
    public static class AMath
    {
        /// <summary>
        /// Returns the larger of two ALong instances.
        /// </summary>
        /// <param name="num1">The first of two ALong instances to compare.</param>
        /// <param name="num2">The second of two ALong instances to compare.</param>
        /// <returns>Parameter num1 or num2, whichever is larger.</returns>
        public static ALong Max(ALong num1, ALong num2)
        {
            if (num1 < num2) { return num2; }
            return num1;
        }

        /// <summary>
        /// Returns the smaller of two ALong instances.
        /// </summary>
        /// <param name="num1">The first of two ALong instances to compare.</param>
        /// <param name="num2">The second of two ALong instances to compare.</param>
        /// <returns>Parameter num1 or num2, whichever is smaller.</returns>
        public static ALong Min(ALong num1, ALong num2)
        {
            if (num1 > num2) { return num2; }
            return num1;
        }

        /// <summary>
        /// Calculates a Subtraction of a two ALong values.
        /// </summary>
        /// <param name="num1">Minuend</param>
        /// <param name="num2">Subtrahend</param>
        /// <returns>ALong instance representing a difference between minuend and subtrahend.</returns>
        public static ALong Sub(ALong num1, ALong num2)
        {
            if (num1 == num2) { return new ALong(0); }
            if (!num1.IsNegative && !num2.IsNegative) { return Sum(num1, new ALong(num2).SetNegative(true)); }
            if (!num1.IsNegative && num2.IsNegative) { return Sum(num1, new ALong(num2).SetNegative(false)); }
            if (num1.IsNegative && !num2.IsNegative) { return Sum(new ALong(num1).SetNegative(false), new ALong(num2).SetNegative(false)).SetNegative(true); }
            if (num1.IsNegative && num2.IsNegative) { return Sum(num1, new ALong(num2).SetNegative(false)); }

            return new ALong(0);
        }

        /// <summary>
        /// Calculates a Summary of a two ALong values.
        /// </summary>
        /// <param name="num1">First addend</param>
        /// <param name="num2">Second addend</param>
        /// <returns>ALong instance representing a sum of two addends.</returns>
        public static ALong Sum(ALong num1, ALong num2)
        {
            if (num1.IsNegative == num2.IsNegative)
            {
                return AbsSum(num1, num2).SetNegative(num1.IsNegative);
            }

            var n1 = Abs(num1);
            var n2 = Abs(num2);
            var result = AbsSub(n1, n2);
            if (n1 < n2) { result.SetNegative(num2.IsNegative); }
            if (n1 > n2) { result.SetNegative(num1.IsNegative); }
            return result;
        }

        /// <summary>
        /// Calculates a Summary of a two ALong absolute values, a.k.a "School" method.
        /// </summary>
        /// <param name="num1">First addend</param>
        /// <param name="num2">Second addend</param>
        /// <returns>ALong instance representing a sum of two addends.</returns>
        private static ALong AbsSum(ALong num1, ALong num2)
        {
            var result = String.Empty;
            int carry = 0;
            var max = Math.Max(num1.Length(), num2.Length());
            for (int i = 0; i < max; i++)
            {
                var n1 = num1.GetRNumAtIndex(i);
                var n2 = num2.GetRNumAtIndex(i);
                var ns = n1 + n2 + carry;
                carry = 0;
                if (ns >= 10) { ns = ns - 10; carry = 1; }
                result = ns.ToString() + result;
                if (i == max - 1 && carry > 0)
                {
                    result = carry.ToString() + result;
                }
            }
            return new ALong(result);
        }

        /// <summary>
        /// Calculates a Subtraction of a two ALong absolute values, a.k.a "School" method.
        /// </summary>
        /// <param name="num1">Minuend</param>
        /// <param name="num2">Subtrahend</param>
        /// <returns>ALong instance representing a difference between absolute values of minuend and subtrahend.</returns>
        private static ALong AbsSub(ALong num1, ALong num2)
        {
            if (num1 == num2) { return new ALong(0); }
            var big = AMath.Max(Abs(num1), Abs(num2));
            var small = AMath.Min(Abs(num1), Abs(num2));

            var result = String.Empty;
            int carry = 0;
            for (int i = 0; i < big.Length(); i++)
            {
                var n1 = big.GetRNumAtIndex(i);
                var n2 = small.GetRNumAtIndex(i);
                var ns = n1 - carry;
                carry = 0;
                if (ns < n2) { ns += 10; carry = 1; }
                ns = ns - n2;
                result = ns.ToString() + result;
            }

            return new ALong(result);
        }

        /// <summary>
        /// Calculates a product of a two ALong instances, a.k.a "School" method (very slow)
        /// </summary>
        /// <param name="num1">Multiplicand</param>
        /// <param name="num2">Multiplier</param>
        /// <returns>Product of num1 and num2.</returns>

        //TODO: replace with Karatsuba, Schönhage–Strassen or Knuth. (Fast Fourier transform?).
        //TODO: use base of size of Int32 to "shorten" sizes of numbers within multiplication speed being kept.
        public static ALong Mul(ALong num1, ALong num2)
        {
            var result = new ALong(0);
            if (num1 == 0 || num2 == 0) { return result; }
            var carry = 0;
            for (int i = 0; i < num1.Length(); i++)
            {
                var n1 = num1.GetRNumAtIndex(i);
                var res = String.Empty;
                var n2len = num2.Length();
                carry = 0;
                for (int k = 0; k < n2len; k++)
                {
                    var n2 = num2.GetRNumAtIndex(k);
                    var ns = n1 * n2 + carry;
                    carry = ns / 10;
                    res = (ns % 10) + res;
                    if (k == n2len - 1 && carry > 0)
                    {
                        res = carry.ToString() + res;
                    }
                }
                var mulres = new ALong(res);
                mulres.MulBase(i);
                result += mulres;
            }
            if (num1.IsNegative != num2.IsNegative) { result.SetNegative(true); }
            return result;
        }

        /// <summary>
        /// Calculates a quotient and remainder of division of an ALong instance and integer.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>A Tuple of Quotient and Remainder.</returns>
        public static Tuple<ALong, ALong> DivInt(ALong num1, int num2)
        {
            if (num2 == 0) { throw new ArgumentException("Division by zero"); }
            if (num1 == 0) { return new Tuple<ALong, ALong>(new ALong(0), new ALong(0)); }
            if (num1 == num2) { return new Tuple<ALong, ALong>(new ALong(1), new ALong(0)); }
            if (Abs(num1) < Math.Abs(num2)) { return new Tuple<ALong, ALong>(new ALong(0), new ALong(num1)); }

            var strres = "";

            string divident = "";
            var dividentInt = 0L;
            var divisor = Math.Abs(num2);
            int remainder = 0;

            for (int i = 0; i < num1.Length(); i++)
            {
                divident += num1.Num[i];
                dividentInt = divident.ToLong();
                if (dividentInt < divisor)
                {
                    if (strres != "") { strres += "0"; }
                    continue;
                }

                var res = dividentInt / divisor;
                strres += res;
                divident = (dividentInt - res * divisor).ToString();
            }
            remainder = divident.ToInt();
            return new Tuple<ALong, ALong>(new ALong(strres).SetNegative(num1.IsNegative != num2 < 0), new ALong(num1.IsNegative ? remainder * -1 : remainder));
        }

        /// <summary>
        /// Calculates a quotient and remainder of division of a two ALong instances.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>A Tuple of Quotient and Remainder.</returns>
        public static Tuple<ALong, ALong> Divide(ALong num1, ALong num2)
        {
            var num2a = Abs(num2);
            if (num2a < Int32.MaxValue) { return DivInt(num1, num2.ToString().ToInt()); }
            if (num2 == 0) { throw new ArgumentException("Division by zero"); }
            if (num1 == 0) { return new Tuple<ALong, ALong>(new ALong(0), new ALong(0)); }

            var num1a = Abs(num1);
            if (num1a == num2a) { return new Tuple<ALong, ALong>(new ALong(1).SetNegative(num1.IsNegative != num2.IsNegative), new ALong(0)); }
            if (num1a < num2a) { return new Tuple<ALong, ALong>(new ALong(0), new ALong(num1)); }

            var b = 10; // base
            var down = new ALong(0);

            // Get upper limit
            var up = new ALong(b);
            while (num1a > num2a * up) { up = up * b; }

            // Divide
            while (up - down > 1)
            {
                var cur = (down + up) / 2;
                var c = num2a * (cur);
                if (c < num1a) { down = cur; continue; }
                if (c > num1a) { up = cur; continue; }
                if (c == num1a) { down = cur; up = cur; continue; }
            }

            var remainder = num1a - down * num2a;
            down.SetNegative(num1.IsNegative != num2.IsNegative);
            remainder.SetNegative(num1.IsNegative);
            return new Tuple<ALong, ALong>(down, remainder);
        }

        /// <summary>
        /// Creates a new ALong instance with the absolute value.
        /// </summary>
        /// <param name="num">ALong instance.</param>
        /// <returns>New ALong instance with an absolute value.</returns>
        public static ALong Abs(ALong num)  { return new ALong(num.Num); }

        /// <summary>
        /// Calculates a specified ALong number raised to the specified power.
        /// </summary>
        /// <param name="num">ALong instance to be raised to a power.</param>
        /// <param name="exp">ALong instance that specifies a power.</param>
        /// <returns>New ALong instance indicating specified ALong number raised to the specified power.</returns>
        public static ALong Pow(ALong num, ALong exp) 
        {
            var res = new ALong(1);
            while (exp != 0)
            {
                if (exp % 2 != 0)
                {
                    res *= num;
                    exp -= 1;
                }
                num *= num;
                exp /= 2;
            }
            return res;
        }

        /// <summary>
        /// Calculates a specified ALong number raised to the specified power.
        /// </summary>
        /// <param name="num">ALong instance to be raised to a power.</param>
        /// <param name="exp">int value that specifies a power.</param>
        /// <returns>New ALong instance indicating specified ALong number raised to the specified power.</returns>
        public static ALong Pow(ALong num, int exp)
        {
            return Pow(num, new ALong(exp));
        }

        /// <summary>
        /// Calculates a specified ALong number raised to the specified power.
        /// </summary>
        /// <param name="num">ALong instance to be raised to a power.</param>
        /// <param name="exp">long value that specifies a power.</param>
        /// <returns>New ALong instance indicating specified ALong number raised to the specified power.</returns>
        public static ALong Pow(ALong num, long exp)
        {
            return Pow(num, new ALong(exp));
        }

        /// <summary>
        /// Calculates a specified ALong number raised to the specified power.
        /// </summary>
        /// <param name="num">ALong instance to be raised to a power.</param>
        /// <param name="exp">string representation of a number that specifies a power.</param>
        /// <returns>New ALong instance indicating specified ALong number raised to the specified power.</returns>
        public static ALong Pow(ALong num, string exp)
        {
            return Pow(num, new ALong(exp));
        }

        public static double Pow(double num, long exp)
        {
            double result = 1;
            while (exp != 0)
            {
                if (exp % 2 != 0)
                {
                    result *= num;
                    exp -= 1;
                }
                num *= num;
                exp /= 2;
            }
            return result;
        }
    }
}
