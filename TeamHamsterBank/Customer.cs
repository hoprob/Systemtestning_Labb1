using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Customer : User
    {
        public Customer(string UserId, string FullName, string Password)
         :base(UserId, FullName, Password)
        {

        }
        int customerId;
        List<Account> accounts;
        private void CreateNewAccount()
        {

        }
    }
}
