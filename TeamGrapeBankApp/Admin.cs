using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class Admin : User
    {
        //Constructor
        public Admin(string username, string password) : base(username, password)
        {
            Username = username;
            Password = password;
        }
    }
}
