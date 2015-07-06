using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArbitraryPortable;
using System.Numerics;

namespace AbitraryPortableTests
{
    [TestClass]
    public class ALongTests
    {
        [TestMethod]
        public void ALongObjectTests() 
        {
            Assert.IsTrue(new ALong("0001").Equals(new ALong("1")));
            Assert.IsTrue(new ALong("0001").Equals(new ALong("01")));
            Assert.IsTrue(new ALong("1").Equals(new ALong(1)));
            Assert.IsTrue(new ALong("12345678901234").Equals(new ALong(12345678901234)));
            Assert.IsTrue(new ALong("-000001").Equals(new ALong("-1")));
            Assert.IsTrue(new ALong("-000001").Equals(new ALong(-1)));
        }

        [TestMethod]
        public void ALongSumTest()
        {
            Assert.AreEqual("1248", (new ALong("00001239") + new ALong("000009")).ToString());
            Assert.AreEqual("10", (new ALong("1") + new ALong("9")).ToString());
            Assert.AreEqual("180", (new ALong("90") + new ALong("90")).ToString());
            Assert.AreEqual("100000000000000000000000000000000000000000000005", (new ALong("100000000000000000000000000000000000000000000000") + new ALong("5")).ToString());

            var a = new ALong("10");

            // Add string (aware of number pasing errors)
            a += "20";
            Assert.AreEqual("30", a.ToString());

            // Add int
            a += 10;
            Assert.AreEqual("40", a.ToString());

            // Add long
            a += 10L;
            Assert.AreEqual("50", a.ToString());

            Assert.AreEqual("10000", (new ALong("1") + new ALong("9999")).ToString());

            Assert.AreEqual("-1", (new ALong("0") + new ALong("-1")).ToString());
            Assert.AreEqual("-999", (new ALong("1") + new ALong("-1000")).ToString());

            a += -60;
            Assert.AreEqual("-10", a.ToString());
        }

        [TestMethod]
        public void ALongComparerTest()
        {
            Assert.IsTrue(new ALong("0") > new ALong("-1"));
            Assert.IsTrue(new ALong("-1") < new ALong("0"));
            Assert.IsTrue(new ALong("1") > new ALong("0"));
            Assert.IsTrue(new ALong("1") > new ALong("-1"));
            Assert.IsTrue(new ALong("10") > new ALong("01"));
            Assert.IsTrue(new ALong("10") > new ALong("-01"));
            Assert.IsTrue(new ALong("10") > new ALong("-010"));
            Assert.IsTrue(new ALong("00001239") > new ALong("000009"));

            Assert.IsTrue(new ALong("0") > -1);
            Assert.IsTrue(new ALong("-1") < 0);
            Assert.IsTrue(new ALong("1") > 0);
            Assert.IsTrue(new ALong("1") > -1);
            Assert.IsTrue(new ALong("10") > 01);
            Assert.IsTrue(new ALong("10") > -01);
            Assert.IsTrue(new ALong("10") > -010);
            Assert.IsTrue(new ALong("00001239") > 000009);

            Assert.IsTrue(new ALong("0") > -1L);
            Assert.IsTrue(new ALong("-1") < 0L);
            Assert.IsTrue(new ALong("1") > 0L);
            Assert.IsTrue(new ALong("1") > -1L);
            Assert.IsTrue(new ALong("10") > 01L);
            Assert.IsTrue(new ALong("10") > -01L);
            Assert.IsTrue(new ALong("10") > -010L);
            Assert.IsTrue(new ALong("00001239") > 000009L);

            Assert.IsTrue(new ALong("0") > "-1");
            Assert.IsTrue(new ALong("-1") < "0");
            Assert.IsTrue(new ALong("1") > "0");
            Assert.IsTrue(new ALong("1") > "-1");
            Assert.IsTrue(new ALong("10") > "01");
            Assert.IsTrue(new ALong("10") > "01");
            Assert.IsTrue(new ALong("10") > "-010");
            Assert.IsTrue(new ALong("00001239") > "000009");
        }

        [TestMethod]
        public void ALongAbsTest() 
        {
            Assert.IsTrue(new ALong("-1").Abs() == new ALong(1));
            Assert.IsTrue(new ALong("1").Abs() == new ALong(1));
            Assert.IsTrue(new ALong("-0").Abs() == new ALong(0));
            Assert.IsTrue(new ALong("0").Abs() == new ALong(0));
        }

        [TestMethod]
        public void ALongMaxMinTest() 
        {
            var a = new ALong("1");
            var b = new ALong("2");
            var c = new ALong("-5");
            var d = new ALong("-3");

            Assert.AreEqual(b, AMath.Max(a, b));
            Assert.AreEqual(b, AMath.Max(c, b));
            Assert.AreEqual(a, AMath.Min(a, b));
            Assert.AreEqual(c, AMath.Min(c, a));
            Assert.AreEqual(c, AMath.Min(c, b));

            Assert.AreEqual(c, AMath.Min(c, d));
            Assert.AreEqual(d, AMath.Max(c, d));
        }

