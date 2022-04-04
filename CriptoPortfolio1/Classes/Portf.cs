using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Threading; //Именно это пространство имен поддерживает многопоточность


namespace CriptoPortfolio1.Classes
{
    static class Portf
    {
        static public double rub = 0;
        static public bool rubF = true;
        static public double price = 0;
        static public double sum_last_usd = 0;
        static public double sum_last_rub = 0;

        static public string request = null;

        public class bodi
        {

            public double price { get; set; }
            public string symbol { get; set; }

        }

        public class bodi2
        {

            public symbolscl[] symbols;

        }

        public class symbolscl
        {
            public string symbol { get; set; }
            public string status { get; set; }

        }

        static public List<string> symbols = new List<string>();

        static public List<Coinnn> Coin = new List<Coinnn>();

        public class Coinnn
        {

            public string Coinn;

            public double amount;

            public double price;

            public double profit = 0;

            public Coinnn(string Coinn, double amount, double price)
            {
                this.Coinn = Coinn;

                this.amount = amount;

                this.price = price;

                profit = price * amount;

            }

        }

        public delegate void delegUp_coin(); // 1. Объявляем делегат
             
        static  public event delegUp_coin evenUp_coin;

        static public void myUp_coin_()
        {
            //  Thread myThread = new Thread(Up_coin); //Создаем новый объект потока (Thread)
            //  myThread.Start(); //запускаем поток
            sum_last_usd = 0;
            rub = binance("USDTRUB");

            foreach (Coinnn Coin2 in Coin)
            {
                Coin2.price = binance(Coin2.Coinn);

                Coin2.profit = Coin2.price * Coin2.amount;

                sum_last_usd = sum_last_usd + Coin2.profit;
            }

            sum_last_rub = sum_last_usd * rub;

            evenUp_coin();
        }

        static public void Up_coin()
        {
            sum_last_usd = 0;
            rub = binance("USDTRUB");

            foreach (Coinnn Coin2 in Coin)
            {
                Coin2.price = binance(Coin2.Coinn);

                Coin2.profit = Coin2.price * Coin2.amount;

                sum_last_usd = sum_last_usd + Coin2.profit;
            }

            sum_last_rub = sum_last_usd * rub;

            evenUp_coin();
        }

        static public void add(string coin, double Amount, double Price)
        {
            Coin.Add(new Coinnn(coin, Amount, Price));

        }

        static  public double binance(string symbol)
        {


            request = GET("https://api.binance.com/api/v3/ticker/price?symbol=" + symbol);


            if (request != "исключения")
            {
                bodi Data = JsonConvert.DeserializeObject<bodi>(request);



                return Data.price;

            }
            else return 1;
        }

        private static string GET(string Url)
        {
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.Stream stream = resp.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                string Out = sr.ReadToEnd();
                sr.Close();
                return Out;
            }
            catch (Exception)
            {
                // MessageBox.Show("Сообщение", "Поверх всех окон", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                // Console.WriteLine("Ошибка: " + ex.Message);
                return "исключения"; ;
            }
            finally
            {
                // Console.WriteLine("Блок finally");

            }

        }

        static public void Up_coinAll()
        {
            evenUp_coin();
        }

        static public void binance_info()
        {


            request = GET("https://api.binance.com/api/v1/exchangeInfo");


            if (request != "исключения")
            {
                bodi2 Data = JsonConvert.DeserializeObject<bodi2>(request);


                foreach (symbolscl D in Data.symbols)
                {
                    symbols.Add(D.symbol);
                }

            }

        }

        static public bool binance_symbols(string symbol)
        {
            foreach (string Sl in symbols)
            {
                if (Sl == symbol && usdt(Sl)) return true;
            }

            return false;
        }

        static public bool usdt(string str)
        {
            char[] ar = str.ToArray<char>();

            if (ar.Length > 4)
            {
                int i = ar.Length;
                if (ar[i - 4] == 'U' && ar[i - 3] == 'S' && ar[i - 2] == 'D' && ar[i - 1] == 'T')
                {
                    return true;
                }

            }
            return false;
        }
    }
}
