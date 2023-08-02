using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace UnitTestLoginPage
{
    [TestClass]
    public class UnitTest1
    {
        UserData userData = new UserData();
        string inputName, inputPassword;
        //int actualUserId;
        [TestMethod]
        public void TestMethod1()
        {
            inputName = "FanfanYuan";
            inputPassword = "fy1234";

            Boolean expectedReturn = true;
            int expectedUserId = 1;

            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            int actualUserId = userData.UserID;

            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);
        } 

        [TestMethod]
        public void TestMethod2()
        {
            inputName = "FanfanYuan";
            inputPassword = "1234";

            Boolean expectedReturn = false;
            int expectedUserId = 0;

            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            int actualUserId = userData.UserID;

            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);
        }

        [TestMethod]
        public void TestMethod3()
        {
            inputName = "FanfanYuan";
            inputPassword = "aw1234";
            Boolean expectedReturn = false;
            int expectedUserId = -1;

            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            int actualUserId = userData.UserID;

            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);
        }

        [TestMethod]
        public void TestMethod4()
        {
            inputName = "FanfanYuan";
            inputPassword = "123456";
            Boolean expectedReturn = false;
            int expectedUserId = 0;

            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            int actualUserId = userData.UserID;

            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);
        }

        [TestMethod]
        public void TestMethod5()
        {
            inputName = "";
            inputPassword = "fy1234";
            Boolean expectedReturn = false;
            int expectedUserId = 0;

            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            int actualUserId = userData.UserID;

            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);

        }
    }
}
