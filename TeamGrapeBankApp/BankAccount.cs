using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class BankAccount
    {
        protected string AccountNumber { get; set; }
        protected string Owner  { get; set; }
        protected string Currency { get; set; }
        protected decimal Balance { get; set; }

        public static List<BankAccount> bankAccounts = new List<BankAccount>();
        public BankAccount(string accountNumber, string owner, string currency, decimal balance)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Currency = currency;
            Balance = balance;
        }

        public static void GenerateBankAccounts()
        {
            //Hardcode some bankaccounts and adds them to list (should change to database later)
            BankAccount Acc1 = new BankAccount("4444-5555", "billgates", "SEK", 1000000.345m);
            BankAccount Acc2 = new BankAccount("4444-3577", "billgates", "SEK", 50043);
            BankAccount Acc3 = new BankAccount("4444-2644", "billgates", "SEK", 3205);

            BankAccount Acc4 = new BankAccount("5555-2644", "annasvensson", "SEK", 45000);
            BankAccount Acc5 = new BankAccount("5555-1533", "annasvensson", "SEK", 2300);

            BankAccount Acc6 = new BankAccount("6666-7533", "hermessaliba", "SEK", 74442.43m);
            BankAccount Acc7 = new BankAccount("6666-2685", "hermessaliba", "USD", 25114.79m);

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
            return $"Accountnumber: {AccountNumber}\nBalance: {Balance}{Currency}\n";
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

            Console.WriteLine("All accounts listed, please press a key to return to menu");
            Console.ReadKey();
        }
    }
}
