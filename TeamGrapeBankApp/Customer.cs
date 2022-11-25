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
    }
}
