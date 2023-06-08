﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TeamGrapeBankApp
{
    public class BankAccount
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Owner  { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }

        public static List<BankAccount> bankAccounts = new List<BankAccount>();
        public BankAccount(string accountName, string accountNumber, string owner, string currency, decimal balance)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            Owner = owner;
            Currency = currency;
            Balance = balance;
        }

        public static void GenerateBankAccounts()
        {
            //Hardcode some bankaccounts and adds them to list (should change to database later)
            BankAccount Acc1 = new BankAccount("Salary Account","4444-5555", "billgates", "SEK", 1000000.345m);
            BankAccount Acc2 = new BankAccount("Bills","4444-3577", "billgates", "SEK", 50043);
            BankAccount Acc3 = new BankAccount("For Emergency","4444-2644", "billgates", "SEK", 3205);

            BankAccount Acc4 = new BankAccount("Salary Account","5555-2644", "annasvensson", "SEK", 45000);
            BankAccount Acc5 = new BankAccount("Bills","5555-1533", "annasvensson", "SEK", 2300);

            BankAccount Acc6 = new BankAccount("Salary Account","6666-7533", "hermessaliba", "SEK", 74442.43m);
            BankAccount Acc7 = new BankAccount("Bills","6666-2685", "hermessaliba", "USD", 25114.79m);

            bankAccounts.Add(Acc1);
            bankAccounts.Add(Acc2);
            bankAccounts.Add(Acc3);
            bankAccounts.Add(Acc4);
            bankAccounts.Add(Acc5);
            bankAccounts.Add(Acc6);
            bankAccounts.Add(Acc7);
        }

        public override string ToString()
        {
            return $"AccountName: {AccountName}\nAccountnumber: {AccountNumber}\nBalance: {RoundTwoDecimals(Balance)}{Currency}\n";
        }

        public static void ListBankaccounts(string username)
        {
            Console.Clear();
            Console.WriteLine("Bankaccounts");
            //Creates a new list and adds bankaccounts owned by loggedInUser
            List<BankAccount> userBankaccount = bankAccounts.FindAll(x => x.Owner == username);

            foreach(BankAccount b in userBankaccount)
            {
                Console.WriteLine(b);
            }

            List<SavingsAccount> userSavingsAccount = SavingsAccount.savingsAccounts.FindAll(x => x.Owner == username);
            if(userSavingsAccount.Count() > 0)
            {
                Console.WriteLine("Savingsaccounts");
                foreach(SavingsAccount h in userSavingsAccount)
                {
                    Console.WriteLine(h);
                }
            }

            List<LoanAccount> userLoanAccount = LoanAccount.loanAccounts.FindAll(x => x.Owner == username);
            if (userLoanAccount.Count() > 0)
            {
                Console.WriteLine("Loan accounts");
                foreach (LoanAccount j in userLoanAccount)
                {
                    Console.WriteLine(j);
                }
            }
            Console.WriteLine("All accounts listed, please press a key to return to menu");
            Console.ReadKey();
        }

        //Method to open a new account
        public static void OpenNewAccount(User loggedInCustomer)
        {
            Console.Clear();

            Console.WriteLine("Open a new bank account\n");

            string accountNumber;
            accountNumber = GenerateAccountNumber();

            Console.Write("Enter account name: ");
            string accountName = Console.ReadLine();

            Console.WriteLine("\nThe available currencies are:");
            foreach (var item in Admin.currencyDict)
            {
                Console.WriteLine(item.Key);
            }
            string userInputCurrency;
            do
            {
                Console.Write("Enter a valid currency: ");
                userInputCurrency = Console.ReadLine().ToUpper();
            } while (!Admin.currencyDict.ContainsKey(userInputCurrency));

            bool parseSuccess;
            decimal balance;
            do
            {
                Console.Write("Enter amount to deposit: ");
                parseSuccess = decimal.TryParse(Console.ReadLine(), out balance);
            } while (!parseSuccess || balance < 0);
          
            
            BankAccount newBankAccount = new BankAccount(accountName, accountNumber, loggedInCustomer.Username, userInputCurrency, balance);
            BankAccount.bankAccounts.Add(newBankAccount);
            Console.WriteLine("Account succesfully created: " + "\n" + newBankAccount + "\n");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }

        public static string GenerateAccountNumber()
        {
            string newAccountNumber;
            do
            {
                Random ranAccount = new Random();
                Random ranAccount2 = new Random();
                int randAccount = ranAccount.Next(9999);
                int randAccount2 = ranAccount2.Next(9999);
                newAccountNumber = randAccount.ToString() + "-" + randAccount2.ToString();
            }
            while (BankAccount.bankAccounts.Any(x => x.AccountNumber == newAccountNumber) && SavingsAccount.savingsAccounts.Any(x => x.AccountNumber == newAccountNumber));

            return newAccountNumber;
        }

        public static void InternalTransaction(string username)
        {
            //Write out the accounts
            Console.Clear();
            Console.WriteLine("Bankaccounts");
            List<BankAccount> userBankaccount = bankAccounts.FindAll(x => x.Owner == username);
            


            for(int i=0; i < userBankaccount.Count; i++)
            {
                BankAccount bankAccount = userBankaccount[i];   
                Console.WriteLine("" + (i+1) + "." + bankAccount);
            }
          
           Console.WriteLine("What account do you want to move money from? ");

            int AccountNumberFrom = GetUserAccountSelection(0, userBankaccount.Count,-1);
            BankAccount AccountFrom = userBankaccount[AccountNumberFrom-1];

            Console.WriteLine("What account do you want to move money to? ");
            int AccountNumberTo = GetUserAccountSelection(0, userBankaccount.Count,AccountNumberFrom);
            
            BankAccount AccountTo = userBankaccount[AccountNumberTo - 1];

            bool parseSuccess;
            decimal AmmountMove;
            do {

                Console.WriteLine("How much money do you want to move? ");
                parseSuccess = decimal.TryParse(Console.ReadLine(), out AmmountMove);
            } while (!parseSuccess || AmmountMove < 0);

            //Find logged in user object by username for use in ProcessTransaction
            User ownerObject = User.userList.Find(x => x.Username == username);

            Console.WriteLine(Transaction.ProcessTransaction(AccountFrom, AccountTo, ownerObject, ownerObject, AmmountMove));
            Console.ReadKey();
        }

        private static int GetUserAccountSelection(int minValue, int maxValue, int previousValue)
        {
            bool ValidSelection = false;
            int Selection = -1;

            while(!ValidSelection)
            {

                  int.TryParse(Console.ReadLine(), out Selection);

                    if(Selection > minValue && Selection <= maxValue && Selection != previousValue)
                    {
                        ValidSelection = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Account Selection, Please try again ");
                    }
            }
            return Selection;
        }
        
        //Method to show decimal numbers rounded to two decimals without changing the accual input
        internal static string RoundTwoDecimals(decimal input)
        {
            //decimal roundedDecimal = Math.Round(input, 2);
            return Math.Round(input, 2).ToString("0.00");
        }

        //Method to convert and return interest decimal to percent format string
        internal static string ConvertInterestToString(decimal interest)
        {
            string inputString = interest.ToString();
            StringBuilder toReturn = new StringBuilder();
            for (int i = 2; i < inputString.Length; i++)
            {
                if (inputString[i] != '0' && i == 2) //1.15 format
                {
                    toReturn.Append(inputString[i]);
                    toReturn.Append(inputString[i + 1]);
                    toReturn.Append(",");
                    toReturn.Append(inputString.Substring(i + 2));
                    return toReturn.ToString();
                }
                else if (inputString[i] != '0' && i == 3) //1.015 format
                {
                    toReturn.Append(inputString[i]);
                    toReturn.Append(",");
                    toReturn.Append(inputString.Substring(i + 1));
                    return toReturn.ToString();
                }
                else if (inputString[i] != '0' && i > 3) //1.005 format
                {
                    toReturn.Append("0,");
                    toReturn.Append(inputString[i]);
                    toReturn.Append(inputString.Substring(i + 1));
                    return toReturn.ToString();
                }
            }
            return null;
        }

        public static void ExternalTransaction(string username)
        {
            Console.Clear();
            Console.WriteLine("Bankaccounts");
            List<BankAccount> userBankaccount = bankAccounts.FindAll(x => x.Owner == username);

            for (int i = 0; i < userBankaccount.Count; i++)
            {
                BankAccount bankAccount = userBankaccount[i];
                Console.WriteLine("" + (i + 1) + "." + bankAccount);
            }

            Console.WriteLine("What account do you want to move money from? ");

            int AccountNumberFrom = GetUserAccountSelection(0, userBankaccount.Count, -1);
            BankAccount AccountFrom = userBankaccount[AccountNumberFrom - 1];

            Console.Write("What account do you want to move the money to: ");
            string AccountToInput = Console.ReadLine();

            if (!bankAccounts.Exists(x => x.AccountNumber == AccountToInput))
            {
                Console.WriteLine("Account does not exsist!");
                Console.ReadKey();
                return;

            }

            
            BankAccount Accountto = bankAccounts.Find(x => x.AccountNumber == AccountToInput);

            bool parseSuccess;
            decimal AmmountMove;
            do
            {

                Console.WriteLine("How much money do you want to move? ");
                parseSuccess = decimal.TryParse(Console.ReadLine(), out AmmountMove);
            } while (!parseSuccess || AmmountMove < 0);

            //Find logged in user and owner to account objects by usernames for use in ProcessTransaction
            User fromOwnerObject = User.userList.Find(x => x.Username == username);
            User toOwnerObject = User.userList.Find(x => x.Username == Accountto.Owner);

            Console.WriteLine(Transaction.ProcessTransaction(AccountFrom, Accountto, fromOwnerObject, toOwnerObject, AmmountMove));
            Console.ReadKey();
        }
    }
}
         

   


