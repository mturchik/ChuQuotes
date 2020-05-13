using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChuQuotes.Services;
using ChuQuotes.Views;

namespace ChuQuotes
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            
            DependencyService.Register<QuoteDataStore>();
            DependencyService.Register<ChuckService>();
            MainPage = new MainPage();
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