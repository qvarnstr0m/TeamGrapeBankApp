using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace TeamGrapeBankApp
{
    internal class Admin : User
    {
        //Constructor
        public Admin(int id, string username, string password, string firstname, string lastname, bool lockedOut) : base(id, username, password, firstname, lastname, lockedOut)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
        }

        //Dictionary to hold SEK to different currencies exchange rates, should change to DB, JSON or dynamic later
        internal static Dictionary<string, decimal> currencyDict = new Dictionary<string, decimal>()
        {
            {"SEK", 1m }, //Do not change
            {"USD", 12.1m }
        };

        //Dictionary to hold saving account rates, Key = months balance is locked from withdrawals
        internal static Dictionary<int, decimal> savingsDict = new Dictionary<int, decimal>()
        {
            {12, 1.0150m },
            {6, 1.0050m }
        };

        //Dictionary to hold interest rate for loan accounts,
        internal static Dictionary<int, decimal> interestDict = new Dictionary<int, decimal>()
        {
            {24, 1.0500m },
            {36, 1.0600m},
        };

        //Admin menu method
        internal static void AdminMenu(User loggedInAdmin)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {loggedInAdmin.Firstname}\n");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add a customer account");
            Console.WriteLine("2. Set a new currency exchange rate");
            Console.WriteLine("3. Log out");

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
                    AddCustomer();
                    AdminMenu(loggedInAdmin);
                    break;
                case 2:
                    SetExchangeRates();
                    AdminMenu(loggedInAdmin);
                    break;
                case 3:
                    LogOutAnimation();
                    break;

                default:
                    Console.WriteLine("Invalid choice, enter a key to return to the menu");
                    Console.ReadKey();
                    AdminMenu(loggedInAdmin);
                    break;
                    
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

        }

        //Method for admin to add customer
        private static void AddCustomer()
        {
            Console.Clear();

            //Get user input for new customer
            int id = User.userList.Last().Id + 1;
            Console.Write("Enter a unique username: ");
            string username = Console.ReadLine();
            //Checks userList if username already exists and saves it as a bool
            bool existing = userList.Any(x => x.Username == username);
            while (existing)
            {
                Console.Write("Username already exists, try a different one: ");
                username = Console.ReadLine();
                existing = userList.Any(x => x.Username == username);
            }
            
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();
            Console.Write("Enter customer first name: ");
            string firstname = Console.ReadLine();
            Console.Write("Enter customer last name: ");
            string lastname = Console.ReadLine();
            Console.Write("Enter customer adress: ");
            string adress = Console.ReadLine();
            Console.Write("Enter customer email: ");
            string email = Console.ReadLine();
            Console.Write("Enter customer phonenumber: ");
            string phonenumber = Console.ReadLine();
            bool lockedOut = false;

            Customer newCustomer = new Customer(id, username, password, firstname, lastname, adress, email, phonenumber, lockedOut);
            
            Console.WriteLine(newCustomer + "\n");
            
            string confirmAddCustomer;
            do
            {
                Console.Write("Do you want to add this customer to the system? (Y/N)");
                confirmAddCustomer = Console.ReadLine().ToUpper();
            } while (confirmAddCustomer != "Y" && confirmAddCustomer != "N");

            if (confirmAddCustomer == "Y")
            {
                User.userList.Add(newCustomer);
                Console.WriteLine("Customer added! Press a key to return to the menu");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Customer not added. Press a key to return to the menu");
                Console.ReadKey();
            }
        }

        //Method for admin to set current exchange rates between different currencies
        private static void SetExchangeRates()
        {
            Console.Clear();
            Console.WriteLine("Available currencies:\n");
            foreach (var item in currencyDict)
            {
                if (item.Key != "SEK") //Hide SEK
                {
                    Console.WriteLine($"Currency: {item.Key}\nCurrent rate: {item.Value}\n");
                }
            }

            string adminChoice;
            do
            {
                Console.Write("Enter a currency to update or \"0\" to return to the main menu: ");
                adminChoice = Console.ReadLine().ToUpper();
            } while (adminChoice != "0" && !currencyDict.ContainsKey(adminChoice) || adminChoice.ToUpper() == "SEK");

            if (adminChoice == "0")
            {
                Console.WriteLine("Press a key to return to main menu");
                Console.ReadKey();
            }

            else
            {
                bool parseSuccess;
                decimal newRate;
                do
                {
                    Console.Write($"Enter new rate for {adminChoice}: ");
                    parseSuccess = decimal.TryParse(Console.ReadLine(), out newRate);
                } while (!parseSuccess);

                currencyDict[adminChoice] = newRate;
                Console.WriteLine($"New rate for {adminChoice}: {newRate}\nPress a key to return to main menu");
                Console.ReadKey();
            }
        }
    }
}
