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
            Console.WriteLine($"\n   Du kommer få en månadsfaktura på beloppet du lånat och det tillkommer även {_interest.ToString("#0.##%")} ränta för de lånade pengarna.");

            decimal withdrawal = amount;
            decimal toInvoice = 0;

            if (balance > 0 && amount > balance) // Checks if there's a plus balance
            {
                amount = amount - balance; // Removes the plus balance so the credit interest can be calculated
                toInvoice = amount + (amount * _interest); // amount withdrawn + interest
            }
            else if (balance <= 0)
            {
                toInvoice = amount + (amount * _interest); // amount withdrawn + interest
            }
           

            //decimal totalDept = amount + (amount * _interest); // amount withdrawn + interest
            Console.WriteLine($"\n   För detta uttag av {withdrawal.ToString("F")} {currency} kommer du behöva betala {toInvoice.ToString("F")} {currency} vid nästa faktura.");
        }
    }
}
