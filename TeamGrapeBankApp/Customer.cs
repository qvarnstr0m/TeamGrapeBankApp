using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TeamGrapeBankApp
{
    public class Customer : User
    {
        //Properties
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        

        //Constructor
        public Customer(int id, string username, string password, string firstname, string lastname, string adress, string email, string phonenumber, bool lockedOut) : base(id, username, password, firstname, lastname, lockedOut)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Adress = adress;
            Email = email;
            Phonenumber = phonenumber;
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
            Console.WriteLine("2. Open a new bank account");
            Console.WriteLine("3. Internal transaction");
            Console.WriteLine("4. External transaction");
            Console.WriteLine("5. List bank account transactions");
            Console.WriteLine("6. Open a new savings account");
            Console.WriteLine("7. Take loan");
            Console.WriteLine("8. Log out");

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
                    BankAccount.OpenNewAccount(loggedInCustomer);
                    CustomerMenu(loggedInCustomer);
                    break;
                    
                case 3:
                    BankAccount.InternalTransaction(loggedInCustomer.Username);
                    CustomerMenu(loggedInCustomer);
                    break;
                case 4:
                    BankAccount.ExternalTransaction(loggedInCustomer.Username);
                    CustomerMenu(loggedInCustomer);
                    break;
                case 5:
                    Transaction.ListTransactions(loggedInCustomer);
                    CustomerMenu(loggedInCustomer);
                    break;
                case 6:
                    SavingsAccount.OpenNewSavingsAccount(loggedInCustomer);
                    CustomerMenu(loggedInCustomer);
                    break;
                case 7:
                    LoanAccount.TakeLoan(loggedInCustomer);
                    CustomerMenu(loggedInCustomer);
                    break;
                case 8:
                    LogOutAnimation();
                    Console.WriteLine("\nYou are logged out. Press a key to return to login menu");
                    Console.ReadKey();
                    User.Login();
                    break;
                default:
                    Console.WriteLine("Invalid choice, enter a key to return to the menu");
                    Console.ReadKey();
                    CustomerMenu(loggedInCustomer);
                    break;
            }
            
            static void LogginOutSound()
            {
                int C = 528;
                int D = 495;
                int E = 440;
                int F = 396;
                int G = 352;
                int A = 330;
                int B = 297;
                int C2 = 264;
                int half = 1000 / 2;
                int quarter = 1000 / 4;

                Console.Beep(C, quarter);
                Console.Beep(D, quarter);
                Console.Beep(E, quarter);
                Console.Beep(F, quarter);
                Console.Beep(G, quarter);
                Console.Beep(A, quarter);
                Console.Beep(B, quarter);
                Console.Beep(C2, half);
                Thread.Sleep(quarter);
            }

            static void LogOutAnimation()
            {
                string bufferDots = "......";
                Console.WriteLine("You are being logged out ");
                for (int i = 0; i < bufferDots.Length; i++)
                {
                    Thread.Sleep(300);
                    Console.Write(bufferDots[i]);
                }
                LogginOutSound();
                Console.WriteLine(" \nPress a key to return to login menu ");
                Console.ReadKey();
                User.Login();
            }
        }
        
    }
}