        [TestMethod]
        public void ALongSubTest()
        {
            Assert.AreEqual("9886", AMath.Sub(new ALong(10509), new ALong(623)).ToString());
            Assert.AreEqual("999", AMath.Sub(new ALong(1000), new ALong(1)).ToString());
            Assert.AreEqual("1000", AMath.Sub(new ALong(999), new ALong(-1)).ToString());
            Assert.AreEqual("-1000", AMath.Sub(new ALong(-1), new ALong(999)).ToString());
            Assert.AreEqual("998", AMath.Sub(new ALong(-1), new ALong(-999)).ToString());
            Assert.AreEqual("2", AMath.Sub(new ALong("1"), new ALong(-1)).ToString());
            Assert.AreEqual("-1", AMath.Sub(new ALong("99"), new ALong(100)).ToString());

            var a = new ALong(10);
            a -= 2;
            Assert.AreEqual("8", a.ToString());

            a -= 8;
            Assert.AreEqual("0", a.ToString());

            a += 10;
            a -= 15;
            Assert.AreEqual("-5", a.ToString());

            a -= "100000000000000000000000000000000000000000000005";
            Assert.AreEqual("-100000000000000000000000000000000000000000000010", a.ToString());

            a += "11";
            Assert.AreEqual("-99999999999999999999999999999999999999999999999", a.ToString());
            Assert.IsTrue(a == "-99999999999999999999999999999999999999999999999");
            Assert.IsFalse(a > "-99999999999999999999999999999999999999999999999");
            Assert.IsTrue(a >= "-99999999999999999999999999999999999999999999999");
        }

        [TestMethod]
        public void MulTenTest()
        {
            var a = new ALong(10);
            a.MulBase(5);
            a += 10;

            Assert.IsTrue(a == "1000010");
        }

        [TestMethod]
        public void MulTest() 
        {
            // basics
            Assert.IsTrue(new ALong() * new ALong() == 0);
            Assert.IsTrue(new ALong() * new ALong() == "0");
            Assert.IsTrue(new ALong(123) * new ALong() == "0");
            Assert.IsTrue(new ALong(-123) * new ALong() == "0");
            Assert.IsTrue(new ALong() * new ALong(123) == "0");
            Assert.IsTrue(new ALong() * new ALong(-123) == "0");

            Assert.IsTrue(new ALong(1) * new ALong(1) == "1");
            Assert.IsTrue(new ALong(1) * new ALong(-1) == "-1");
            Assert.IsTrue(new ALong(-1) * new ALong(-1) == "1");
            Assert.IsTrue(new ALong(-1) * new ALong(1) == "-1");

            Assert.IsTrue(new ALong(123) * new ALong(999) == "122877");
            Assert.IsTrue(new ALong(-123) * new ALong(999) == "-122877");
            Assert.IsTrue(new ALong(123) * new ALong(-999) == "-122877");
            Assert.IsTrue(new ALong(-123) * new ALong(-999) == "122877");

            
            var a = BigInteger.Parse("1010101010101010101010101010101010101010101010101010");
            var b = BigInteger.Parse("202020202020202020202020202020202020202020202020202020202020");
            var c = a * b;
            // 204060810121416182022242628303234363840424446485052525252525048464442403836343230282624222018161412100806040200
            Assert.IsTrue(new ALong("1010101010101010101010101010101010101010101010101010") * new ALong("202020202020202020202020202020202020202020202020202020202020") == "204060810121416182022242628303234363840424446485052525252525048464442403836343230282624222018161412100806040200");
            Assert.IsTrue(new ALong("1010101010101010101010101010101010101010101010101010") * new ALong("202020202020202020202020202020202020202020202020202020202020") == c.ToString());

            var aa = new ALong("1010101010101010101010101010101010101010101010101010");
            var bb = new ALong("202020202020202020202020202020202020202020202020202020202020");
            var cc = aa * bb;

            Assert.IsTrue(cc == "204060810121416182022242628303234363840424446485052525252525048464442403836343230282624222018161412100806040200");
        }

        [TestMethod]
        public void SmallPowTest()
        {
            Assert.AreEqual(AMath.Pow(10, 2), 100);
            Assert.AreEqual(AMath.Pow(2, 16), 65536d);
        }

        public void AssertDivInt(string num1, int num2) 
        {
            var a = new ALong(num1);
            var b = AMath.DivInt(a, num2);
            //var b = ALong.Divide(a, new ALong(num2));

            var aa = BigInteger.Parse(num1);
            var bb = new Tuple<BigInteger, BigInteger>(aa / num2, aa % num2);

            Assert.IsTrue(b.Item1 == bb.Item1.ToString());
            Assert.IsTrue(b.Item2 == bb.Item2.ToString());
        }

