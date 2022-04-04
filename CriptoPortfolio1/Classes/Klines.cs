using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CriptoPortfolio1.Classes
{
    class Klines
    {
        List<Bar_Class> Bar = new List<Bar_Class>();

        public double price = 0;
        public double high = 0;
        public double low = 0;

        public List<Bar_Class> binance(string symbol)
        {
            Bar.Clear();


            string request = GET("https://api.binance.com/api/v1/klines?symbol=" + symbol + "&interval=1d&limit=14");


            if (request != "исключения")
            {
                var Data = JsonConvert.DeserializeObject<double[][]>(request);

                if (Data.Length - 1 > 0) low = Data[0][3];
                for (int i = 0; i < Data.Length; i++)
                {

                    Bar.Add(new Bar_Class(Data[i][1], Data[i][2], Data[i][3], Data[i][4]));

                    if(Data.Length - 1 == i)  price = Data[i][4];

                    if (Data[i][2] > high) high = Data[i][2];
                    if (Data[i][3] > low) high = Data[i][3];
                }

            }

            return Bar;


        }

        string GET(string Url)
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


    }
}
