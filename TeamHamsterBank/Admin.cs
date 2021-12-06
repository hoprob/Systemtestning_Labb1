﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TeamHamsterBank
{
    class Admin:User
    {
        public Admin(string UserId, string FullName,  string Password) : base(UserId, FullName,  Password) { }
        public static void CreateNewCustomer(List<User> UsersList)
        {
            Console.WriteLine("\t*** Skapa en ny användare ***\n");

            // Generate new user id
            Random rnd = new Random();
            int id = rnd.Next(100000, 999999);
            string userId = id.ToString();

            // Check if user id is unique else generate a new id
            bool isIdUnique = false;
            do
            {
                foreach (User user in UsersList)
                {
                    if (user.UserID == userId)
                    {
                        id = rnd.Next(100000, 999999);
                        userId = id.ToString();
                    }
                    else
                    {
                        isIdUnique = true;
                    }
                }
            } while (isIdUnique == false);

            // Get full name
            Console.Write("  Ange kundens fullständiga namn: ");
            string inputFullName = Console.ReadLine().Trim();
            while (inputFullName.Any(char.IsNumber) || inputFullName.Length < 6)
            {
                Console.Write("\n  Oglitligt namn. Ange ett fullständigt namn: ");
                inputFullName = Console.ReadLine().Trim();
            }
            // Get password
            string inputPassword = NewPassword();
            // Create a new customer object and add to UsersList
            Customer newCustomer = new Customer(userId, inputFullName, inputPassword);
            UsersList.Add(newCustomer);

            Console.Clear();
            Console.WriteLine($"\n  Ny användare {newCustomer.FullName} med ID {newCustomer.UserID} har skapats.\n");

            // Create new account for new user
            newCustomer.CreateNewAccount();
        }
        internal static void SetCurrencyRate() // Prints available currencies in the bank
        {
            Bank.PrintCurrentExchange();
            int index = -1;
            while (index > Account.CurrencyList.Count - 1 || index < 1)
            {
                Console.Write("\n\n\tVälj vilken valuta:   ");
                Int32.TryParse(Console.ReadLine(), out index);
            }
            Console.Write("\n\n\tSkriv in växlingspriset:   ");
            decimal price = 0;
            while (!Decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("\n\n\tSkriv in växlingspriset:   ");
            }
            string priceStr = price.ToString();
            if (priceStr.Contains(','))
                priceStr = priceStr.Replace(',', '.');
            Account.CurrencyList[index][1] = priceStr;

            Console.WriteLine(Account.CurrencyList[index][1]);
            Bank.PrintCurrentExchange();
        }
    }
}
