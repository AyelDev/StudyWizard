using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyWizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void ButtonUserSignUp_Clicked(object sender, EventArgs e)
        {
           
            //Loader for nagivating the UserSignUp page span of .5 seconds
            UserDialogs.Instance.ShowLoading("Loading Please Wait...");
            await Navigation.PushAsync(new Views.User.UserSignUp());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            System.Threading.Thread.Sleep(500);
            stopwatch.Stop();
            UserDialogs.Instance.HideLoading();
        }

        private async void ButtonUserForgotPassword_Clicked(object sender, EventArgs e)
        {
            //Loader for nagivating the UserForgotPassword page span of .5 seconds
            UserDialogs.Instance.ShowLoading("Loading Please Wait...");
            await Navigation.PushAsync(new Views.User.UserForgotPassword());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            System.Threading.Thread.Sleep(500);
            stopwatch.Stop();
            UserDialogs.Instance.HideLoading();
       
        }

        private async void ButtonUserDashboard_Clicked(object sender, EventArgs e)
        {

            try
            {

                string userRawEmail = UserEmail.Text.ToLower();
                string userRawPass = UserPassword.Text.ToLower();

                string userEmail = string.Concat(userRawEmail.Where(c => !char.IsWhiteSpace(c)));
                string userPass = string.Concat(userRawPass.Where(c => !char.IsWhiteSpace(c)));
                Console.WriteLine(userEmail + " " + userPass);
                if (userEmail == "user" && userPass == "123")
                {
                    await Navigation.PushAsync(new Views.User.UserDashboard());
                }
                else
                {
                    await DisplayAlert("Incorrect Username or Password", "Please type it correctly", "ok");

                }
                //partial codes

            }
            catch (NullReferenceException error)
            {
                await DisplayAlert("Empty Username or Password", "Please type it correctly", "ok");
                //await DisplayAlert("Opps somethings wrong" ,$"An error occurred: {error.Message}, {error.StackTrace}", "Ok");
                Console.WriteLine($"An error occurred: {error.Message}");
                Console.WriteLine($"Stack trace: {error.StackTrace}");
            }
            catch(ArgumentNullException error)
            {
                Console.WriteLine($"An error occurred: {error.Message}");
                Console.WriteLine($"Stack trace: {error.StackTrace}");
            }

            
        }
        
    }
}