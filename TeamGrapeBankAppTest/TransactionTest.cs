using TeamGrapeBankApp;


namespace TeamGrapeBankAppTest
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        //method is used to test if the transfer is made in the same currency
        public void ProcessTransaction_ShouldTransferSameCurrency()
        {
            // Given a transaction from one account to another
            //Arrange
            BankAccount fromAccount = new BankAccount("From Account", "1111-2222", "Hermy", "SEK", 1000);
            BankAccount toAccount = new BankAccount("To Account", "2222-3333", "Anna", "SEK", 500);
            User fromUser = new Customer(1, "hermessaliba", "pass3", "Hermes", "Saliba", "Backgatan 26 5678 Varberg", "hermes@microsoft.se", "+46702222222", false);
            User toUser = new Customer(2, "annasvensson", "pass2", "Anna", "Svensson", "Nygatan 26 31176 Falkenberg", "anna@svensson.se", "+46703333333", false);
            decimal amount = 500; //The given amount

            //Act
            //Call the method
            string returnMessage = Transaction.ProcessTransaction(fromAccount, toAccount, fromUser, toUser, amount);

            // Assert
            //Check if returnMessage matches the expected result
            Assert.AreEqual(500, fromAccount.Balance);
            Assert.AreEqual(1000, toAccount.Balance);
            Assert.AreEqual("500 SEK transferred from account 1111-2222 to account 2222-3333. Press a key to return to main menu.", returnMessage);
        }


    }
}