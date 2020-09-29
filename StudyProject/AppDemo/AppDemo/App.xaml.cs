using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppDemo.MainPage.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
