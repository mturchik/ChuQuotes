using ChuQuotes.Models;
using ChuQuotes.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChuQuotes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RandomPage : ContentPage
    {
        public  IChuckService ChuckService => DependencyService.Get<IChuckService>();
        public  Quote         Quote        { get; set; }

        public RandomPage()
        {
            InitializeComponent();

            Title             = "Random Chuck Quote";
            QuoteContent.Text = "I'm a cheesy quote, click 'Get New Quote' to kick me to the curb.";
            BindingContext    = this;
        }

        public void Fave_Clicked(object sender, EventArgs e)
        {
            if (Quote != null && !IsBusy)
                MessagingCenter.Send(this, "AddQuote", Quote);
        }

        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            
            QuoteContent.Text = "Loading...";
            Quote             = await ChuckService.GetQuote();
            QuoteContent.Text = Quote.Content;

            IsBusy = false;
        }
    }
}