using System;
using System.Collections.Generic;
using System.Text;

namespace CriptoPortfolio1.Classes
{
    class Bar_Class
    {
        public double open = 0;
        public double close = 0;
        public double high = 0;
        public double low = 0;

        public Bar_Class(double open1, double high1, double low1, double close1)
        {
            open = open1;
            close = close1;
            high = high1;
            low = low1;
        }

        public void point_Price(ref float high_y, ref float low_y, ref float close_y, int Height, double max, double min)
        {

            high_y = (float)(Height - (high - min) * (Height / (max - min)));
            low_y = (float)(Height - (low - min) * (Height / (max - min)));
            close_y = (float)(Height - (close - min) * (Height / (max - min)));

        }

        public int FF()
        {
            if (open < close)
            {
                return 1;
            }
            if (open > close)
            {
                return 2;
            }
            return 3;
        }
    }
}
