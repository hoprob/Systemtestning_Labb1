using System;
using System.Collections.Generic;
using System.Text;

namespace TeamHamsterBank
{
    class CreditAccount : Account
    {
        public static decimal _interest = 0.02m;

        public CreditAccount(string accountName, string accountType, string currency, string customerID)
            : base(accountName, accountType, currency, customerID)
        {
        }

        public static void CalculateCreditInterest(decimal amount, string currency, decimal balance)
        {
            Console.Clear();
            Console.WriteLine($"\n   Du kommer få en månadsfaktura på beloppet du använt och det tillkommer även {_interest.ToString("#0.##%")} ränta för de använda pengarna.");

            if (balance > 0)
            {
                amount = amount - balance;
            }

            decimal totalDept = amount + (amount * _interest); // amount withdrawn + interest
            Console.WriteLine($"\n   För detta uttag av {amount} {currency} kommer du behöva betala {totalDept.ToString("F")} {currency} vid nästa faktura.");
        }
    }
}
