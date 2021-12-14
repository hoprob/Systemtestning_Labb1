using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Art = TeamHamsterBank.HamsterArt;

namespace TeamHamsterBank
{
    static partial class Bank
    {
        static System.Timers.Timer aTimer;
        internal static List<Task> UpcomingTransactions = new List<Task>();
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
        static string GetRequest(string ISO_Code)
        {
            StringBuilder response = new StringBuilder();
            response.Append("https://free.currconv.com/api/v7/convert?q=");
            response.Append(ISO_Code);
            response.Append("_SEK&compact=ultra&apiKey=b9ab32024407ef485ccf");

            return response.ToString();
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
        internal static void TransactionProcessTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(900000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
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
    }
}
