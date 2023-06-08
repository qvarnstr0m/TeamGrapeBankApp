using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamGrapeBankApp;
using static System.Net.Mime.MediaTypeNames;

namespace TeamGrapeBankAppTest
{
    [TestClass]
    public class LoanAccountTest
    {
        [TestMethod]
        //Emthod to test the correct interest for given loan amount
        public void ReturnMonthlyInterest_ExpectTheCorrectInterest()
        {
            //Arrange
            int months = 36;//month to pay the loan
            decimal amount = 45000;//Given loan amount

           //Act
           //Call the method
            decimal monthlyInterest = LoanAccount.ReturnMonthlyInterest(months, amount);

            //Assert
            // The expected monthly interest for a loan amount of 45000 SEK for 36 months
            decimal expectedInterest = 225m;
            // Verify that the calculated monthly interest matches the expected interest
            Assert.AreEqual(expectedInterest, monthlyInterest);
        }

    }
}
