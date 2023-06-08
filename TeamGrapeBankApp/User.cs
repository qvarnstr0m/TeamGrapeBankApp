using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TeamGrapeBankApp
{
    public abstract class User
    {
        //Properties
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool LockedOut { get; set; }

        //List to hold users
        public static List<User> userList = new List<User>();

        //Constructor
        public User(int id, string username, string password, string firstname, string lastname, bool lockedOut)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
            LockedOut = lockedOut;
        }
        internal static void GenerateUsers()
        {
            //Create User objects as Admin and Customers and add to list
            //This should be handled by a database in the future
            User sven = new Customer(1, "billgates", "pass1", "Bill", "Gates", "Nygatan 26 31176 Falkenberg", "bill@microsoft.se", "+46702222222", false);
            User anna = new Customer(2, "annasvensson", "pass2", "Anna", "Svensson", "Nygatan 26 31176 Falkenberg", "anna@svensson.se", "+46703333333", false);
            User hermes = new Customer(3, "hermessaliba", "pass3", "Hermes", "Saliba", "Nygatan 28 31176 Falkenberg", "hermes@gmail.com", "+46704444444", false);
            User admin = new Admin(4, "admin", "pass", "Anas", "Qlok", false);


            userList.Add(sven);
            userList.Add(anna);
            userList.Add(hermes);
            userList.Add(admin);
        }

        //Method to login user
        internal static void Login()
        {
            

            //Welcome message and login logic
            //Loop while entered username doesnt exist
            Console.Clear();
            LogoArt();
            Console.WriteLine("Welcome the the bank\n");
            string enteredUsername;
            do
            {
                Console.Write("Enter username: ");
                enteredUsername = Console.ReadLine();
            } while (!userList.Any(x => x.Username == enteredUsername));

            //Store found user in userTryLogin
            User userTryLogin = userList.Find(x => x.Username == enteredUsername);
            //Check if user is locked
            if (userTryLogin.LockedOut == true)
            {
                Console.WriteLine($"{userTryLogin.Username} is currently locked out, please contact admin to unlock it. Please press a key to return to login menu.");
                Console.ReadKey();
                Login();
            }
            //Loop while there are logintries left or login is not successful
            int loginTries = 3;
            bool loginSuccess = false;
            string bufferDots = ".........";
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
                Console.WriteLine("You are being logged in ");
                for(int i =0; i < bufferDots.Length; i++)
                {
                    Thread.Sleep(100);
                    Console.Write(bufferDots[i]);
                }
                LogginInSound();
                Admin.AdminMenu(userTryLogin);
            }
            else if (loginSuccess == true && userTryLogin is Customer)
            {
                Console.WriteLine("You are being logged in ");
                for(int i = 0; i < bufferDots.Length; i++)
                {
                    Thread.Sleep(100);
                    Console.Write(bufferDots[i]);
                }
                LogginInSound();
                Customer.CustomerMenu(userTryLogin);
            }
            else
            {
                Console.WriteLine($"User {userTryLogin.Username} is locked from login, press a key to return to login menu");
                userTryLogin.LockedOut = true;
                Console.ReadKey();
                Login();
            }

            static void LogoArt()
            {
                Console.WriteLine("\r\n   _____                        ____              _    \r\n  / ____|                      |  _ \\            | |   \r\n | |  __ _ __ __ _ _ __   ___  | |_) | __ _ _ __ | | __\r\n | | |_ | '__/ _` | '_ \\ / _ \\ |  _ < / _` | '_ \\| |/ /\r\n | |__| | | | (_| | |_) |  __/ | |_) | (_| | | | |   < \r\n  \\_____|_|  \\__,_| .__/ \\___| |____/ \\__,_|_| |_|_|\\_\\\r\n                  | |                                  \r\n                  |_|                                  ");

            }

            static void LogginInSound()
            {
                int C = 264;
                int D = 297;
                int E = 330;
                int F = 352;
                int G = 396;
                int A = 440;
                int B = 495;
                int C2 = 528;


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
    }
}