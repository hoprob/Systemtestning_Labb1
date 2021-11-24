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
            int customerIndex = -1;
            bool found = false;
            while (attempts > 0)
            {
                /*Vid varje loop ska frågas användarnamnet om , och om
                namnet blir hittat, ska hoppa över att fråga om pinkoden */
                if (!found)
                {
                    inputUser_ID = Console.ReadLine().ToUpper();
                    for (int i = 0; i < UsersList.Count; i++)
                    {
                        if (user_ID == UsersList[i].CheckUserName().ToUpper())
                        {
                            inputUser_ID = UsersList[i].CheckUserName();
                            customerIndex = i;
                            founded = true;
                        }
                    }
                }
                if (!found)
                {
                    Console.Write("\n\n   Användar-ID kan " +
                        "inte hittas. Var god och Försök igen:  ", username);
                    continue;
                }
                Console.Write("\n\n   Skriv in ditt lösenord:  ");
                string inputPassword = Console.ReadLine();
                if (inputPassword == UsersList[customerIndex].CheckPinCode())
                {
                    CheckUserType(UsersList[customerIndex]);
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
                CustomerMenu(user)
            }
            else if (user is Admin)
            {
                AdminMenu(user)
            }
            else
            {
                throw new Exception("Fel med klasstypen :(    Fixa !!!!!!!!!!!    ")
            }
        }

        static void LockOut()
        {
            int seconds = ;
            while (seconds >= 600)
            {
                Console.Clear();
                Console.Write("\tFör många misslyckade försök !" +
                   " Försök igen om tio minuter\n\n\n\t\t[ {0} ]\t\t", seconds);
                Thread.Sleep(1000);
                seconds--;
            }
            Console.Clear();
        }

        static void CustomerMenu(Customer customer)
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
                    "   \tVälj:  ", customer.ReturnFullName());
                Int32.TryParse(Console.ReadLine(), out int option);
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine(customer);
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

        static void AdminMenu(Admin admin)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.Write("\n\t\t* (( Välkommen {0} \t\t* Admin *" +
                                                                  " )) * \n\n" +
                    "  [1] Registrera en ny kund  \n\n" +
                    "  [2]  \n\n" +
                    "  [3]  \n\n" +
                    "  [4]  \n\n" +
                    "  [5]  \n\n" +
                    "  [6] Logga ut \n\n" +
                    "   \tVälj:  ", admin.ReturnFullName());
                Int32.TryParse(Console.ReadLine(), out int option);
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        RegisterNewUser();
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

        static void RegisterNewUser()
        {
            RegistrationMenu(out string username_input, out string password_input)
            UsersList.Add(new Customer(username_input, password_input));
        }

        static void RegistrationMenu(out string username_input, out string password_input)
        {
            // User_ID  will be given automatically upon the object creation 
            string username_input = String.Empty;
            string password_input = String.Empty;
            Console.Write("\n\n   Skriv in fullständiga namn:  ");
            username_input = Console.ReadLine().Trim();
            while (username_input.Any(char.IsDigit) || username_input.Length > 2)
            {
                Console.Write("   Ogiltigt namn! var god och försök igen\n\n"+
                    "\n\n   Skriv in fullständiga namn:  ");
                username_input = Console.ReadLine().Trim());
            }            
            
            Console.Write("\n\n   Skriv in ett lösenord:  ");
            password_input = Console.ReadLine().Trim();
            while (password_input.Length > 8)
            {
                Console.Write("   Lösenordet måste vara minst 8-tecken\n\n"+
                    "\n\n   Skriv in ett lösenord:  ");
                password_input = Console.ReadLine().Trim());
            }
        }

        static void Redirecting()
        {
            Console.Write("\n\n\n\t\tKlicka 'Enter' för att komma till huvudmenyn");
            Console.ReadLine();
        }
        
    }
}