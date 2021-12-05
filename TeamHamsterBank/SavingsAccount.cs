﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class SavingsAccount : Account
    {
        public decimal _interest; // Delete?

        public SavingsAccount(string accountName, string accountType, string currency, string customerID) 
            : base (accountName, accountType, currency, customerID)
        {
            _interest = 0.01m; // Delete?
        }

        public static void CalculateSavingsInterest(decimal deposit, double years, bool showText, string currency, decimal _interest = 0.01m)
        {
            if (showText)
            {
                Console.WriteLine($"  På vårt sparkonto får du {_interest.ToString("#0.##%")} i månadssparränta\n  {deposit} {currency} är värt: \n");
            }

            decimal amount = deposit;
            double months = years * 12;

            for (int i = 1; i <= months; i++)
            {
                amount += (amount * _interest);
            }

            Console.WriteLine($"  Efter {years} år\t {amount.ToString("F")} {currency}");
        }
    }
}
