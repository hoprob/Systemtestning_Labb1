using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

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
        public void ChangeUserPassword(List<User> users)
        {
            int id;
            string input;
            bool errorBool = false;
            Bank.ReturnInstruction(0);
            do
            {
                Console.Write("\n\n\tSkriv in ID för användaren du vill ändra" +
                    " lösenord på: ");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out id) && id.ToString().Length == 6)
                {
                    errorBool = false;
                    if (users.Exists(u => u.UserID == id.ToString()))
                    {
                        User temp = users.Find(u => u.UserID == id.ToString());
                        Console.Clear();
                        Bank.ReturnInstruction(0);
                        Console.WriteLine($"\n\n\t**Ändra lösenord för användare" +
                            $" [{temp.UserID}] [{temp.FullName}]**");
                        if (temp.ChangePassword())
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n\t\tLösenordet är nu ändrat!");
                            Thread.Sleep(1800);
                        }
                        errorBool = false;
                    }
                    else
                    {
                        Console.Clear();
                        Bank.ReturnInstruction(0);
                        Console.WriteLine($"\n\n\tAnvändare med ID: [{id}]" +
                            $" finns EJ registerad!\n\n");
                        errorBool = true;
                    }
                }
                else if (input.ToUpper() == "R")
                {
                    errorBool = false;
                }
                else
                {
                    //Fel inmatning
                    Console.Clear();
                    Bank.ReturnInstruction(0);
                    Console.WriteLine("\n\n\tFel inmatning! Var vänlig mata in ett" +
                        " 6-siffrigt ID!\n\n");
                    errorBool = true;
                }
            } while (errorBool);
        }
        internal static void DeleteUser()
        {
            string inputUser_ID = String.Empty;
            string username = String.Empty;
            int index;
            while (true)
            {
                Console.Clear();
                Bank.ReturnInstruction(0);
                Console.CursorVisible = true;
                Console.Write("\n\n\tSkriv in användar-ID du vill ta bort: ");
                inputUser_ID = Console.ReadLine().Trim();
                if (inputUser_ID.ToUpper() == "R")
                {
                    return;
                }
                else if (CheckUserName(Bank.UsersList, inputUser_ID))
                {
                    index = Bank.UsersList.FindIndex(user => user.UserID == inputUser_ID);
                    username = Bank.UsersList[index].FullName;
                    Bank.UsersList.RemoveAt(index);
                    Console.Write($"\n\n\t  {username} avlägsnades från Hamsterbanken");
                    Bank.Redirecting();
                    return;
                }
                else if (inputUser_ID != "")
                {
                    Console.Write($"\n\n\t{inputUser_ID} finns inte i systemet");
                    Console.CursorVisible = false;
                    Console.ReadKey();
                }
            }

        }
    }
}
