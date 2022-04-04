using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CriptoPortfolio1.Classes;

namespace CriptoPortfolio1.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Portfolio_Page : ContentPage
    {

        double size_Width = Device.Info.ScaledScreenSize.Width;
        double sw = 0;
        string PortfoStr = "";
        public bool rubF = true;
        Button buttonadd;
        Button buttonsave;
        Button buttondel;
        Button buttonchange;
        Switch switcher;
        TableView table;
        TableSection mainSection;
        TableView table2;
        TableSection mainSection2;
        Entry CoinEntry;
        Entry AmountEntry;
        Label label1;
        Label label2;
        string coinstr = "";
        string Amountstr = "0";

        public Portfolio_Page()
        {
            InitializeComponent();

            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = 30},
                 new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                 new RowDefinition { Height =220 }

            },
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },

            }
            };

            label1 = new Label();
            label2 = new Label();

            Portf.rubF = rubFSave_Loading(2, true);

            sw = size_Width / 3;

            switcher = new Switch
            {
                IsToggled = rubFSave_Loading(2, true),
                Margin = new Thickness(220, 26, 80,150),
             
            };

            label1 = new Label
            {
                Margin = new Thickness(5, 2),
                Text = "загрузка данных",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.Black
            };

            label2 = new Label
            {
                Margin = new Thickness(5, 30),
                Text = "отображать в рублях:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.Black
            };

            buttonadd = new Button
            {
                Text = "Добавить",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                BorderWidth = 3,
                CornerRadius = 50,
                BackgroundColor = Color.Aqua,
                Margin = new Thickness(4, 81, (sw * 2) + 2, 71),
                TextColor = Color.Black
            };

            buttondel = new Button
            {
                Text = "Удалить",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                BorderWidth = 3,
                CornerRadius = 50,
                BackgroundColor = Color.Aqua,
                Margin = new Thickness(4, 152, (sw * 2) + 2, 3),
                TextColor = Color.Black
            };

            buttonsave = new Button
            {
                Text = "Сохранить",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                BorderWidth = 3,
                CornerRadius = 50,
                BackgroundColor = Color.Aqua,
                Margin = new Thickness(sw + 2, 152, sw + 4, 3),
                TextColor = Color.Black
            };

            buttonchange = new Button
            {
                Text = "Изменить",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                BorderWidth = 3,
                CornerRadius = 50,
                BackgroundColor = Color.Aqua,
                Margin = new Thickness(sw + 2, 81, sw + 4, 71),
                TextColor = Color.Black
            };


            CoinEntry = new Entry { Text = "", Placeholder = "Название", PlaceholderColor = Color.Black, TextColor = Color.Black, Keyboard = Keyboard.Default, Margin = new Thickness((sw * 2) + 2, 105, 10, 70) };
            AmountEntry = new Entry { Text = "", Placeholder = "Количество", PlaceholderColor = Color.Black, TextColor = Color.Black, Keyboard = Keyboard.Numeric, Margin = new Thickness((sw * 2) + 2, 150, 10, 10) };

            #region // подписка на события

            switcher.Toggled += switcher_Toggled;
            CoinEntry.TextChanged += CoinEntry_TextChanged;
            AmountEntry.TextChanged += AmountEntry_TextChanged;

            buttonadd.Clicked += OnButtonClickedadd;
            buttondel.Clicked += OnButtonClickeddel;
            buttonsave.Clicked += OnButtonClickedsave;
            buttonchange.Clicked += OnButtonClickedchange;

            Portf.evenUp_coin += Up_coin;

            #endregion

           
            Portfolio_Save_Loading(2);
            pars_str(1, PortfoStr);
            //  Portf.add("BTCUSDT", "0.005", "30000"); Portf.add("EHTUSDT", "121", "1200"); Portf.add("XRPUSDT", "600", "0.5999"); Portf.add("ADAUSDT", "160", "1.5999");

            #region // table 1,2

            table = new TableView();
            table.Intent = TableIntent.Settings;
            mainSection = new TableSection();

            table2 = new TableView();
            table2.Intent = TableIntent.Settings;
            mainSection2 = new TableSection();
            #endregion

            mainSection.Clear();
            mainSection2.Clear();

            mainSection2.Add(new TextCell { Text = "Монета | количество | цена | Прибыль", TextColor = Color.Black, Detail = "________________________________" });
            //  mainSection2.Add(new TextCell { Text = lext1+"  "+lext2, TextColor = Color.Black, Detail = "________________________________" });

            foreach (Portf.Coinnn Coin in Portf.Coin)
            {

                string strsum = Coin.Coinn + " |  " + Coin.amount + " | " + Coin.price + " | " + Coin.profit;
                mainSection.Add(new MyTextCell { coin_neme = Coin.Coinn, amount = Coin.amount, price = Coin.price, profit = Coin.profit, Text = strsum, TextColor = Color.Black, Command = new Command((object e) => MyTextCell_Event(Coin.Coinn, Coin.amount.ToString())) });


            }



            table.Root = new TableRoot()
            {
                mainSection,
            };

            table2.Root = new TableRoot()
            {
                mainSection2,
            };


            #region // grid.Children.Add
            grid.Children.Add(new BoxView { Color = Color.GreenYellow }, 0, 0);
            grid.Children.Add(table2, 0, 0);

            grid.Children.Add(new BoxView { Color = Color.White }, 0, 1);
            grid.Children.Add(table, 0, 1);
            grid.Children.Add(new BoxView { Color = Color.GreenYellow }, 0, 2);

            grid.Children.Add(label1, 0, 2);
            grid.Children.Add(label2, 0, 2);
            grid.Children.Add(switcher, 0, 2);
            grid.Children.Add(buttonadd, 0, 2);
            grid.Children.Add(buttondel, 0, 2);
            grid.Children.Add(buttonsave, 0, 2);
            grid.Children.Add(buttondel, 0, 2);
            grid.Children.Add(buttonchange, 0, 2);

            grid.Children.Add(CoinEntry, 0, 2);
            grid.Children.Add(AmountEntry, 0, 2);
            #endregion

            Content = grid;

            Portf.myUp_coin_();

            Device.StartTimer(TimeSpan.FromSeconds(15), () =>
            {
                Portf.myUp_coin_();
                return true;
            });

        }

        void OnButtonClickedadd(object sender, System.EventArgs e)
        {
            string strsum = coinstr + " :  " + Amountstr + " : " + "0";

            string mess = "Такой монеты нет на Binance! \n" +
                          "Возможно, вы неправильно написали пару торгуемая на Binance, она должна кончатся на USDT, например BTCUSDT, ETHUSDT, XRPUSDT и т.п.";
          
            foreach (Portf.Coinnn Coin in Portf.Coin)
            {
                if (Coin.Coinn == coinstr) { DisplayAlert("Уведомление", "В портфеле такая монета уже есть", "ОK"); return; }
            }

            if (coinstr == "") { DisplayAlert("Уведомление", "Поле <Монета> не может быть пустым", "ОK"); return; }

            if (!Portf.binance_symbols(coinstr)) { DisplayAlert("Уведомление", mess, "ОK"); return; }
         
            if (Amountstr == "") { DisplayAlert("Уведомление", "Поле <Количество> не может быть пустым", "ОK"); return; }
            if (!is_double_bool(Amountstr)) { DisplayAlert("Уведомление", "Поле <Количество> не может быть строкой", "ОK"); return; }
            else
            {
                Portf.add(coinstr, Convert.ToDouble(Amountstr), 0);
                CoinEntry.Text = "";
                AmountEntry.Text = "";
            }

            Portf.myUp_coin_();
            updatetable();
        }

        async void OnButtonClickeddel(object sender, System.EventArgs e)
        {
            if (coinstr == "") { await DisplayAlert("Уведомление", "Выберете монету !", "ОK"); return; }

            for (int i = 0; i < Portf.Coin.Count; i++)
            {
                if (Portf.Coin[i].Coinn == coinstr)
                {

                    bool result = await DisplayAlert("Подтвердить действие", "Вы точно хотите удалить монету?", "Да", "Нет");

                    if (result == true)
                    {
                        Portf.Coin.RemoveAt(i);
                        updatetable();
                        CoinEntry.Text = "";
                        AmountEntry.Text = "";
                    }
                    return;
                }

            }

            await DisplayAlert("Уведомление", "Такая монета не найдена в портфеле !", "ОK");

        }

        void OnButtonClickedsave(object sender, System.EventArgs e)
        {
            if (Portf.Coin.Count > 0)
            {
                string strsum = Portf.Coin[0].Coinn + ";" + Portf.Coin[0].amount + ";" + Portf.Coin[0].price;

                for (int i = 1; i < Portf.Coin.Count; i++)
                {
                    strsum = strsum + "|" + Portf.Coin[i].Coinn + ";" + Portf.Coin[i].amount + ";" + Portf.Coin[i].price;
                }

                PortfoStr = strsum;

                Portfolio_Save_Loading(1);

                Portf.Coin.Clear();
                string[] arr = null;
                string[] arr2 = null;
                arr = PortfoStr.Split(new char[] { '|' });
             
                for (int i = 0; i < arr.Length; i++)
                {
                    arr2 = arr[i].Split(new char[] { ';' });
                    Portf.add(arr2[0], Convert.ToDouble(arr2[1]), Convert.ToDouble(arr2[2]));
                }

                updatetable();
            }

        }

        void OnButtonClickedchange(object sender, System.EventArgs e)
        {
            string mess = "Такой монеты нет на Binance! \n" +
                         "Возможно, вы неправильно написали пару торгуемая на Binance, она должна кончатся на USDT, например BTCUSDT, ETHUSDT, XRPUSDT и т.п.";
            if (coinstr == "") { DisplayAlert("Уведомление", "Выберите монету!", "ОK"); return; }

            if (Amountstr == "") { DisplayAlert("Уведомление", "Введите количество монет!", "ОK"); return; }

            if (!is_double_bool(Amountstr)) { DisplayAlert("Уведомление", "Поле <Количество> не может быть строкой", "ОK"); return; }
            else
            {

                foreach (Portf.Coinnn Coin in Portf.Coin)
                {
                    if (Coin.Coinn == coinstr)
                    {
                        if (Coin.amount.ToString() == Amountstr) { DisplayAlert("Уведомление", "Вы не чего не поменяли!", "ОK"); return; }
                        Coin.amount = Convert.ToDouble(Amountstr);
                        CoinEntry.Text = "";
                        AmountEntry.Text = "";
                        updatetable();
                        return;
                    }
                }
            }

            if (!Portf.binance_symbols(coinstr)) { DisplayAlert("Уведомление", mess, "ОK"); return; }

            DisplayAlert("Уведомление", "Такая монета не найдена в портфеле! \n добавьте её в портфель!", "ОK");

            Portf.myUp_coin_();
            updatetable();
        }

        void updatetable()
        {
           
            mainSection.Clear();

            foreach (Portf.Coinnn Coin in Portf.Coin)
            {
                string strsum = Coin.Coinn + " |  " + Coin.amount + " | " + Coin.price + " | " + Coin.profit;
                mainSection.Add(new MyTextCell { coin_neme = Coin.Coinn, amount = Coin.amount, price = Coin.price, profit = Coin.profit, Text = strsum, TextColor = Color.Black, Command = new Command((object e) => MyTextCell_Event(Coin.Coinn, Coin.amount.ToString())) });

            }

        }

        private void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            Portf.rubF= switcher.IsToggled;
            rubFSave_Loading(1, switcher.IsToggled);
            Portf.Up_coinAll();
        }

        void OnButtonClicked(object sender, System.EventArgs e)
        {


        }

        void MyTextCell_Event(string coinstr2, string Amountstr2)
        {

            CoinEntry.Text = coinstr2;
            AmountEntry.Text = Amountstr2;

        }

        bool rubFSave_Loading(int k, bool rubF2)
        {
            object rubF = true;

            if (k == 1)
            {
                App.Current.Properties["rubF"] = rubF2;

            }

            if (k == 2)
            {
                if (App.Current.Properties.TryGetValue("rubF", out rubF))
                {
                    return (bool)rubF;
                    // выполняем действия, если в словаре есть ключ "rubF"


                }
            }

            return  true;
        }


        void Portfolio_Save_Loading(int k)
        {
            object Portfolio = "";

            if (k == 1)
            {
                App.Current.Properties["Portfolio"] = PortfoStr;

            }

            if (k == 2)
            {
                if (App.Current.Properties.TryGetValue("Portfolio", out Portfolio))
                {
                    PortfoStr = (string)Portfolio;
                    // выполняем действия, если в словаре есть ключ "name"
                    // button.Text = PortfoStr;

                }
            }
        }

        void pars_str(int k, string str)
        {
            if (k == 1 && str != "")
            {
                Portf.Coin.Clear();
                string[] arr = null;
                string[] arr2 = null;
                arr = str.Split(new char[] { '|' });
                for (int i = 0; i < arr.Length; i++)
                {
                    arr2 = arr[i].Split(new char[] { ';' });
                    Portf.add(arr2[0], Convert.ToDouble(arr2[1]), Convert.ToDouble(arr2[2]));
                }
            }
            if (k == 2 && Portf.Coin.Count > 0)
            {
                string strsum = "|" + Portf.Coin[0].Coinn + ";" + Portf.Coin[0].amount + ";" + Portf.Coin[0].price;

                for (int i = 1; i < Portf.Coin.Count; i++)
                {
                    strsum = strsum + "|" + Portf.Coin[i].Coinn + ";" + Portf.Coin[i].amount + ";" + Portf.Coin[i].price;
                }

                PortfoStr = strsum;
            }
        }


        private void CoinEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            coinstr = CoinEntry.Text; 
            
        }

        private void AmountEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

            //   if (is_double_bool(AmountEntry.Text)) Amountstr = AmountEntry.Text;
            //   else DisplayAlert("Уведомление", "Поле Amount не может быть строкой", "ОK"); return;
            Amountstr = AmountEntry.Text;

        }

        public bool is_double_bool(string st)
        {
            double res;
            bool isdoub = double.TryParse(st, out res);

            if (isdoub)
            {
                return isdoub;
            }

            return isdoub;

        }

        public void Up_coin()
        {
            if (Portf.Coin.Count > 0)
            {
                if (Portf.rubF) { label1.Text = "Общая прибыль: " + Portf.sum_last_rub.ToString("#.##") + " rub"; }
                else { label1.Text = "Общая прибыль: $ " + Portf.sum_last_usd.ToString("#.##"); }

            }
            else { label1.Text = "У тебя нет монет"; }

            updatetable();

         
        }

        
    }
}
