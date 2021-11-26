using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Customer : User
    {
        int customerId;
        List<Account> accounts;
        public Customer(string UserId, string FullName, string Password)
         : base(UserId, FullName, Password)
        {

        }
        public List<Account> Accounts {get => accounts;}
        private void CreateNewAccount()
        {

        }
    }
}
