using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Timers;
using Art = TeamHamsterBank.HamsterArt;

namespace TeamHamsterBank
{
    class Bank
    {
        static System.Timers.Timer aTimer;
        internal static List<User> UsersList = new List<User>();
        internal static List<Task> UpcomingTransactions = new List<Task>();
        public static void Login()
        {           
            Console.Clear();
            //Console.Write("\n\n\t\t\tVälkommen till HamsterBanken\n\n\n" +
            //        "\tVar god och skriv in ditt Användar-ID:  ");
            Art.HeadLine("\n\n\t\t\tVälkommen till HamsterBanken\n\n\n");
            Console.Write("\tVar god och skriv in ditt Användar-ID:  ");
            int attempts = 3;
            string inputUser_ID = String.Empty;
            bool found = false;
            User user;
            while (attempts > 0)
            {
                if (!found)
                {
                    inputUser_ID = Console.ReadLine().ToUpper();
                    if (User.CheckUserName(UsersList, inputUser_ID))
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

                if ((user = User.CheckPassword(UsersList, inputUser_ID, inputPassword)) != null )
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
                Art.HeadLine($"\n\t\t\t* (( Välkommen {user.FullName} )) * \n\n\n");
                Console.Write("  [1] Konton och saldo \n\n" +
                    "  [2] Överföring\n\n" +
                    "  [3] Sätt in pengar \n\n" +
                    "  [4] Ta ut pengar \n\n" +
                    "  [5] Öppna ett nytt konto \n\n" +
                    "  [6] Byta lösenord \n\n" +
                    "  [7] Banklån \n\n" +
                    "  [8] Logga ut \n\n" +
                    "   \tVälj:  ");
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
                        if (customer._accounts.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\t\tDu har inget registrerat konto." +
                                                    "  Var god och öppna ett nytt konto");
                            Redirecting();
                            break;
                        }
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
                        break;
                    case 4: // Withdraw
                        Console.Clear();
                        Console.WriteLine(Account.PrintAccounts(customer));
                        Withdraw(customer);
                        break;
                    case 5: // Create new account as customer
                        Console.Clear();
                        customer.CreateNewAccount();
                        break;
                    case 6://Change password
                        Console.Clear();
                        if(VerifyCustomer(customer))
                        {
                            Console.Clear();
                            Art.HeadLine("\n\t**Ändra Lösenord**");
                            if(customer.ChangePassword())
                            {
                                Console.Clear();
                                Console.WriteLine("\n\n\n\t\tLösenordet är ändrat!");
                                Thread.Sleep(1800);
                            }
                        }                       
                        break;
                    case 7:
                        Console.Clear();
                        Account.BankLoan(customer);
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\t\tVälkommen åter :-)");
                        Thread.Sleep(1800);
                        run = false;
                        break;
                    default:
                        Console.WriteLine("\t\tOgiltig inmatning!   " +
                        "Var god och välj från 1 - 8\n");
                        Console.ReadKey();
                        break;
                }
                StoreAndLoad.SaveAccounts();
                StoreAndLoad.SaveTransactions();
            }
        }

        static void Deposit(Customer customer)
        {
            if (customer._accounts.Count == 0)
            {
                Console.WriteLine("\n\n\t\tDu har inget registrerat konto." +
                                        "  Var god och öppna ett nytt konto");
                Redirecting();
                return;
            }
            //Console.SetCursorPosition(0, 8);
            int index = 0;
            string input;
            while (index < 1 || index > customer._accounts.Count)
            {
                Console.Write("\n\n   Välj vilket konto du vill" +
                    " sätta in eller mata in 'R' för att avbryta!:   ");
                Int32.TryParse(input = Console.ReadLine(), out index);
                if (input.Trim().ToUpper() == "R")
                {
                    return;
                }
            }
            index += -1;
            decimal deposit = 0;
            while (deposit < 1)
            {
                Console.Write("\n\n   Var vänlig och bekräfta hur mycket" +
                    " du kommer sätta in:   ");
                Decimal.TryParse(input = Console.ReadLine(), out deposit);
                if (input.Trim().ToUpper() == "R")
                {
                    return;
                }

            }

            customer._accounts[index].Balance += deposit;
            string currency = customer._accounts[index].Currency;
            Console.Clear();
            Console.Write("\n\n   '{0}' har lagts till [{1}]", deposit,
                customer._accounts[index].AccountNumber);
            Account.SubmitTransaction("Insättning", customer, index, deposit, currency);
            Console.WriteLine(Account.PrintAccounts(customer));

            // Print amount after interest per year
            if (customer._accounts[index].AccountType == "Sparkonto")
            {
                Console.WriteLine("\n  Vill du se hur mycket nuvarande summa på sparkontot kommer att öka med vår sparränta? Svara 'Ja'/'Nej");

                bool runAgain = false;
                do 
                {
                    Console.Write("\n  ");
                    string answer = Console.ReadLine().ToLower().Trim();

                    if (answer == "ja")
                    {
                        Console.Write("\n  Ange antal år för ökning: ");
                        int years = 0;
                        Int32.TryParse(Console.ReadLine(), out years);
                        Console.Clear();
                        Console.WriteLine($"\n  Nuvarande summa {customer._accounts[index].Balance} {customer._accounts[index].Currency} på sparkontot kommer att vara värt:\n");
                        SavingsAccount.CalculateSavingsInterest(customer._accounts[index].Balance, years, false, customer._accounts[index].Currency);
                        runAgain = false;
                    }
                    else if (answer == "nej")
                    {
                        runAgain = false;
                    }
                    else if (answer != "nej" || answer != "ja")
                    {
                        Console.WriteLine("Svara 'Ja' eller 'Nej'");
                        runAgain = true;
                    }
                } while (runAgain);
            }
            Redirecting();
        }
        static void Withdraw(Customer customer)
        {
            if (customer._accounts.Count == 0)
            {
                Console.WriteLine("\n\n\t\tDu har inget registrerat konto." +
                                        "  Var god och öppna ett nytt konto");
                return;
            }
            int index = 0;
            string input;
            while (index < 1 || index > customer._accounts.Count)
            {
                Console.Write("\n\n   Välj vilket konto du vill" +
                    " ta ut från eller mata in 'R' för att avbryta!:   ");
                Int32.TryParse(input = Console.ReadLine(), out index);
                if (input.Trim().ToUpper() == "R")
                {
                    return;
                }
            }
            index += -1;
            decimal withdrawal = 0;
            decimal balance = customer._accounts[index].Balance;
            string currency = customer._accounts[index].Currency;

            if (customer._accounts[index].AccountType != "Kreditkonto") // if not a credit account
            {
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
                    Decimal.TryParse(input = Console.ReadLine(), out withdrawal);
                    if (input.Trim().ToUpper() == "R")
                    {
                        return;
                    }
                }
            }
            else // If credit account
            {
                decimal maxWithdrawal = 10000m;
                if (currency != "SEK") // Exchange the maximum amount to withdraw if not SEK
                {
                    Bank.ExchangeBack(ref maxWithdrawal, ref currency);
                }

                Console.Write($"\n   Maxsumman du kan ta ut är {maxWithdrawal.ToString("F")} {customer._accounts[index].Currency}\n" +
                    "   Var vänlig och bekräfta hur mycket du vill ta ut:   ");
                Decimal.TryParse(Console.ReadLine(), out withdrawal);

                while (withdrawal < 1 || withdrawal > maxWithdrawal)
                {
                    Console.Write("\n\n   Ogiltlig summa. Var vänlig och bekräfta hur mycket du vill ta ut:   ");
                    Decimal.TryParse(Console.ReadLine(), out withdrawal);
                }
                // Calculate and print out the debt for the withdrawal
                CreditAccount.CalculateCreditInterest(withdrawal, customer._accounts[index].Currency);
            }
                
            if (!VerifyCustomer(customer))
            {
                return;
            }
            customer._accounts[index].Balance -= withdrawal;
            Console.Clear();
            Console.Write("\n\n   '{0}'  har tagits bort från [{1}]", withdrawal,
                customer._accounts[index].AccountNumber);
            Account.SubmitTransaction("Uttag", customer, index, - withdrawal, currency);
            Console.WriteLine(Account.PrintAccounts(customer));
            Redirecting();
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
            if (customer._accounts.Count == 1)
            {
                Console.WriteLine("\n\n\t\tDu har bara ett registrerat konto." +
                                        "  Var god och öppna ett nytt konto");
                return;
            }
            int transferFrom;
            int transferTo;
            decimal transferSum;
            bool transferBool = true;
            string input;
            Console.Clear();
            ReturnInstruction(customer._accounts.Count+2);
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
                    ReturnInstruction(customer._accounts.Count +2);
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
                            string currency = customer._accounts[transferFrom].Currency;
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
                                        Console.WriteLine(Account.PrintAccounts(
                                            new Account[]{ customer._accounts[transferFrom],
                                                customer._accounts[transferTo]}));
                                        Account.SubmitTransaction("Överföring", customer,
                                            transferFrom, -transferSum, currency);
                                        Account.SubmitTransaction("Överföring",customer,
                                            transferTo, transferSum, currency);
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
        static void AdminMenu(User user)
        {
            Admin admin = user as Admin;
            bool run = true;
            while (run)
            {
                Console.Clear();
                Art.HeadLine($"\n  * Admin * \t\t\t*  (( Välkommen {admin.FullName} )) * \n\n\n");
                Console.Write("  [1] Registrera en ny kund  \n\n" +
                    "  [2] Uppdatera växelkurser för alla valutor  (API-samtal)\n\n" +
                    "  [3] Sätt in växelkurser för en valuta \n\n" +
                    "  [4] Ändra en annan användares lösenord \n\n" +
                    "  [5] Ändra eget lösenord \n\n" +
                    "  [6] Ta bort en användare från systemet \n\n" +
                    "  [7] Logga ut \n\n" +
                    "   \tVälj:  ");
                Int32.TryParse(Console.ReadLine(), out int option);
                switch (option)
                {
                    case 1: // Create a new customer and account as Admin
                        Console.Clear();
                        Admin.CreateNewCustomer(UsersList);
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
                        break;
                    case 4://Change other users password
                        Console.Clear();
                        admin.ChangeUserPassword(UsersList);
                        break;
                    case 5://Change admin password
                        Console.Clear();
                        Art.HeadLine("\n\t**Ändra Lösenord**");
                        if (admin.ChangePassword())
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n\t\tLösenordet är ändrat!");
                            Thread.Sleep(1800);
                        }
                        break;
                    case 6:
                        Admin.DeleteUser();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\t\tVälkommen åter :-)");
                        Thread.Sleep(1800);
                        run = false;
                        break;
                    default:
                        Console.WriteLine("\t\tOgiltig inmatning!   " +
                        "Var god och välj från 1 - 7\n");
                        Console.ReadKey();
                        break;
                }
                StoreAndLoad.SaveUsers();
                StoreAndLoad.SaveAccounts();
                StoreAndLoad.SaveTransactions();
            }
        }
        
        public static void Redirecting()
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
                                string currency = customer._accounts[fromAccount].Currency;
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
                                                MakeExternalTransfer(transferSum,
                                                toAccount);
                                                Console.Clear();
                                                Console.WriteLine("\nÖverföring " +
                                                    "genomförd!\n\nNytt saldo är:\n");
                                                Console.WriteLine(Account.PrintAccounts(customer._accounts[fromAccount]));
                                                UpcomingTransactions.Add(new Task(() =>
                                                {
                                                    Account.SubmitTransaction(customer.FullName, toCustomer,
                                                    toCustomer._accounts.FindIndex
                                                    (a => a.Equals(toAccount)), transferSum, currency);
                                                }));
                                                Account.SubmitTransaction(toCustomer.FullName, customer,
                                                    fromAccount, -transferSum, currency);
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
        public static void ReturnInstruction(int addRow)
        {
            Console.SetCursorPosition(5, 15 + addRow);
            Console.Write("Mata in \"R\" för att avbryta!");
            Console.SetCursorPosition(0, 0);
        }

        static async void UpdateEchangeRates()
        {
            string updateDate = string.Empty;
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
                updateDate = $"\n\t\t\tUppdaterat {DateTime.Now}\n\n";
                Art.HeadLine2(updateDate);
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
            Art.HeadLine("\n\tAktuell valutakurs för Svenska kronor (SEK) \n\n");
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
                    transfer *= decimal.Parse(_currency[1], CultureInfo.InvariantCulture);
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
                    transfer /= decimal.Parse(_currency[1], CultureInfo.InvariantCulture);
                    return;
                }
            }
        }
        public static string GetPassword()
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
        internal static void TransactionProcessTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(900000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            foreach (Task task in UpcomingTransactions)
            {
                task.Start();
            }
            UpcomingTransactions.Clear();
            StoreAndLoad.SaveAccounts();
            StoreAndLoad.SaveTransactions();
        }
    }
}
