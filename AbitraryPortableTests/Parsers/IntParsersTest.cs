using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArbitraryPortable.Parsers;

namespace AbitraryPortableTests.Parsers
{
    [TestClass]
    public class IntParsersTest
    {
        [TestMethod]
        public void CharToIntTest()
        {
            Assert.AreEqual(0, '0'.ToInt());
            Assert.AreEqual(1, '1'.ToInt());
            Assert.AreEqual(2, '2'.ToInt());
            Assert.AreEqual(3, '3'.ToInt());
            Assert.AreEqual(4, '4'.ToInt());
            Assert.AreEqual(5, '5'.ToInt());
            Assert.AreEqual(6, '6'.ToInt());
            Assert.AreEqual(7, '7'.ToInt());
            Assert.AreEqual(8, '8'.ToInt());
            Assert.AreEqual(9, '9'.ToInt());

            try
            {
                'a'.ToInt();
                Assert.Fail();
            } catch (FormatException) {
            } catch (Exception) { Assert.Fail(); }
        }

        [TestMethod]
        public void StringToIntTest()
        {
            Assert.AreEqual(0, "0".ToInt());
            Assert.AreEqual(-0, "-0".ToInt());
            Assert.AreEqual(1, "1".ToInt());
            Assert.AreEqual(12, "12".ToInt());
            Assert.AreEqual(125, "125".ToInt());
            Assert.AreEqual(-125, "-125".ToInt());
            Assert.AreEqual(345763747, "345763747".ToInt());
            Assert.AreEqual(987445654, "987445654".ToInt());
            Assert.AreEqual(2147483647, "2147483647".ToInt());
            Assert.AreEqual(-2147483648, "-2147483648".ToInt());
            try
            {
                "--125".ToInt();
                Assert.Fail();
            }
            catch (FormatException) {}
            catch (Exception)  {  Assert.Fail(); }
        }
    }
}
