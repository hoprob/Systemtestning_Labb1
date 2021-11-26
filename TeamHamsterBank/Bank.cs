using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace TeamHamsterBank
{
    class Bank
    {
        internal static List<User> UsersList = new List<User>();
                      // Objects for testing
        public static void DeclareUsers()
        {
                                     //    _user_ID  , _fullName ,  _password                  
            UsersList.Add(new Admin("111111", "Robin Svensson", "password"));
            UsersList.Add(new Admin("222222", "Elin Ericstam", "password"));
            UsersList.Add(new Customer("333333", "Nael Sharabi", "password"));
            UsersList.Add(new Customer("444444", "Gillian Brown", "password"));
            UsersList.Add(new Customer("555555", "Allen Lee", "password"));
            UsersList.Add(new Customer("666666", "Mike Jefferson", "password"));
            UsersList.Add(new Customer("777777", "Alfred Kaiser", "password"));
        }
                // Adding accounts to the test-objects for testing.
                // All customers are getting the same acount details.
        public static void AddAccounts()
        {
            foreach (User user in UsersList)
            {
                if (user is Customer)
                {
                    Customer customer =  user as Customer;
                    customer._accounts.Add(new Account("Allkonto         ", 50000));
                    customer._accounts.Add(new Account("Sparkonto_       ", 120000));
                    customer._accounts.Add(new Account("Framtidskonto    ", 15000));
                    customer._accounts.Add(new Account("Investeringskonto", 30000));
                }
            }
        }
        public static void Login()
        {
            Console.Clear();
            Console.Write("\n\n\t\tVälkommen till HamsterBanken\n\n\n" +
                    "\tVar god och skriv in ditt Användar-ID:  ");
            int attempts = 3;
            string inputUser_ID = String.Empty;
            bool found = false;
            User user = null;
            while (attempts > 0)
            {
                if (!found)
                {
                    inputUser_ID = Console.ReadLine().ToUpper();
                    if (User.CheckUserName(UsersList, inputUser_ID.ToUpper()))
                    {
                        found = true;
                    } 
                }
                if (!found)
                {
                    Console.Write("\n\n   Användar-ID kan " +
                        "inte hittas. Var god och Försök igen:  ");
                    continue;
                }
                Console.Write("\n\n   Skriv in ditt lösenord:  ");
                string inputPassword = Console.ReadLine();

                if ((user = User.CheckPassWord(UsersList, inputUser_ID.ToUpper(), inputPassword)) != null )
                {
                    CheckUserType(user);
                    Login();
                }
                attempts--;
                if (attempts == 1)
                {
                    Console.WriteLine("\n\t(( Observera! Du har bara " +
                                                "ett försök kvar! ))");
                }
                Console.Write("\n\tFelaktig kod !\tVar god och " +
                                               "försök ingen: ");
                if (attempts == 0)
                {
                    LockOut();
                }
            }
        }

        static void CheckUserType(User user)
        {
            if (user is Customer)
            {
                CustomerMenu(user);
            }
            else if (user is Admin)
            {
                AdminMenu(user);
            }
        }

        static void LockOut()
        {
            int seconds = 600;
            while (seconds >= 0)
            {
                Console.Clear();
                Console.Write("\tFör många misslyckade försök !" +
                   " Försök igen om tio minuter\n\n\n\t\t[ {0} ]\t\t", seconds);
                Thread.Sleep(1000);
                seconds--;
            }
            Console.Clear();
        }

        static void CustomerMenu(User user)
        {
            Customer customer = user as Customer;
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.Write("\n\t\t* (( Välkommen {0}" +
                                                                  " )) * \n\n" +
                    "  [1] konton och saldo \n\n" +
                    "  [2]  \n\n" +
                    "  [3]  \n\n" +
                    "  [4]  \n\n" +
                    "  [5] Öppna ett nytt konto \n\n" +
                    "  [6] Logga ut \n\n" +
                    "   \tVälj:  ", user.FullName);
                Int32.TryParse(Console.ReadLine(), out int option);
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine(Account.PrintAccounts(customer));
                        Account.SelectAccount(customer, customer._accounts.Count);
                        Redirecting();
                        break;
                    case 2:
                        Console.Clear();
                        // Transfer method  ??
                        Redirecting();
                        break;
                    case 3:
                        Console.Clear();
                        // Deposition method  ??  
                        Redirecting();
                        break;
                    case 4:
                        Console.Clear();
                        // Withdrawal method  ??
                        Redirecting();
                        break;
                    case 5: // Create new account as customer
                        Console.Clear();
                        customer.CreateNewAccount();
                        Redirecting();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\t\tVälkommen åter :-)");
                        Thread.Sleep(1800);
                        run = false;
                        break;
                    default:
                        Console.WriteLine("\t\tOgiltig inmatning!   " +
                        "Var god och välj från 1 - 6\n");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AdminMenu(User admin)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.Write("\n * Admin * \t\t*  (( Välkommen {0} " +
                                                                  " )) * \n\n" +
                    "  [1] Registrera en ny kund  \n\n" +
                    "  [2]  \n\n" +
                    "  [3]  \n\n" +
                    "  [4]  \n\n" +
                    "  [5]  \n\n" +
                    "  [6] Logga ut \n\n" +
                    "   \tVälj:  ", admin.FullName);
                Int32.TryParse(Console.ReadLine(), out int option);
                switch (option)
                {
                    case 1: // Create a new customer and account as Admin
                        Console.Clear();
                        Admin.CreateNewCustomer(UsersList);
                        Redirecting();
                        break;
                    case 2:
                        Console.Clear();
                        //   ??
                        Redirecting();
                        break;
                    case 3:
                        Console.Clear();
                        //   ??
                        Redirecting();
                        break;
                    case 4:
                        Console.Clear();
                        //   ??
                        Redirecting();
                        break;
                    case 5:
                        Console.Clear();
                        //   ??
                        Redirecting();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\t\tVälkommen åter :-)");
                        Thread.Sleep(1800);
                        run = false;
                        break;
                    default:
                        Console.WriteLine("\t\tOgiltig inmatning!   " +
                        "Var god och välj från 1 - 6\n");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void Redirecting()
        {
            Console.Write("\n\n\n\t\tKlicka 'Enter' för att komma till huvudmenyn");
            Console.ReadLine();
        }          
    }
}
