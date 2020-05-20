using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChuQuotes.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public  ICommand OpenWebCommand       { get; }
        public  ICommand TrainStrengthCommand { get; }
        private AccelerometerData Reading { get; set; }
        private double   strength  = 1;
        private double   willpower = 0;

        public double Strength
        {
            get => strength;
            set => SetProperty(ref strength, value);
        }

        public double Willpower
        {
            get => willpower;
            set => SetProperty(ref willpower, value);
        }

        public AboutViewModel()
        {
            try
            {
                if(!Accelerometer.IsMonitoring)
                    Accelerometer.Start(SensorSpeed.UI);
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }

            Title          = "About ChuQuotes";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://api.chucknorris.io/"));
            TrainStrengthCommand = new Command(() =>
            {
                switch (Strength)
                {
                    case 1:
                        Strength++;
                        break;
                    case double.PositiveInfinity:
                        Willpower += Willpower is 0 ? 1 : Willpower;
                        Strength = Willpower;
                        break;
                    default:
                        Strength = Math.Pow(Strength, 2);
                        break;
                }
            });
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            if(!e.Reading.Acceleration.Equals(Reading.Acceleration))
                switch (Strength)
                {
                    case 1:
                        Strength++;
                        break;
                    case double.PositiveInfinity:
                        Willpower += Willpower is 0 ? 1 : Willpower;
                        Strength  =  Willpower;
                        break;
                    default:
                        Strength = Math.Pow(Strength, 2);
                        break;
                }
            Reading = e.Reading;
        }

    }
}