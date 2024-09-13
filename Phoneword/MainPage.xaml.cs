
using System;

namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;
        string translatedNumber;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = Phoneword.PhoneWordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";
            }
        }

        async void OnCall(object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a Number",
                $"Would you like to call {translatedNumber}?",
                "Yes",
                "No"))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                    {
                        if (!string.IsNullOrWhiteSpace(translatedNumber))
                        {
                            PhoneDialer.Default.Open(translatedNumber);
                        }
                        else
                        {
                            await DisplayAlert("Invalid Number", "Phone number is not valid.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Unsupported Feature", "Phone dialing is not supported on this device.", "OK");
                    }
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to Dial", "Phone number was not valid.", "OK");
                }
                catch (Exception)
                {
                    // Handle other exceptions
                    await DisplayAlert("Unable to Dial", "Phone dialing failed due to an unexpected error.", "OK");
                }
            }
        }


        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

        //private void btnHello_Clicked(object sender, EventArgs e)
        //{
        //    DisplayAlert("Hello", "Welcome to MAUI", "OK");
        //}

        //private void btnSubmit_Clicked(object sender, EventArgs e)
        //{
        //    string name = usernameInput.Text;
        //    DisplayAlert("Welcome", $"Hello {name}", "OK");
        //}


    }

}