        [TestMethod]
        public void DivIntTest() 
        {
            AssertDivInt("55", 5);
            AssertDivInt("555", 11);
            AssertDivInt("5555", 55);

            // sign tests
            AssertDivInt("2903845092834590820", -55);
            AssertDivInt("-2903845092834590820", 55);
            AssertDivInt("-2903845092834590820", -55);
            AssertDivInt("2903845092834590820", 55);

            AssertDivInt("010982340981902348", 55);
            AssertDivInt("-1345124385091234852345", 11);
            AssertDivInt("2345243524352435257476856783542346534574678473455624564568756783456234563457643576", Int32.MaxValue);

            AssertDivInt("0", -55);
            AssertDivInt("7", 62);
        }

        public void AssertDivide(string num1, string num2) 
        {
            var a = new ALong(num1);
            var b = new ALong(num2);
            var d = AMath.Divide(a, b);

            var aa = BigInteger.Parse(num1);
            var bb = BigInteger.Parse(num2);
            var dd = aa / bb;
            var rr = aa % bb;

            Assert.IsTrue(d.Item1 == dd.ToString());
            Assert.IsTrue(d.Item2 == rr.ToString());
        }

        [TestMethod]
        public void DivideTest() 
        {
            AssertDivide("132121923409128340918230491802394810", "112394810932481093284091");
            AssertDivide("-132121923409128340918230491802394810", "112394810932481093284091");
            AssertDivide("132121923409128340918230491802394810", "-112394810932481093284091");
            AssertDivide("-132121923409128340918230491802394810", "-112394810932481093284091");
            AssertDivide("132121923409128340918230491802394810", "132121923409128340918230491802394810");
            AssertDivide("-132121923409128340918230491802394810", "132121923409128340918230491802394810");
            AssertDivide("132121923409128340918230491802394810", "-132121923409128340918230491802394810");
            AssertDivide("-132121923409128340918230491802394810", "-132121923409128340918230491802394810");
            AssertDivide("0", "-132121923409128340918230491802394810");
            AssertDivide("132121923409128340918230491802394810214352346243511", "1321219234091283409182304918023948101234132413243");

            Assert.IsTrue(
                new ALong("132121923409128340918230491802394810") % "112394810932481093284091" ==
                (BigInteger.Parse("132121923409128340918230491802394810") % BigInteger.Parse("112394810932481093284091")).ToString()
            );

            Assert.IsTrue(
                new ALong("132121923409128340918230491802394810") / "112394810932481093284091" ==
                (BigInteger.Parse("132121923409128340918230491802394810") / BigInteger.Parse("112394810932481093284091")).ToString()
            );
        }

        [TestMethod]
        public void BigPowTest()
        {
            var a = BigInteger.Parse("132121923409128340918230491802394810");
            var aa = BigInteger.Pow(a, 50);

            var b = new ALong("132121923409128340918230491802394810");
            var bb = AMath.Pow(b, 50);

            Assert.IsTrue(bb == aa.ToString());
        }

        [TestMethod]
        public void Examples() 
        {
            var a = new ALong(10);	    // Initialize with 10
	        var b = new ALong("10");	// Initialize with 10
	
	        // Sum
	        a += 20; 				    // 30
	        a += 20L;				    // 50
	        a += "20";				    // 70
	        a += new ALong("20");	    // 90
	        a -= b;						// 80
	        a += b;						// 90
	        a -= -10;				    // 100
	
	        // Comparing
	        var cmp = a < b;	        // false
	        cmp = a <= b;			    // false
	        cmp = a > b;			    // true
	        cmp = a > 200;				// false
            cmp = a >= "100";			// true

	        // Multiplication
	        a *= 10;					// 1000
	        a *= b;						// 10000
	        a = a * b;					// 100000
	        a = a * "-10";              // -1000000
	
	        // Division
            a /= -1000;					// 1000
	        a /= b;						// 100
	
	        // Remainder
            var c = a % (b + 3);        // 9
            c = a % 13;                 // 9
            c = b % 5;                  // 0
            c = b % 3;                  // 1
	
	        // Power
            c = AMath.Pow(a, b);        // 100^10 = 100000000000000000000
            
            // Base Conversion
            string d;
            d = "c367b3eb9df3bd5bdca9a3516af2d4da".FromArbitraryBase(16).ToArbitraryBase(62);   // "5WITyx9Hj07ZJvDzZcZHrI"
            d = d.FromArbitraryBase(62).ToArbitraryBase(16);                                    // "c367b3eb9df3bd5bdca9a3516af2d4da"
        }
    }
}
