using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class Customer : User
    {
        //Properties
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public bool LockedOut { get; set; }

        //Constructor
        public Customer(int id, string username, string password, string firstname, string lastname, string adress, string email, string phonenumber, bool lockedOut) : base(id, username, password, firstname, lastname)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Adress = adress;
            Email = email;
            Phonenumber = phonenumber;
            LockedOut = lockedOut;
        }

        //Override ToString method
        public override string ToString()
        {
            return $"Id: {Id}\nUsername: {Username}\nPassword: {Password}\nFirstname: {Firstname}\nLastname: {Lastname}\nAdress: {Adress}\nEmail: {Email}\nPhonenumber: {Phonenumber}\nLocked out: {LockedOut}";
        }

        //Customer menu
        internal static void CustomerMenu (User loggedInCustomer)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {loggedInCustomer.Firstname}\n");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. List your bankaccounts");
            Console.WriteLine("2. Log out");
            Console.WriteLine("3. Transfer between accounts");

            bool parseSuccess;
            int userChoice;
            do
            {
                Console.Write("Enter a choice: ");
                parseSuccess = int.TryParse(Console.ReadLine(), out userChoice);
            } while (!parseSuccess);

            switch (userChoice)
            {
                case 1:
                    BankAccount.ListBankaccounts(loggedInCustomer.Username);
                    CustomerMenu(loggedInCustomer);
                    break;
                case 2:
                    Console.WriteLine("Log out");
                    break;

                case 3:
                    BankAccount.internalTransaction(loggedInCustomer.Username);
                    CustomerMenu(loggedInCustomer);
                    break;

                default:
                    Console.WriteLine("Invalid choice, enter a key to return to the menu");
                    Console.ReadKey();
                    CustomerMenu(loggedInCustomer);
                    break;
            }
        }
    }
}
