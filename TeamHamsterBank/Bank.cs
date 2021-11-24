using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace TeamHamsterBank
{
    class Bank
    {
        internal static List<User> UsersList = new List<User>();
        public static void WriteAccounts()
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
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(8, 5);
            Console.Write("Du matade in fel uppgifter 3 gånger!");
            Console.SetCursorPosition(11, 6);
            Console.Write("Programmet är nu låst i 1 min!");
            Console.SetCursorPosition(10, 8);
            Console.Write("╔══════════════════════════════╗");
            Console.SetCursorPosition(10, 9);
            Console.Write("║------------------------------║");
            Console.SetCursorPosition(10, 10);
            Console.Write("╚══════════════════════════════╝");
            for (int i = 0; i < 30; i++)
            {
                Console.SetCursorPosition(i + 11, 9);
                Console.Write("█");
                Thread.Sleep(2000);
            }
            Console.CursorVisible = true;
        }

        static void CustomerMenu(User customer)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.Write("\n\t\t* (( Välkommen {0}" +
                                                                  " )) * \n\n" +
                    "  [1] Se dina konton och saldo \n\n" +
                    "  [2]  \n\n" +
                    "  [3]  \n\n" +
                    "  [4]  \n\n" +
                    "  [5] Öppna ett nytt konto \n\n" +
                    "  [6] Logga ut \n\n" +
                    "   \tVälj:  ", customer.FullName);
                Int32.TryParse(Console.ReadLine(), out int option);
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        // Account details - method  
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
                    case 5:
                        Console.Clear();
                        // Open new account method  ??
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
                    case 1:
                        Console.Clear();
                                           // RegisterNewUser();
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
