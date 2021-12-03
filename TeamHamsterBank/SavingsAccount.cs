using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class SavingsAccount : Account
    {
        decimal _interest;

        public SavingsAccount(string accountName, string accountType, string currency, string customerID) 
            : base (accountName, accountType, currency, customerID)
        {
            _interest = 1.01m;
        }

        public static void CalculateSavingsInterest(decimal deposit, double years, decimal interest, bool showText)
        {
            if (showText)
            {
                Console.WriteLine($"  På vårt sparkonto får du 1% i månadssparränta\n  {deposit} kr är värda: \n");
            }

            decimal amount = deposit;
            double months = years * 12;

            for (int i = 1; i <= months; i++)
            {
                amount += (amount * interest);
            }

            Console.WriteLine($"  Efter {years} år\t {amount.ToString("F")}" /*Add currency here*/);
        }
    }
}
