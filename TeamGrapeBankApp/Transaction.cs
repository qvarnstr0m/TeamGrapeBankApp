using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class Transaction
    {
        //Properties
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public User FromOwner { get; set; }
        public User ToOwner { get; set; }
        public decimal Amount { get; set; }
        public bool Processed { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ProcessedTime { get; set; }

        //List of processed transactions
        internal static List<Transaction> processedTransactions = new List<Transaction>();

        //Constructor
        public Transaction(string fromAccount, string toAccount, User fromOwner, User toOwner, decimal amount, bool processed, DateTime createdTime, DateTime processedTime)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            FromOwner = fromOwner;
            ToOwner = toOwner;
            Amount = amount;
            Processed = processed;
            CreatedTime = createdTime;
            ProcessedTime = processedTime;
        }

        //Override ToString
        public override string ToString()
        {
            return $"From account: {FromAccount} (Owner: {FromOwner.Firstname} {FromOwner.Lastname})\nTo account: {ToAccount} (Owner: {ToOwner.Firstname} {ToOwner.Lastname})\nAmount: {BankAccount.RoundTwoDecimals(Amount)}\nIs processed: " +
                $"{Processed}\nCreated at: {CreatedTime}\nProcessed at: {ProcessedTime}";
        }

        //Method to process a bank account transaction
        internal static string ProcessTransaction(BankAccount fromAccount, BankAccount toAccount, User fromOwner, User toOwner, decimal amount)
        {
            string returnMessage = null;
            DateTime createdTime = DateTime.Now;
            DateTime processedTime = DateTime.Now;

            if (amount > fromAccount.Balance)
            {
                return "Not enough money on account. Press a key to return to main menu.";
            }

            //Logic to handle same and different currency transfers
            if (fromAccount.Currency == toAccount.Currency)
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                returnMessage = $"{amount} {fromAccount.Currency} transferred from account {fromAccount.AccountNumber} to account {toAccount.AccountNumber}. Press a key to return to main menu.";
            }

            else
            {
                if (fromAccount.Currency == "SEK")
                {
                    fromAccount.Balance -= amount;
                    toAccount.Balance += amount / Admin.currencyDict[toAccount.Currency];
                    returnMessage = $"{amount} {fromAccount.Currency} transferred from account {fromAccount.AccountNumber} to account {toAccount.AccountNumber} " +
                        $"at the exchange rate 1 / {Admin.currencyDict[toAccount.Currency]}. Press a key to return to main menu.";
                }
                else
                {
                    fromAccount.Balance -= amount;
                    toAccount.Balance += amount * Admin.currencyDict[fromAccount.Currency];
                    returnMessage = $"{amount} {fromAccount.Currency} transferred from account {fromAccount.AccountNumber} to account {toAccount.AccountNumber} " +
                        $"at the exchange rate 1 * {Admin.currencyDict[fromAccount.Currency]}. Press a key to return to main menu.";
                }
            }

            //Create Transaction object and store in list of processed transactions
            processedTransactions.Add(new Transaction(fromAccount.AccountNumber, toAccount.AccountNumber, fromOwner, toOwner, amount, true, createdTime, processedTime));

            return returnMessage;
        }

        //Method to list logged in customers bank account transactions
        internal static void ListTransactions(User loggedInCustomer)
        {
            Console.Clear();
            Console.WriteLine("These are your transactions:\n");
            int customerTransactions = 0;

            foreach (var item in processedTransactions)
            {
                if (item.FromOwner == loggedInCustomer || item.ToOwner == loggedInCustomer)
                {
                    Console.WriteLine($"{item}\n");
                    customerTransactions++;
                }
            }

            Console.WriteLine(customerTransactions > 0 ? "All transactions listed. Press a key to return to main menu." : "There are no transactions to list. Press a key to return to main menu.");
            //Console.WriteLine("All transactions listed. Press a key to return to main menu.");
            Console.ReadKey();
        }
    }
}
