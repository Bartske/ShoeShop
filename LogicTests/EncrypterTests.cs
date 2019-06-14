using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System;

namespace LogicTests
{
    [TestClass]
    public class EncrypterTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception), "There was no salt key given")]
        public void Test_Encrypt_NoSalt()
        {
            Encrypter.Encrypt("Wachtwoord", "");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "There was no text given")]
        public void Test_Encrypt_NoPassword()
        {
            Encrypter.Encrypt("", "SALT");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "There was no text given")]
        public void Test_Encrypt_NoValues()
        {
            Encrypter.Encrypt("", "");
        }


        [TestMethod]
        public void Test_Encrypt_Normal()
        {
            string encrypted = Encrypter.Encrypt("Password", "S81EWKB#CVV6P8PI");

            Assert.AreEqual("UuLduQbrS1pvqFom6dPP7A==", encrypted);
        }
    }
}
