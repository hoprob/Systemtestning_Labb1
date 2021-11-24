using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class Admin:User
    {
        public Admin(string UserId, string FullName, string Password)
            :base(UserId, FullName, Password)
        {
             
        }
        private void CreateNewCustomer()
        {

        }
    }
}
