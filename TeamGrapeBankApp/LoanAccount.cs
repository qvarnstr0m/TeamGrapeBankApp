using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class LoanAccount : BankAccount
    {
        //Properties
        public decimal Interest { get; set; }

        //List to hold loan accounts
        public static List<LoanAccount> loanAccounts = new List<LoanAccount>();

        //Constructor
        public LoanAccount(string accountName, string accountNumber, string owner, string currency, decimal balance, decimal interest) : base(accountName, accountNumber, owner, currency, balance)
        {
            Interest = interest;
        }

        //Overide ToString method
        public override string ToString()
        {
            return $"AccountName: {AccountName}\nAccountnumber: {AccountNumber}\nBalance: -{RoundTwoDecimals(Balance)}{Currency}\n";
        }

        public static void GenerateLoanAccounts()
        {
            //Hardcode loan accounts and add to list (should change to database later)
            LoanAccount Acc1 = new LoanAccount("Morgage", "1234-1234", "billgates", "SEK", 1350000.345m, Admin.interestDict[24]);
            LoanAccount Acc2 = new LoanAccount("Car loan", "5555-1234", "annasvensson", "SEK", 245000, Admin.interestDict[36]);
            LoanAccount Acc3 = new LoanAccount("Loan to party", "5555-0000", "hermessaliba", "SEK", 7000.43m, Admin.interestDict[24]);

            loanAccounts.Add(Acc1);
            loanAccounts.Add(Acc2);
            loanAccounts.Add(Acc3);
        }

        //Method for customer to take out a loan
        internal static void TakeLoan(User loggedInCustomer)
        {
            Console.Clear();

            Console.WriteLine("Open a loan account\n");

            //Get user input for name of account
            Console.WriteLine("Enter name of your new loan account");
            string accountName = Console.ReadLine();

            //Get user input for how many months to pay off loan
            foreach(var item in Admin.interestDict)
            {
                Console.WriteLine($"Months to pay off: {item.Key} Interest rate: {item.Value}");
            }
            bool parseSuccess;
            int userinputKey;
            do
            {
                Console.WriteLine("How many months would you like to pay off loan");
                parseSuccess = int.TryParse(Console.ReadLine(), out userinputKey);
            }
            while (!parseSuccess || !Admin.interestDict.ContainsKey(userinputKey));

            bool parseSuccessAmount = false;
            decimal userInputAmount;
            decimal maxLoanAmount = LoanLimit(loggedInCustomer);
            do
            {
                Console.Write($"Enter a valid amount to loan in SEK: (Max amount: {RoundTwoDecimals(LoanLimit(loggedInCustomer))})");
                parseSuccessAmount = decimal.TryParse(Console.ReadLine(), out userInputAmount);
            } while (!parseSuccessAmount || userInputAmount < 0 || userInputAmount > maxLoanAmount);

            Console.WriteLine($"Congratulations! You just took a loan of {userInputAmount} SEK which will cost {RoundTwoDecimals(ReturnMonthlyInterest(userinputKey, userInputAmount))} SEK per month.");
            Console.ReadKey();
            string accountNumber = GenerateAccountNumber();

            loanAccounts.Add(new LoanAccount(accountName, accountNumber, loggedInCustomer.Username, "SEK", userInputAmount, Admin.interestDict[userinputKey]));
        }

        internal static decimal LoanLimit(User loggedInCustomer)
        {
            decimal totalBalance = 0;
            List<BankAccount> LoggedInUserBankAccounts = bankAccounts.FindAll(x => x.Owner == loggedInCustomer.Username);

            foreach (var item in LoggedInUserBankAccounts)
            {
                decimal currentBalance = 0;
                if (item.Currency != "SEK")
                {
                    currentBalance = item.Balance * Admin.currencyDict[item.Currency];
                }
                else
                {
                    currentBalance = item.Balance;
                }
                totalBalance += currentBalance;
            }

            List<SavingsAccount> LoggedInUserSavingsAccount = SavingsAccount.savingsAccounts.FindAll(x => x.Owner == loggedInCustomer.Username);

            foreach (var item in LoggedInUserSavingsAccount)
            {
                decimal currentBalance = 0;
                if (item.Currency != "SEK")
                {
                    currentBalance = item.Balance * Admin.currencyDict[item.Currency];
                }
                else
                {
                    currentBalance = item.Balance;
                }
                totalBalance += currentBalance;
            }

            List<LoanAccount> LoggedInUserLoanAccount = LoanAccount.loanAccounts.FindAll(x => x.Owner == loggedInCustomer.Username);

            foreach (var item in LoggedInUserLoanAccount)
            {
                totalBalance -= item.Balance;
            }

            return totalBalance;
        }

        internal static decimal ReturnMonthlyInterest(int Months, decimal Amount)
        {
            return (Amount * Admin.interestDict[Months] - Amount) / 12;
        }

        //Method to once a month update each loan account in list with new balance
        internal static void UpdateLoanAccounts()
        {
            if (DateTime.Today.Day == 8)
            {
                foreach (LoanAccount item in loanAccounts)
                {
                    item.Balance = item.Balance * item.Interest;
                }
            }
        }
    }
}
