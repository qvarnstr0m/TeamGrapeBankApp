using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamGrapeBankApp
{
    internal abstract class User
    {
        //Properties
        public string Username { get; set; }
        public string Password { get; set; }

        //Constructor
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        //Method to login user
        internal static void Login()
        {
            //Create User objects as Admin and Customers and add to list
            //This should be handled by a database in the future
            User sven = new Customer("billgates", "pass1", "Bill", "Gates", "Nygatan 26 31176 Falkenberg", "bill@microsoft.se", "+46702222222", false);
            User anna = new Customer("annasvensson", "pass2", "Anna", "Svensson", "Nygatan 26 31176 Falkenberg", "anna@svensson.se", "+46703333333", false);
            User hermes = new Customer("hermessaliba", "pass3", "Hermes", "Saliba", "Nygatan 28 31176 Falkenberg", "hermes@gmail.com", "+46704444444", false);
            User admin = new Admin("admin", "pass");

            List<User> userList = new List<User>();
            userList.Add(sven);
            userList.Add(anna);
            userList.Add(hermes);
            userList.Add(admin);

            //Welcome message and login logic
            //Loop while entered username doesnt exist
            Console.WriteLine("Welcome the the bank\n");
            string enteredUsername;
            do
            {
                Console.Write("Enter username: ");
                enteredUsername = Console.ReadLine();
            } while (!userList.Any(x => x.Username == enteredUsername));

            //Store found user in userTryLogin
            User userTryLogin = userList.Find(x => x.Username == enteredUsername);

            //Loop while there are logintries left or login is not successful
            int loginTries = 3;
            bool loginSuccess = false;
            do
            {
                
                Console.Write($"Enter password, you have {loginTries} tries left: ");
                string enteredPassword = Console.ReadLine();
                loginTries--;
                if (enteredPassword == userTryLogin.Password)
                {
                    loginSuccess = true;
                }
            } while (loginTries > 0 && loginSuccess == false);

            //Logic to check if login is successful, change later
            if (loginSuccess == true && userTryLogin is Admin)
            {
                Console.WriteLine($"{userTryLogin.Username} is logged in as Admin");
            }
            else if (loginSuccess == true && userTryLogin is Customer)
            {
                Console.WriteLine($"{userTryLogin.Username} is logged in as Customer");
            }
            else
            {
                Console.WriteLine($"User {userTryLogin.Username} is locked from login");
            }
        }
    }
}
