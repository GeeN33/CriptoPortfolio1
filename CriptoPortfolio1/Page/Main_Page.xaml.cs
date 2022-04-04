using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using CriptoPortfolio1.Classes;

namespace CriptoPortfolio1.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main_Page : ContentPage
    {
        #region // Label
        Label label1;
        Label label1_L;
        Label label1_H;

        Label label2;
        Label label2_L;
        Label label2_H;

        Label label3;
        Label label3_L;
        Label label3_H;

        Label labelPrf;
        #endregion

        double size_Height = Device.Info.ScaledScreenSize.Height;

        SKCanvasView canvasView;
        SKCanvasView canvasView2;
        SKCanvasView canvasView3;

        Draww draww ;
        Draww draww2;
        Draww draww3;

        Klines klines;
        Klines klines2;
        Klines klines3;

        Color Color_Backg = Color.Aqua;

        public Main_Page()
        {
            InitializeComponent();

            BackgroundColor = Color.Lime;
           
            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star), },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
            };

            

            int sec = 15;
            double s_H = size_Height / 4;
            draww = new Draww(0, 0);
            draww2 = new Draww(0, 0);
            draww3 = new Draww(0, 0);

            klines = new Klines();
            klines2 = new Klines();
            klines3 = new Klines();

            #region // Label

            label1 = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "загрузка данных",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.Black
            };
            label1_L = new Label
            {
                Margin = new Thickness(2, 2),
                Text = (s_H / 4).ToString(),
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),

            };
            label1_H = new Label
            {
                Margin = new Thickness(2, 2),
                Text = (s_H / 2).ToString(),
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };
           
            label2 = new Label
            {

                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "загрузка данных",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.Black
            };
            label2_L = new Label
            {
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.End,
                Text = "0.0000",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),

            };
            label2_H = new Label
            {
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                Text = "0.0000",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),

            };
           
            label3 = new Label
            {

                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "загрузка данных",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.Black
            };
            label3_L = new Label
            {
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.End,
                Text = "0.0000",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),

            };
            label3_H = new Label
            {
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                Text = "0.0000",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
            };
           
            labelPrf = new Label
            {

                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "загрузка данных",
                BackgroundColor = Color_Backg,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.Black
            };
            #endregion

            Portf.binance_info();

            Portf.evenUp_coin += Up_coin;

            canvasView = new SKCanvasView();
            canvasView.PaintSurface += draww.canvasView_PaintSurface;

            canvasView2 = new SKCanvasView();
            canvasView2.PaintSurface += draww2.canvasView_PaintSurface;

            canvasView3 = new SKCanvasView();
            canvasView3.PaintSurface += draww3.canvasView_PaintSurface;

            Up_draww();

            Device.StartTimer(TimeSpan.FromSeconds(sec), () =>
            {
                Up_draww();

                sec = 30;


                return true;
            });

            BoxView GreenYellow = new BoxView { Color = Color.GreenYellow };


       

            grid.Children.Add(new Image { Source = "Bitcoin1.png" }, 0, 0);
            grid.Children.Add(new Image { Source = "Ethereum1.png" }, 0, 1);
            grid.Children.Add(new Image { Source = "ripple.png" }, 0, 2);

            grid.Children.Add(canvasView, 1, 0);
            grid.Children.Add(canvasView2, 1, 1);
            grid.Children.Add(canvasView3, 1, 2);

         
            //  grid.Children.Add(label1_L, 2, 0);
            grid.Children.Add(label1, 2, 0);

            grid.Children.Add(label2, 2, 1);

            grid.Children.Add(label3, 2, 2);

           

            grid.Children.Add(labelPrf, 0, 3);
            Grid.SetColumnSpan(labelPrf, 3);

            Content = grid;
        }

        public void Up_coin()
        {

            if (Portf.Coin.Count > 0)   
            {
                if (Portf.rubF) { labelPrf.Text = Portf.sum_last_rub.ToString("#.##") + " rub"; }
                else { labelPrf.Text = "$ " +  Portf.sum_last_usd.ToString("#.##"); }
            
            }
            else { labelPrf.Text = "У тебя нет монет"; }

        }

        public void Up_draww()
        {

            draww.Bar = klines.binance("BTCUSDT");
            draww2.Bar = klines2.binance("ETHUSDT");
            draww3.Bar = klines3.binance("XRPUSDT");

            label1.Text = klines.price.ToString();
            //     label1_L.Text = klines.high.ToString();
            //      label1_H.Text = klines.high.ToString();

            label2.Text = klines2.price.ToString();
            //    label2_L.Text = klines2.high.ToString();
            //      label2_H.Text = klines2.high.ToString();

            label3.Text = klines3.price.ToString();
            //      label3_L.Text = klines3.high.ToString();
            //       label3_H.Text = klines3.high.ToString();

            canvasView.InvalidateSurface();
            canvasView2.InvalidateSurface();
            canvasView3.InvalidateSurface();

        }
    }
}