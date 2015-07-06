using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ArbitraryPortable.Parsers;

namespace ArbitraryPortable
{
    /// <summary>
    /// Implementation of an arbitrary long signed integer.
    /// </summary>
    public class ALong : IEquatable<ALong>, IComparable<ALong>
    {
        private string _num;
        private string _rnum;
        private bool _isNegative = false;

        /// <summary>
        /// Unsigned reversed string representation of the ALong value.
        /// </summary>
        private string RNum { 
            get 
            { 
                if (_rnum == null && _num != null) {
                    char[] charArray = _num.ToCharArray();
                    Array.Reverse(charArray);
                    _rnum = new string(charArray);
                }
                return _rnum;
            } 
        }

        /// <summary>
        /// Unsigned string representation of the ALong value.
        /// </summary>
        public string Num  { get { return _num; } }

        /// <summary>
        /// Negativity indicator of the ALong value.
        /// </summary>
        public bool IsNegative
        {
            get 
            {
                return _isNegative;
            }
            private set 
            {
                _isNegative = value;
            }
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ALong with the 0 value.
        /// </summary>
        public ALong() 
        {
            _num = "0";
        }

        /// <summary>
        /// Initializes a new instance of the ALong using another ALong instance (clone).
        /// </summary>
        /// <param name="num">Another ALong instance</param>
        public ALong(ALong num) 
        {
            _num = num._num;
            _rnum = num._rnum;
            _isNegative = num._isNegative;
        }

        /// <summary>
        /// Initializes a new instance of the ALong using integer value.
        /// </summary>
        /// <param name="num">int value</param>
        public ALong(int num) 
        {
            if (num < 0) { _isNegative = true; }
            _num = Math.Abs(num).ToString();
        }

        /// <summary>
        /// Initializes a new instance of the ALong using long value.
        /// </summary>
        /// <param name="num">long value</param>
        public ALong(long num)
        {
            if (num < 0) { _isNegative = true; }
            _num = Math.Abs(num).ToString();
        }

        /// <summary>
        /// Initializes a new instance of the ALong using string value.
        /// </summary>
        /// <param name="num">String representation of a number</param>
        /// <exception cref="FormatException">Thrown when num contains other charachters than '-' and '0'..'9']</exception>
        public ALong(string num)
        {
            if (!Regex.Match(num, @"^\-?\d+$").Success) 
            {
                throw new FormatException("Invalid number format");
            }
            var m = Regex.Match(num, @"^(-?)(0{0,})(\d+)$");
            if (m.Success)
            {
                if (m.Groups[1].Value == "-") { _isNegative = true; }
                _num = m.Groups[3].Value;
            }
            else 
            {
                throw new FormatException("Invalid number format");
            }
        }

        #endregion

        # region Operator +

        /// <summary>
        /// Implements '+' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">First addend</param>
        /// <param name="num2">Second addend</param>
        /// <returns>ALong instance representing a sum of two addends.</returns>
        public static ALong operator +(ALong num1, ALong num2)
        {
            return AMath.Sum(num1, num2);
        }

        /// <summary>
        /// Implements '+' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">First addend</param>
        /// <param name="num2">Second addend</param>
        /// <returns>ALong instance representing a sum of two addends.</returns>
        public static ALong operator +(ALong num1, int num2)
        {
            return AMath.Sum(num1, new ALong(num2));
        }

        /// <summary>
        /// Implements '+' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">First addend</param>
        /// <param name="num2">Second addend</param>
        /// <returns>ALong instance representing a sum of two addends.</returns>
        public static ALong operator +(ALong num1, long num2)
        {
            return AMath.Sum(num1, new ALong(num2));
        }

        /// <summary>
        /// Implements '+' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">First addend</param>
        /// <param name="num2">Second addend</param>
        /// <returns>ALong instance representing a sum of two addends.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static ALong operator +(ALong num1, string num2)
        {
            return AMath.Sum(num1, new ALong(num2));
        }

        #endregion

        # region Operator -

        /// <summary>
        /// Implements '-' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">Minuend</param>
        /// <param name="num2">Subtrahend</param>
        /// <returns>ALong instance representing a difference between minuend and subtrahend.</returns>
        public static ALong operator -(ALong num1, ALong num2)
        {
            return AMath.Sub(num1, num2);
        }

        /// <summary>
        /// Implements '-' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">Minuend</param>
        /// <param name="num2">Subtrahend</param>
        /// <returns>ALong instance representing a difference between minuend and subtrahend.</returns>
        public static ALong operator -(ALong num1, int num2)
        {
            return AMath.Sub(num1, new ALong(num2));
        }

        /// <summary>
        /// Implements '-' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">Minuend</param>
        /// <param name="num2">Subtrahend</param>
        /// <returns>ALong instance representing a difference between minuend and subtrahend.</returns>
        public static ALong operator -(ALong num1, long num2)
        {
            return AMath.Sub(num1, new ALong(num2));
        }

        /// <summary>
        /// Implements '-' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">Minuend</param>
        /// <param name="num2">Subtrahend</param>
        /// <returns>ALong instance representing a difference between minuend and subtrahend.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static ALong operator -(ALong num1, string num2)
        {
            return AMath.Sub(num1, new ALong(num2));
        }

        #endregion

        #region Operator >

        /// <summary>
        /// Implements '>' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater than num2.</returns>
        public static bool operator >(ALong num1, ALong num2)
        {
            return num1.IsGreater(num2);
        }

        /// <summary>
        /// Implements '>' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater than num2.</returns>
        public static bool operator >(ALong num1, int num2)
        {
            return num1.IsGreater(new ALong(num2));
        }

        /// <summary>
        /// Implements '>' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater than num2.</returns>
        public static bool operator >(ALong num1, long num2)
        {
            return num1.IsGreater(new ALong(num2));
        }

        /// <summary>
        /// Implements '>' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater than num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static bool operator >(ALong num1, string num2)
        {
            return num1.IsGreater(new ALong(num2));
        }

        #endregion

        #region Operator <

        /// <summary>
        /// Implements '<' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less than num2.</returns>
        public static bool operator <(ALong num1, ALong num2)
        {
            return num1.IsLess(num2);
        }

        /// <summary>
        /// Implements '<' with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less than num2.</returns>
        public static bool operator <(ALong num1, int num2)
        {
            return num1.IsLess(new ALong(num2));
        }

        /// <summary>
        /// Implements '<' with ALong instance and long value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less than num2.</returns>
        public static bool operator <(ALong num1, long num2)
        {
            return num1.IsLess(new ALong(num2));
        }

        /// <summary>
        /// Implements '<' with ALong instance and string value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less than num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static bool operator <(ALong num1, string num2)
        {
            return num1.IsLess(new ALong(num2));
        }

        #endregion

        #region Operator ==

        /// <summary>
        /// Implements '==' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Equality indication of num1 and num2.</returns>
        public static bool operator ==(ALong num1, ALong num2)
        {
            return num1.Equals(num2);
        }

        /// <summary>
        /// Implements '==' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Equality indication of num1 and num2.</returns>
        public static bool operator ==(ALong num1, int num2)
        {
            return num1.Equals(new ALong(num2));
        }

        /// <summary>
        /// Implements '==' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Equality indication of num1 and num2.</returns>
        public static bool operator ==(ALong num1, long num2)
        {
            return num1.Equals(new ALong(num2));
        }

        /// <summary>
        /// Implements '==' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Equality indication of num1 and num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static bool operator ==(ALong num1, string num2)
        {
            return num1.Equals(new ALong(num2));
        }

        #endregion

        #region Operator !=

        /// <summary>
        /// Implements '!=' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Inequality indication of num1 and num2.</returns>
        public static bool operator !=(ALong num1, ALong num2)
        {
            return !num1.Equals(num2);
        }

        /// <summary>
        /// Implements '!=' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Inequality indication of num1 and num2.</returns>
        public static bool operator !=(ALong num1, int num2)
        {
            return !num1.Equals(new ALong(num2));
        }

        /// <summary>
        /// Implements '!=' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Inequality indication of num1 and num2.</returns>
        public static bool operator !=(ALong num1, long num2)
        {
            return !num1.Equals(new ALong(num2));
        }

        /// <summary>
        /// Implements '!=' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Inequality indication of num1 and num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static bool operator !=(ALong num1, string num2)
        {
            return !num1.Equals(new ALong(num2));
        }

        #endregion

        #region Operator >=

        /// <summary>
        /// Implements '>=' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater or equal to num2.</returns>
        public static bool operator >=(ALong num1, ALong num2)
        {
            return num1.IsGreater(num2) || num1.Equals(num2);
        }

        /// <summary>
        /// Implements '>=' with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater or equal to num2.</returns>
        public static bool operator >=(ALong num1, int num2)
        {
            var al = new ALong(num2);
            return num1.IsGreater(al) || num1.Equals(al);
        }

        /// <summary>
        /// Implements '>=' with ALong instance and long value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater or equal to num2.</returns>
        public static bool operator >=(ALong num1, long num2)
        {
            var al = new ALong(num2);
            return num1.IsGreater(al) || num1.Equals(al);
        }

        /// <summary>
        /// Implements '>=' with ALong instance and string value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is greater or equal to num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static bool operator >=(ALong num1, string num2)
        {
            var al = new ALong(num2);
            return num1.IsGreater(al) || num1.Equals(al);
        }

        #endregion

        #region Operator <=

        /// <summary>
        /// Implements '<=' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less or equal to num2.</returns>
        public static bool operator <=(ALong num1, ALong num2)
        {
            return num1.IsLess(num2) || num1.Equals(num2);
        }

        /// <summary>
        /// Implements '<=' with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less or equal to num2.</returns>
        public static bool operator <=(ALong num1, int num2)
        {
            var al = new ALong(num2);
            return num1.IsLess(al) || num1.Equals(al);
        }

        /// <summary>
        /// Implements '<=' with ALong instance and long value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less or equal to num2.</returns>
        public static bool operator <=(ALong num1, long num2)
        {
            var al = new ALong(num2);
            return num1.IsLess(al) || num1.Equals(al);
        }

        /// <summary>
        /// Implements '<=' with ALong instance and string value.
        /// </summary>
        /// <param name="num1">First comparable</param>
        /// <param name="num2">Second comparable</param>
        /// <returns>Indication whether num1 is less or equal to num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static bool operator <=(ALong num1, string num2)
        {
            var al = new ALong(num2);
            return num1.IsLess(al) || num1.Equals(al);
        }

        #endregion

        #region Operator *

        /// <summary>
        /// Implements '*' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">Multiplicand</param>
        /// <param name="num2">Multiplier</param>
        /// <returns>Product of num1 and num2.</returns>
        public static ALong operator *(ALong num1, ALong num2)
        {
            return AMath.Mul(num1, num2);
        }

        /// <summary>
        /// Implements '*' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">Multiplicand</param>
        /// <param name="num2">Multiplier</param>
        /// <returns>Product of num1 and num2.</returns>
        public static ALong operator *(ALong num1, int num2)
        {
            return AMath.Mul(num1, new ALong(num2));
        }

        /// <summary>
        /// Implements '*' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">Multiplicand</param>
        /// <param name="num2">Multiplier</param>
        /// <returns>Product of num1 and num2.</returns>
        public static ALong operator *(ALong num1, long num2)
        {
            return AMath.Mul(num1, new ALong(num2));
        }

        /// <summary>
        /// Implements '*' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">Multiplicand</param>
        /// <param name="num2">Multiplier</param>
        /// <returns>Product of num1 and num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static ALong operator *(ALong num1, string num2)
        {
            return AMath.Mul(num1, new ALong(num2));
        }

        #endregion

        #region Operator /

        /// <summary>
        /// Implements '/' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Quotient of num1 and num2.</returns>
        public static ALong operator /(ALong num1, int num2)
        {
            var res = AMath.DivInt(num1, num2);
            return res.Item1;
        }

        /// <summary>
        /// Implements '/' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Quotient of num1 and num2.</returns>
        public static ALong operator /(ALong num1, ALong num2)
        {
            var res = AMath.Divide(num1, num2);
            return res.Item1;
        }

        /// <summary>
        /// Implements '/' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Quotient of num1 and num2.</returns>
        public static ALong operator /(ALong num1, long num2)
        {
            var res = AMath.Divide(num1, new ALong(num2));
            return res.Item1;
        }

        /// <summary>
        /// Implements '/' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Quotient of num1 and num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static ALong operator /(ALong num1, string num2)
        {
            var res = AMath.Divide(num1, new ALong(num2));
            return res.Item1;
        }

        #endregion

        #region Operator %

        /// <summary>
        /// Implements '%' operation with two ALong instances.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Remainder of division of num1 and num2.</returns>
        public static ALong operator %(ALong num1, int num2)
        {
            var res = AMath.DivInt(num1, num2);
            return res.Item2;
        }

        /// <summary>
        /// Implements '%' operation with ALong instance and integer value.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Remainder of division of num1 and num2.</returns>
        public static ALong operator %(ALong num1, ALong num2)
        {
            var res = AMath.Divide(num1, num2);
            return res.Item2;
        }

        /// <summary>
        /// Implements '%' operation with ALong instance and long value.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Remainder of division of num1 and num2.</returns>
        public static ALong operator %(ALong num1, long num2)
        {
            var res = AMath.Divide(num1, new ALong(num2));
            return res.Item2;
        }

        /// <summary>
        /// Implements '%' operation with ALong instance and string value.
        /// </summary>
        /// <param name="num1">Divident</param>
        /// <param name="num2">Divisor</param>
        /// <returns>Remainder of division of num1 and num2.</returns>
        /// <exception cref="FormatException">Thrown when num2 contains other charachters than '-' and '0'..'9']</exception>
        public static ALong operator %(ALong num1, string num2)
        {
            var res = AMath.Divide(num1, new ALong(num2));
            return res.Item2;
        }

        #endregion

        /// <summary>
        /// Sets the ALong instance negative state.
        /// </summary>
        /// <param name="isNegative">Negative indicator to be set.</param>
        /// <returns>A reference to the ALong instance (this).</returns>
        public ALong SetNegative(bool isNegative) { IsNegative = isNegative; return this; }

        /// <summary>
        /// Sets the ALong instance to its absolute value.
        /// </summary>
        /// <returns>A reference to the ALong instance (this).</returns>
        public ALong Abs() { SetNegative(false); return this; }

        /// <summary>
        /// Indicates whether current ALong instance is greater than other.
        /// </summary>
        /// <param name="other">Another ALong instance to be compared.</param>
        /// <returns>Indication whether current ALong instance is greater than other</returns>
        public bool IsGreater(ALong other) {
            if (!_isNegative && other._isNegative) { return true; }
            if (_isNegative && !other._isNegative) { return false; }
            if (_isNegative == other._isNegative) {
                if (Length() > other.Length()) { return _isNegative ? false : true; }
                if (Length() < other.Length()) { return _isNegative ? true : false; }
                for (int i = 0; i < Length(); i++)
                {
                    if (GetNumAtIndex(i) > other.GetNumAtIndex(i)) { return _isNegative ? false : true; }
                    if (GetNumAtIndex(i) < other.GetNumAtIndex(i)) { return _isNegative ? true : false; }
                }
            }
            return false;
        }

        /// <summary>
        /// Indicates whether current ALong instance is less than other.
        /// </summary>
        /// <param name="other">Another ALong instance to be compared.</param>
        /// <returns>Indication whether current ALong instance is less than other</returns>
        public bool IsLess(ALong other) 
        {
            return !IsGreater(other) && this != other;
            //if (!_isNegative && num2._isNegative) { return false; }
            //if (_isNegative && !num2._isNegative) { return true; }
            //if (_isNegative == num2._isNegative)
            //{
            //    if (Length() > num2.Length()) { return _isNegative ? true : false; }
            //    if (Length() < num2.Length()) { return _isNegative ? false : true; }
            //    for (int i = 0; i < Length(); i++)
            //    {
            //        if (GetNumAtIndex(i) > num2.GetNumAtIndex(i)) { return _isNegative ? true : false; }
            //        if (GetNumAtIndex(i) < num2.GetNumAtIndex(i)) { return _isNegative ? false : true; }
            //    }
            //}
            //return false;
        }

        /// <summary>
        /// Gets an integer value for specific base index.
        /// Example: for base-10 number '1234', for index=3 result would be '4'.
        /// Example: for base-16 number equivalent of hexadecimal '1FFF', for index=3 result would be '15'.
        /// </summary>
        /// <param name="index">base index</param>
        /// <returns>Integer value of the given base index.</returns>
        public int GetNumAtIndex(int index) 
        {
            if (index > _num.Length - 1) { return 0; }
            return _num[index].ToInt();
        }

        /// <summary>
        /// Gets an integer value for specific base index for 'reversed' representation of a ALong value.
        /// Example: for base-10 number '1234', for index=3 result would be '1'.
        /// Example: for base-16 number equivalent of hexadecimal '1FFF', for index=3 result would be '4096'.
        /// </summary>
        /// <param name="index">base index</param>
        /// <returns>Integer value of the given base index of the 'reversed' representation of ALong value.</returns>
        public int GetRNumAtIndex(int index)
        {
            if (index > RNum.Length - 1) { return 0; }
            return RNum[index].ToInt();
        }

        /// <summary>
        /// Gets the length of a value of the ALong instance.
        /// </summary>
        /// <returns>Length of the number.</returns>
        public int Length() { return _num.Length; }

        /// <summary>
        /// Multiplies ALong instance to a base in power of exp.
        /// Example: base-10 ALong(1234).MulBase(2) will result ALong value to 123400.
        /// </summary>
        /// <param name="exp">Power of base.</param>
        public void MulBase(int exp) 
        {
            for (int i = 0; i < exp; i++) { _num = _num + "0"; }
            _rnum = null; // reset
        }

        #region IEquatable

        /// <summary>
        /// Implements Equality indicator for ALong instance.
        /// </summary>
        /// <param name="other">Other instance of ALong to equate.</param>
        /// <returns>Indication of equality.</returns>
        public bool Equals(ALong other)
        {
            return _num == other._num && _isNegative == other._isNegative;
        }

        #endregion

        #region IComparable

        /// <summary>
        /// Implements Comparison indicator for ALong instance.
        /// </summary>
        /// <param name="other">Other instance of ALong to compare.</param>
        /// <returns>Indication of comparison.</returns>
        public int CompareTo(ALong other)
        {
            if (this < other) { return -1; }
            if (this > other) { return 1; }
            return 0;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Objects Equality
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Represents ALong instance as string value.
        /// </summary>
        /// <returns>String representation of the ALong value.</returns>
        public override string ToString()
        {
            return _isNegative ? '-' + _num : _num;
        }

        /// <summary>
        /// Represents ALong instance as string value ignoring the sign.
        /// </summary>
        /// <param name="unsigned">Indicate</param>
        /// <returns>String representation of the unsigned ALong value if 'unsigned'.</returns>
        public string ToString(bool unsigned)
        {
            return unsigned ? _num : ToString();
        }

        #endregion
    }
}
