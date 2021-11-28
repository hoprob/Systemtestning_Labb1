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
                    Customer customer = user as Customer;

                    customer._accounts.Add(new Account(
                        "Allkonto         ", 50000, "[SEK]"));
                    customer._accounts.Add(new Account(
                        "Sparkonto_       ", 1120.220m, "[EUR]"));
                    customer._accounts.Add(new Account(
                        "Framtidskonto    ", 15000, "[GBP]"));
                    customer._accounts.Add(new Account(
                        "Investeringskonto", 30000, "[USD]"));
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

                if ((user = User.CheckPassword(UsersList, inputUser_ID.ToUpper(), inputPassword)) != null )
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
                    "  [1] Konton och saldo \n\n" +
                    "  [2]  \n\n" +
                    "  [3] Sätt in pengar \n\n" +
                    "  [4] Ta ut pengar \n\n" +
                    "  [5] Öppna ett nytt konto \n\n" +
                    "  [6] Logga ut \n\n" +
                    "   \tVälj:  ", user.FullName);
                Int32.TryParse(Console.ReadLine(), out int option);
                switch (option)
                {
                    case 1: // View accounts and balance
                        Console.Clear();
                        Console.WriteLine(Account.PrintAccounts(customer));
                        Account.SelectAccount(customer, customer._accounts.Count);
                        Redirecting();
                        break;
                    case 2: // Transfer money
                        Console.Clear();
                        Redirecting();
                        break;
                    case 3: // Deposit
                        Console.Clear();
                        Console.WriteLine(Account.PrintAccounts(customer));
                        Deposit(customer);
                        Redirecting();
                        break;
                    case 4: // Withdraw
                        Console.Clear();
                        Console.WriteLine(Account.PrintAccounts(customer)); 
                        Withdraw(customer);
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

        static void Deposit(Customer customer)
        {
            int index = 0;
            while (index < 1 || index > customer._accounts.Count)
            {
                Console.Write("\n\n   Välj vilket konto du vill" +
                    " sätta in:   ");
                Int32.TryParse(Console.ReadLine(), out index);
            }
            index += -1;
            decimal deposit = 0;
            while (deposit < 1)
            {
                Console.Write("\n\n   Var vänlig och bekräfta hur mycket" +
                    " du kommer sätta in:   ");
                Decimal.TryParse(Console.ReadLine(), out deposit);
            }

            customer._accounts[index].Balance += deposit;
            Console.Clear();
            Console.Write("\n\n   '{0}' har lagts till [{1}]", deposit,
                customer._accounts[index].AccountNumber);
            Account.SubmitTransaction(customer, index, deposit);
            Console.WriteLine(Account.PrintAccounts(customer));
        }
        static void Withdraw(Customer customer)
        {
            int index = 0;
            while (index < 1 || index > customer._accounts.Count)
            {
                Console.Write("\n\n   Välj vilket konto du vill" +
                    " ta ut från:   ");
                Int32.TryParse(Console.ReadLine(), out index);
            }
            index += -1;
            decimal withdrawal = 0;
            decimal balance = customer._accounts[index].Balance;
            if (balance < 1)
            {
                Console.Write("\n\n   Det går inte att ta ut pengar.\t Kontot är tomt !");
                Console.ReadKey();
                return;
            }
            while (!customer._accounts[index].EnoughBalance(withdrawal) || withdrawal < 1)
            {
                Console.Write("\n\n   Maxvärdet du kan ta ut är [{0}]\n  " +
                    "\n   Var vänlig och bekräfta hur mycket du vill" +
                    " ta ut:   ", balance);
                Decimal.TryParse(Console.ReadLine(), out withdrawal);
            }
            if (!VerifyCustomer(customer))
            {
                return;
            }
            customer._accounts[index].Balance -= withdrawal;
            Console.Clear();
            Console.Write("\n\n   '{0}'  har tagits bort från [{1}]", withdrawal,
                customer._accounts[index].AccountNumber);
            Account.SubmitTransaction(customer, index, - withdrawal);
            Console.WriteLine(Account.PrintAccounts(customer));
        }

        static bool VerifyCustomer(Customer customer)
        {
            int attempts = 3;
            bool valid = false;
            while (attempts > 0)
            {
                attempts--;
                Console.Write("\n\n   Skriv in ditt lösenord för att verifiera dig:  ");
                string inputPassword = Console.ReadLine().Trim();
                if (User.CheckPassword(customer, inputPassword))
                {
                    valid = true;
                    break;
                }
                if (attempts == 1)
                {
                    Console.WriteLine("\n\t(( Observera! Du har bara " +
                                                "ett försök kvar! ))");
                }
                Console.Write("\n\tFelaktig kod !\tVar god och " +
                                                   "försök ingen: ");
            }
            return valid;
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
