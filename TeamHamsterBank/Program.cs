using System;
using System.Collections.Generic;
namespace TeamHamsterBank
{
    class Program
    {
        static void Main(string[] args)
        {
            HamsterArt.HamsterWelcome();
            Bank.TransactionProcessTimer();
            StoreAndLoad.LoadTransactions();
            StoreAndLoad.LoadAccounts();
            StoreAndLoad.LoadUsers();
            Bank.Login();
        }
    }
}
