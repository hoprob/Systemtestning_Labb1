using System;
using System.Collections.Generic;
namespace TeamHamsterBank
{
    class Program
    {
        static void Main(string[] args)
        {
            StoreAndLoad.LoadAccounts();
            StoreAndLoad.LoadUsers();

            Bank.Login();
        }
    }
}
