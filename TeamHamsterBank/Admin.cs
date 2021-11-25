using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamHamsterBank
{
    class Admin:User
    {
        public Admin(string FullName, string UserId, string Password) : base(FullName, UserId, Password) { }
        public static void CreateNewCustomer(List<User> UsersList)
        {
            Console.WriteLine("\t*** Skapa en ny användare ***\n");

            // Generate user id
            Random rnd = new Random();
            int id = rnd.Next(100000, 999999);
            string userId = id.ToString();

            // Check if user id is unique else generate a new id
            bool isIdUnique = false;
            do
            {
                foreach (var item in UsersList)
                {
                    if (item.UserID == userId)
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
            Console.Write("Ange kundens fullständiga namn: ");
            string inputFullName = Console.ReadLine().Trim();
            while (inputFullName.Any(char.IsNumber) || inputFullName.Length < 2)
            {
                Console.Write("\nOglitligt namn. Ange ett fullständigt namn: ");
                inputFullName = Console.ReadLine().Trim();
            }

            // Get password
            Console.Write("Ange ett lösenord: ");
            string inputPassword = Console.ReadLine().Trim();
            while (inputPassword.Length < 8)
            {
                Console.WriteLine("\nLösenordet måste vara minst 8 tecken. Vänligen ange ett annat lösenord.");
                Console.Write("Ange ett lösenord:");
                inputPassword = Console.ReadLine().Trim();
            }

            // New customer object
            Customer newCustomer = new Customer(userId, inputFullName, inputPassword);
            UsersList.Add(newCustomer);

            Console.WriteLine($"\nNy användare {newCustomer.FullName} med ID {newCustomer.UserID} har skapats.");
        }
    }
}
