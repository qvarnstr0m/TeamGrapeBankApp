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

        //List to hold customers savings accounts
        public static List<LoanAccount> loanAccounts = new List<LoanAccount>();

        //Method to generate loan accounts and add to list (should change to database later)
        public static void GenerateLoanAccounts()
        {
            LoanAccount Acc1 = new LoanAccount("Morgage", "1234-1234", "billgates", "SEK", 1000000m, Admin.interestDict[24]);
            LoanAccount Acc2 = new LoanAccount("Car loan", "5555-1234", "annasvensson", "SEK", 100000.12m, Admin.interestDict[24]);
            LoanAccount Acc3 = new LoanAccount("Boat loan", "5555-0000", "hermessaliba", "SEK", 5000000m, Admin.interestDict[36]);

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
            Console.Write("Enter name of your new loan account: ");
            string accountName = Console.ReadLine();

            //Get user input for how many months to pay off loan
            foreach(var item in Admin.interestDict)
            {
                Console.WriteLine($"Months to pay off: {item.Key} Interest rate: {ConvertInterestToString(item.Value)}%");
            }
            bool parseSuccess;
            int userinputKey;
            do
            {
                Console.Write("Months to pay off loan: ");
                parseSuccess = int.TryParse(Console.ReadLine(), out userinputKey);
            }
            while (!parseSuccess || !Admin.interestDict.ContainsKey(userinputKey));

            bool parseSuccessAmount = false;
            decimal userInputAmount = 0m;
            decimal maxLoanAmount = LoanLimit(loggedInCustomer);
            if (maxLoanAmount > 0)
            {
                do
                {
                    Console.Write($"Enter a valid amount to loan in SEK: (Max amount: {RoundTwoDecimals(maxLoanAmount)})");
                    parseSuccessAmount = decimal.TryParse(Console.ReadLine(), out userInputAmount);
                } while (!parseSuccessAmount || userInputAmount < 0 || userInputAmount > maxLoanAmount);
            }
            else
            {
                Console.WriteLine("You do not have enough balance to take out a loan at the moment, press a key to return to the main menu.");
                Console.ReadKey();
                Customer.CustomerMenu(loggedInCustomer);
            }


            Console.WriteLine($"\nCongratulations! You just took a loan of {userInputAmount} SEK which will cost {RoundTwoDecimals(ReturnMonthlyInterest(userinputKey, userInputAmount))} SEK per month in interest.");
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

            //Max loan amount is 5 times total balance
            totalBalance *= 5;

            //Subtract possible current loans from total balance before returning max loan value
            List<LoanAccount> LoggedInUserLoanAccount = LoanAccount.loanAccounts.FindAll(x => x.Owner == loggedInCustomer.Username);
            foreach (var item in LoggedInUserLoanAccount)
            {
                totalBalance -= item.Balance;
            }

            return totalBalance > 0 ? totalBalance : 0;
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
