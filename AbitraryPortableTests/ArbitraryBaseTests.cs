using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArbitraryPortable;
using System.Security.Cryptography;
using System.Text;

namespace AbitraryPortableTests
{
    [TestClass]
    public class ArbitraryBaseTests
    {
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public string RandMD5()
        {
            var res = String.Empty;
            var source = new Random();
            using (MD5 md5Hash = MD5.Create())
            {
                res = GetMd5Hash(md5Hash, source.ToString());
            }
            return res;
        }

        [TestMethod]
        public void ArbitraryBaseConversionTest()
        {
            //var res = String.Empty;
            //using (MD5 md5Hash = MD5.Create())
            //{
            //    res = GetMd5Hash(md5Hash, "ArbitraryPortable").ToLower();
            //    var a = res.FromArbitraryBase(16);
            //    var b = a.ToArbitraryBase(62);
            //}

            for (int i = 0; i < 100; i++)
            {
                var md5 = RandMD5();
                var a = md5.FromArbitraryBase(16);
                var b = a.ToArbitraryBase(62);
                var c = b.FromArbitraryBase(62);
                var d = c.ToArbitraryBase(16);

                Assert.IsTrue(md5 == d);
            }
        }

        [TestMethod]
        public void ArbitraryBaseBinaryTest() 
        {
            var a = "101010101".FromArbitraryBase(2);
            var b = a.ToArbitraryBase(10);
            Assert.IsTrue(a == 341);
            Assert.IsTrue(b == "341");

            var c = b.FromArbitraryBase(10);
            var d = c.ToArbitraryBase(2);

            Assert.IsTrue(c == 341);
            Assert.IsTrue(d == "101010101");
        }
    }
}
