using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamGrapeBankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Generates hardcoded users
            User.GenerateUsers();
            //Generates bankaccounts for users
            BankAccount.GenerateBankAccounts();
            //Run login method
            User.Login();
            
        }
    }
}
