using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace TeamHamsterBank
{
    class Bank
    {
        internal static List<User> UsersList = new List<User>();
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
                string inputPassword = GetPassword();

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
                    "  [2] Överföring\n\n" +
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
                        string input;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n\t\tVilken typ av överföring vill du göra?");
                            Console.WriteLine("\n\n[1] Till eget konto\n\n[2] Till externt konto\n\n[3] Avbryt");
                            Console.Write("\n\n\tVälj: ");
                            input = Console.ReadLine();
                        } while (input != "1" && input != "2" && input != "3");
                        if(input == "1") { InternalTransfer(customer); }
                        else if (input == "2") { ExternalTransfer(customer); }                        
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
                StoreAndLoad.SaveAccounts();
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
                string inputPassword = GetPassword();
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
        static void InternalTransfer(Customer customer)
        {
            int transferFrom;
            int transferTo;
            decimal transferSum;
            bool transferBool = true;
            string input;
            Console.Clear();
            ReturnInstruction(customer._accounts.Count);
            do
            {
                Console.WriteLine("Vilket konto vill du föra över FRÅN?");
                Console.WriteLine(Account.PrintAccounts(customer));
                Console.Write("\n\tVälj: ");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out transferFrom)
                    && transferFrom <= customer._accounts.Count() &&
                    transferFrom > 0)
                {
                    transferFrom--;
                    Console.Clear();
                    ReturnInstruction(customer._accounts.Count);
                    do
                    {
                        Console.WriteLine("Vilket konto vill du överföra TILL?");
                        Console.WriteLine(Account.PrintAccounts(customer));
                        Console.Write("\n\tVälj: ");
                        input = Console.ReadLine();
                        if (Int32.TryParse(input, out transferTo) &&
                            transferTo <= customer._accounts.Count()
                            && transferTo > 0
                            && transferFrom + 1 != transferTo)
                        {
                            transferTo--;
                            Console.Clear();
                            ReturnInstruction(0);
                            do
                            {
                                Console.Write("\n\tHur mycket vill du föra över?: ");
                                input = Console.ReadLine();
                                if (Decimal.TryParse(input,
                                    out transferSum))
                                {
                                    if (customer._accounts[transferFrom].
                                        EnoughBalance(transferSum))
                                    {
                                        customer._accounts[transferFrom].
                                            MakeTransfer(transferSum,
                                            customer._accounts[transferTo]);
                                        Console.Clear();
                                        Console.WriteLine("\nÖverföring genomförd!" +
                                            "\n\nNya saldon är: \n");
                                        Console.WriteLine(customer.
                                            _accounts[transferFrom]);
                                        Console.WriteLine(customer.
                                            _accounts[transferTo]);
                                        Account.SubmitTransaction(customer,
                                            transferFrom, transferSum);
                                        Account.SubmitTransaction(customer,
                                            transferTo, transferSum);
                                        transferBool = false;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        ReturnInstruction(0);
                                        Console.WriteLine("\n\tDu har inte" +
                                            " tillräckligt med pengar på kontot!" +
                                            $"\n\tDu kan max föra över" +
                                            $" {customer._accounts[transferFrom].Balance}!\n");
                                        transferBool = true;
                                    }
                                }
                                else if (input.ToUpper() == "R") { transferBool = false; }
                                else
                                {
                                    Console.Clear();
                                    ReturnInstruction(0);
                                    Console.WriteLine("\n\tOgiltlig inmatning!" +
                                        " Skriv in summa med siffror!\n");
                                    transferBool = true;
                                }
                            } while (transferBool);
                        }
                        else if (input.ToUpper() == "R") { transferBool = false; }
                        else
                        {
                            Console.Clear();
                            ReturnInstruction(customer._accounts.Count);
                            Console.WriteLine("\n\tOgiltligt val! Försök igen!\n");
                            transferBool = true;
                        }
                    } while (transferBool);
                }
                else if (input.ToUpper() == "R") { transferBool = false; }
                else
                {
                    Console.Clear();
                    ReturnInstruction(customer._accounts.Count);
                    Console.WriteLine("\n\tOgiltligt val! Försök igen!\n");
                    transferBool = true;
                }
            } while (transferBool);
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
                    "  [2] Uppdatera växelkurser för alla valutor  (API-samtal)\n\n" +
                    "  [3] Sätt in växelkurser för en valuta \n\n" +
                    "  [4] Logga ut \n\n" +
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
                        UpdateEchangeRates();
                        Thread.Sleep(2450);
                        Redirecting();
                        break;
                    case 3:
                        Console.Clear();
                        Admin.SetCurrencyRate();
                        Redirecting();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\t\tVälkommen åter :-)");
                        Thread.Sleep(1800);
                        run = false;
                        break;
                    default:
                        Console.WriteLine("\t\tOgiltig inmatning!   " +
                        "Var god och välj från 1 - 4\n");
                        Console.ReadKey();
                        break;
                }
                StoreAndLoad.SaveUsers();
                StoreAndLoad.SaveAccounts();
            }
        }
        
        static void Redirecting()
        {
            Console.Write("\n\n\n\t\tKlicka 'Enter' för att komma till huvudmenyn");
            Console.ReadLine();
        }
        static void ExternalTransfer(Customer customer)
        {
            int toAccountNum;
            Customer toCustomer = null;
            Account toAccount = null;
            string input;
            bool transferBool;
            Console.Clear();
            ReturnInstruction(0);
            do
            {
                //Gets account number to transfer to
                transferBool = true;              
                Console.Write("\n\n\tSkriv in vilket kontonummer du vill" +
                    " överföra TILL: ");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out toAccountNum) && input.Length == 9)
                {
                    /*Gets list of customers from UsersList and
                     * checks if accountnumber exists*/
                    List<User> customerList = UsersList.FindAll(u => u is Customer);
                    foreach (User user in customerList)
                    {
                        toCustomer = user as Customer;
                        for (int i = 0; i < toCustomer._accounts.Count; i++)
                        {
                            if (toCustomer._accounts[i].AccountNumber == toAccountNum)
                            {
                                //Saves found account in object.
                                toAccount = toCustomer._accounts[i];
                                break;
                            }
                        }
                        //If toAccount not equals null, we got a searchresult!
                        if (toAccount != null) { break; }
                    }
                    if (toAccount != null)
                    {
                        Console.Clear();
                        ReturnInstruction(customer._accounts.Count + 3);
                        //Gets account to transfer from
                        do
                        {
                            int fromAccount;
                            Console.WriteLine($"\n\n\tKontonummer: [{toAccountNum}]" +
                                $" tillhör [{toCustomer.FullName}]");                            
                            Console.WriteLine("\n\n\tVälj vilket konto du vill" +
                                " överföra FRÅN:");
                            Console.WriteLine(Account.PrintAccounts(customer));                            
                            Console.Write("\n\tVälj: ");
                            input = Console.ReadLine();
                            if (Int32.TryParse(input, out fromAccount) &&
                                fromAccount <= customer._accounts.Count() &&
                                fromAccount > 0)
                            {
                                fromAccount--;
                                Console.Clear();
                                ReturnInstruction(0);
                                //Gets transferSum
                                do
                                {
                                    decimal transferSum;                                    
                                    Console.Write("\n\n\tHur mycket vill du föra" +
                                        " över?: ");
                                    input = Console.ReadLine();
                                    if (Decimal.TryParse(input,
                                        out transferSum) && transferSum > 0)
                                    {
                                        if (customer._accounts[fromAccount].
                                            EnoughBalance(transferSum))
                                        {
                                            Console.Clear();
                                            //Verifies customer with password
                                            if(VerifyCustomer(customer))
                                            {
                                                //Makes transfer and prints to log.
                                                customer._accounts[fromAccount].
                                                MakeTransfer(transferSum,
                                                toAccount);
                                                Console.Clear();
                                                Console.WriteLine("\nÖverföring " +
                                                    "genomförd!\n\nNytt saldo är:\n");
                                                Console.WriteLine(customer.
                                                    _accounts[fromAccount]);
                                                Account.SubmitTransaction(customer,
                                                    fromAccount, transferSum);
                                                Account.SubmitTransaction(toCustomer,
                                                    toCustomer._accounts.FindIndex
                                                    (a => a.Equals(toAccount)),
                                                    transferSum);
                                                transferBool = false;
                                            } 
                                            else { transferBool = false; }
                                        }
                                        else if(input.ToUpper() == "R")
                                            transferBool = false;
                                        else
                                        {
                                            Console.Clear();
                                            ReturnInstruction(0);
                                            Console.WriteLine("\n\tDu har inte" +
                                                " tillräckligt med pengar på kontot!" +
                                                "\n\tDu kan max föra över" +
                                                $" {customer._accounts[fromAccount].Balance}" +
                                                $"!\n");
                                            transferBool = true;
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        ReturnInstruction(0);
                                        Console.WriteLine("\n\tOgiltlig inmatning!" +
                                            " Skriv in summa med siffror!\n");
                                        transferBool = true;
                                    }
                                } while (transferBool);
                            }
                            else if (input.ToUpper() == "R")
                                transferBool = false; 
                            else
                            {
                                Console.Clear();
                                ReturnInstruction(customer._accounts.Count + 3);
                                Console.WriteLine("\n\tOgiltligt val! Försök igen!\n");
                                transferBool = true;
                            }
                        } while (transferBool);
                        
                    }
                    else if (input.ToUpper() == "R") { transferBool = false; }
                    else
                    {
                        Console.Clear();
                        ReturnInstruction(0);
                        Console.WriteLine($"\n\n\t\tKontonummer: {toAccountNum}," +
                            $" går Ej att hitta!\n\t\tVar vänlig försök med" +
                            $" annat kontonummer!\n");
                        transferBool = true;
                    }
                }
                else if (input.ToUpper() =="R") { transferBool = false; }
                else
                {
                    Console.Clear();
                    ReturnInstruction(0);
                    Console.WriteLine("\n\n\t\tFel inmatning! Skriv in" +
                        " kontonummer med 9st siffror!\n");
                    transferBool = true;
                }
            } while (transferBool);
        }
        static void ReturnInstruction(int addRow)
        {
            Console.SetCursorPosition(5, 15 + addRow);
            Console.Write("Mata in \"R\" för att avbryta!");
            Console.SetCursorPosition(0, 0);
        }

        static async void UpdateEchangeRates()
        {
            Console.WriteLine("\n\n\t\tHämtar uppgifter.........");
            try
            {
                HttpClient client = new HttpClient();
                foreach (string[] currency in Account.CurrencyList)
                {
                    string respons = await client.GetStringAsync(GetRequest(currency[0]));
                    currency[1] = respons.Substring(11).Replace("}", "");
                }
                Console.Clear();
                Console.WriteLine("\n\t\t\tUppdaterat {0}\n\n", DateTime.Now);
                PrintCurrentExchange();
            }
            catch (Exception)
            {
                Console.WriteLine("\n\tKontrollera din internetanslutning" +
                                    " eller testa igen om en timme");
            }
        }
        static string GetRequest(string ISO_Code)
        {
            StringBuilder response = new StringBuilder();
            response.Append("https://free.currconv.com/api/v7/convert?q=");
            response.Append(ISO_Code);
            response.Append("_SEK&compact=ultra&apiKey=b9ab32024407ef485ccf");

            return response.ToString();
        }
        internal static void PrintCurrentExchange()
        {
            Console.WriteLine("\tAktuell valutakurs för Svenska kronor (SEK) \n\n");
            for (int i = 0; i < Account.CurrencyList.Count; i++)
            {
                if (Account.CurrencyList[i][0] == "SEK")
                {
                    continue;
                }
                Console.WriteLine($"   [{i}]    [{ Account.CurrencyList[i][0]}]" +
                                             $"    {Account.CurrencyList[i][1]}\n");
            }
        }
        public static void ExchangeCurrency(ref decimal transfer, ref string currency)
        {
            foreach (string[] _currency in Account.CurrencyList)
            {
                if (currency == "SEK")
                {
                    return;
                }
                else if (_currency[0] == currency)
                {
                    transfer *= decimal.Parse(_currency[1]);
                    return;
                }
            }
        }
        public static void ExchangeBack(ref decimal transfer, ref string currency)
        {
            foreach (string[] _currency in Account.CurrencyList)
            {
                if (currency == "SEK")
                {
                    return;
                }
                else if (_currency[0] == currency)
                {
                    transfer /= decimal.Parse(_currency[1]);
                    return;
                }
            }
        }
        static string GetPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;
            //Gets a char from each keypress until user press Enter
            while ((key = Console.ReadKey()).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace)
                {
                    //Enables user to use backspace if string is 1 or more chars.
                    if (password.Length > 0)
                    {
                        Console.Write(" \b");
                        password.Remove(password.Length - 1, 1);
                    }
                    else
                    {
                        //Ignores backspace if string has no chars
                        Console.CursorLeft = Console.CursorLeft + 1;
                        continue;
                    }                   
                }
                //If input key is whitespace or control key, it ignores the input
                else if (char.IsControl(key.KeyChar) || char.IsWhiteSpace(key.KeyChar))
                {
                    Console.CursorLeft = Console.CursorLeft - 1;
                }
                else
                {
                    //Erases char from console, replace it with * and saves char to string
                    Console.Write("\b");
                    Console.Write("*");
                    password.Append(key.KeyChar);
                }
            }
            return password.ToString();
        }
    }
}
