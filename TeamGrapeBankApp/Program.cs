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
            //Start background timer
            StartTimer();
            //Generates hardcoded users
            User.GenerateUsers();
            //Generates bankaccounts for users
            BankAccount.GenerateBankAccounts();
            //Generate saving accounts for users
            SavingsAccount.GenerateSavingsAccounts();
            //Run login method
            User.Login();
        }

        //Method to run timer in background
        internal static void StartTimer()
        {
            var timer = new System.Timers.Timer(30000); //15 min = 900 000
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
        }

        //Method to run background checks
        internal static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            SavingsAccount.UpdateSavingsAccounts();
        }
    }
}
