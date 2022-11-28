using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class Admin : User
    {
        //Constructor
        public Admin(int id, string username, string password, string firstname, string lastname) : base(id, username, password, firstname, lastname)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
        }

        //Admin menu method
        internal static void AdminMenu(User loggedInAdmin)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {loggedInAdmin.Firstname}\n");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add a customer account");
            Console.WriteLine("2. Log out");

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
                    Console.WriteLine("Log out");
                    break;

                default:
                    Console.WriteLine("Invalid choice, enter a key to return to the menu");
                    Console.ReadKey();
                    AdminMenu(loggedInAdmin);
                    break;
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
    }
}
