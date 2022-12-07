using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class LoanAccount : BankAccount
    {
        public decimal Interest { get; set; }
        public decimal Limit { get; set; }

        public LoanAccount(string accountName, string accountNumber, string owner, string currency, decimal balance, decimal interest, decimal limit) : base(accountName, accountNumber, owner, currency, balance)
        {
            Interest = interest;
            Limit = limit;
        }

        public override string ToString()
        {
            return $"AccountName: {AccountName}\nAccountnumber: {AccountNumber}\nBalance: -{RoundTwoDecimals(Balance)}{Currency}\n";
        }

        public static List<LoanAccount> loanAccounts = new List<LoanAccount>();

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
            do
            {
                Console.Write($"Enter a valid amount to loan in SEK: (Max amount: {RoundTwoDecimals(LoanLimit(loggedInCustomer))})");
                parseSuccessAmount = decimal.TryParse(Console.ReadLine(), out userInputAmount);
            } while (!parseSuccessAmount || userInputAmount < 0);

            Console.WriteLine($"Congratulations! You just took a loan of {userInputAmount} SEK which will cost {RoundTwoDecimals(ReturnMonthlyInterest(userinputKey, userInputAmount))} SEK per month.");
            Console.ReadKey();
            string accountNumber = GenerateAccountNumber();

            loanAccounts.Add(new LoanAccount(accountName, accountNumber, loggedInCustomer.Username, "SEK", userInputAmount, Admin.interestDict[userinputKey], LoanLimit(loggedInCustomer)));
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
    }
}
