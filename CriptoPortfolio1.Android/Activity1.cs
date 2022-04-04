﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CriptoPortfolio1.Droid
{
    [Activity(Label = "VICP",
        Icon = "@drawable/ICON128",
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true )]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
            // Create your application here
        }
    }
}