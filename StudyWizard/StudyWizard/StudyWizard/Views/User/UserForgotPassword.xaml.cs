using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Auth;
using Acr.UserDialogs;

namespace StudyWizard.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserForgotPassword : ContentPage
    {

        //user repository
        UserRepository _userRepository = new UserRepository();

        public UserForgotPassword()
        {
            InitializeComponent();
        }
        private async void ButtonUserVerify_Clicked(object obj, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                string userEmailRaw = Email.Text.ToLower();
                string userEmail = string.Concat(userEmailRaw.Where(c => !char.IsWhiteSpace(c)));

                if (string.IsNullOrEmpty(userEmail))
                {
                    await DisplayAlert("Incomplete Filling up", "Please fill the Email correctly", "ok");
                    
                }
                else
                {
                    //verify email
                    await _userRepository.PasswordReset(userEmail);
                    await DisplayAlert("Password Reset Complete", "Please check your email to reset your password", "ok");
                    await Navigation.PopModalAsync();


                }
            }
            catch(Exception err)
            {
               
              
               
                if(err.Message.Contains("Object reference"))
                {
                    await DisplayAlert("Incomplete Filling up", "Please fill the Email correctly", "ok");
                }
                else if (err.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("Invalid Email", "Please type the Email correctly", "ok");
                }
                else if(err.Message.Contains("Index was out of range"))
                {
                    await DisplayAlert("Index Was Out Of Range", "Must Be Non-Negative And Less Than The Size Of Collection", "ok");
                }
                else
                {
                    await DisplayAlert("Opps somethings wrong", $"An error occurred: {err.Message}, {err.StackTrace}", "Ok");
                }
            }
            //await DisplayAlert("Incomplete Filling up", "Please fill the Email correctly", "ok");
            UserDialogs.Instance.HideLoading();

        }
        public async void ButtonLogin_Clicked(object obj, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}