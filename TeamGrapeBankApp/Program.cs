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

            ConsoleSettings();
           
            WelcomeLoop();
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

        //We need this for setting color and consolewindow size when app is runnning 
        static void ConsoleSettings()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WindowHeight = 25;
            Console.WindowWidth = 70;
            Console.Title = "Grape Bank";

        }

        //This code is needed for welcoming the user to the app when starting. 
        static void WelcomeLoop()
        {
            string welcometext = "Welcome and thank you for chosing TeamGrape´s bank application";
            for (int i = 0; i < welcometext.Length; i++)
            {
                System.Threading.Thread.Sleep(100);
                Console.Write(welcometext[i]);
            }
            string PressAnyKey = "\nPress any key to proceed....";
            for (int i = 0; i < PressAnyKey.Length; i++)
            {
                System.Threading.Thread.Sleep(100);
                Console.Write(PressAnyKey[i]);
            }

            Console.WriteLine("\n   \r\n     __ {_/\r\n     \\_}\\\\ _\r\n        _\\(_)_\r\n       (_)_)(_)_\r\n      (_)(_)_)(_)\r\n       (_)(_))_)\r\n        (_(_(_)\r\n         (_)_)\r\n       (_)  ");

            Console.ReadKey();

        }






    }
}
