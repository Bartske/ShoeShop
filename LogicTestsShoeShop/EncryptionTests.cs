
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoeShop

namespace LogicTestsShoeShop
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception), "There was no salt key given")]
        public void Test_Encrypt_NoSalt()
        {
            EncryptHandler.Encrypt("Wachtwoord", "");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "There was no text given")]
        public void Test_Encrypt_NoPassword()
        {
            EncryptHandler.Encrypt("", "SALT");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "There was no text given")]
        public void Test_Encrypt_NoValues()
        {
            EncryptHandler.Encrypt("", "");
        }


        [TestMethod]
        public void Test_Encrypt_Normal()
        {
            string encrypted = EncryptHandler.Encrypt("Password", "S81EWKB#CVV6P8PI");

            Assert.AreEqual("UuLduQbrS1pvqFom6dPP7A==", encrypted);
        }
    }
}
