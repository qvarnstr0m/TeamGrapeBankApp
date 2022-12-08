using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace TeamGrapeBankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Start daily background timer
            StartDailyTimer();
            //Generates hardcoded users
            User.GenerateUsers();
            //Generates bankaccounts for users
            BankAccount.GenerateBankAccounts();
            //Generate saving accounts for users
            SavingsAccount.GenerateSavingsAccounts();
            //Generate saving accounts for users
            LoanAccount.GenerateLoanAccounts();
            //Run login method
            User.Login();
        }

        //Method to run 24h timer for daily background updates
        internal static void StartDailyTimer()
        {
            var timer = new Timer(86400000); //24h = 86400000 ms, 30 s = 30000 ms 
            timer.Elapsed += RunInterestUpdates;
            timer.Enabled = true;
        }

        //Method to run daily background updates
        internal static void RunInterestUpdates(object source, ElapsedEventArgs e)
        {
            SavingsAccount.UpdateSavingsAccounts();
            LoanAccount.UpdateLoanAccounts();
        }
    }
}
