using System;
using System.Collections.Generic;
namespace TeamHamsterBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank.DeclareUsers();
            Bank.AddAccounts();
            Bank.Login();
        }
    }
}
