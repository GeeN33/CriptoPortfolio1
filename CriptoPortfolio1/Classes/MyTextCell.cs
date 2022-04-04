using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CriptoPortfolio1.Classes
{
    class MyTextCell : TextCell
    {
        public string coin_neme { get; set; }

        public double amount { get; set; }

        public double price { get; set; }

        public double profit { get; set; }

    }
}
