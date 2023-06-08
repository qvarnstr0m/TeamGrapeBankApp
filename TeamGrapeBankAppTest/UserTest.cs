using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamGrapeBankApp;

namespace TeamGrapeBankAppTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        //Method to check valin username and pasword while logging in
        public void Login_CheckValidUsernameAndPassword()
        {
            //Arrange
            string expectedUsername = "testUsername"; //Specifies the expected username for the test user.
            string expectedPassword = "testPasword"; //Specifies the expected pasword for the test user.
                                                     //Creates a Customer object representing the test user with the expected username and password.
            User test = new Customer(6, expectedUsername, expectedPassword, "testUsername", "testPasword", "Testgatan 2 43567 Varberg", "test@hotmail.se", "+4673456098", false);

            //Act
            string actualUsername = test.Username; //Retrieves the actual username from the test user object.
            string actualPassword = test.Password; //Retrieves the actual pasword from the test user object.

            //Assert
            Assert.AreEqual(expectedUsername, actualUsername);//Checks if the expected username matches the actual username.
            Assert.AreEqual(expectedPassword, actualPassword); //Checks if the expected password matches the actual password.
        }
    }
}
