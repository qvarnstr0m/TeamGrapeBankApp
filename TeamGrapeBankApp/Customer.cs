using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class Customer : User
    {
        //Properties
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public bool LockedOut { get; set; }

        //Constructor
        public Customer(string username, string password, string firstname, string lastname, string adress, string email, string phonenumber, bool lockedOut) : base(username, password)
        {
            Firstname = firstname;
            Lastname = lastname;
            Adress = adress;
            Email = email;
            Phonenumber = phonenumber;
            LockedOut = lockedOut;
        }
    }
}
