using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Auth;
using Plugin.SimpleAudioPlayer;
namespace StudyWizard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        
        //user repository
        UserRepository _userRepository = new UserRepository();
        
        public Login()
        {
      
            InitializeComponent();
            LblForgotPassword();
            //Check if user save login credentials
            Task.Run(async () => await CheckCredentials());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            //current state of connectivity
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                NoNetPage.IsVisible = false;
                LoginPage.IsVisible = true;
                // Connection to internet is available
            }
            else
            {
                NoNetPage.IsVisible = true;
                LoginPage.IsVisible = false;
            }
           
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if(e.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Network Lost", "Please connect your internet and try again", "ok");
                NoNetPage.IsVisible = true;
                LoginPage.IsVisible = false;
                 //   NetWorkPageVisible.IsVisible = false;
                 // NoNetWorkPageVisible.IsVisible = true;

            }
            else
            {
                NoNetPage.IsVisible = false;
                LoginPage.IsVisible = true;
                /* await NetWorkPageVisible.FadeTo(1).ContinueWith((result) => {
                   
                });*/

                // NoNetWorkPageVisible.IsVisible = false;
                //NetWorkPageVisible.IsVisible = true;
            }
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

     


        private async void ButtonUserSignUp_Clicked(object sender, EventArgs e)
        {  
            //Loader for nagivating the UserSignUp page span of .5 seconds
            UserDialogs.Instance.ShowLoading("Loading Please Wait...");
            await Navigation.PushModalAsync(new Views.User.UserSignUp());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            System.Threading.Thread.Sleep(500);
            stopwatch.Stop();
            UserDialogs.Instance.HideLoading();

        }

        public void LblForgotPassword()
        { 
          LabelUserForgotPassword_Tap.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async()=>
                {
                    //Loader for nagivating the UserForgotPassword page span of .5 seconds
                    UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                    await Navigation.PushModalAsync(new Views.User.UserForgotPassword());
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    System.Threading.Thread.Sleep(500);
                    stopwatch.Stop();
                    UserDialogs.Instance.HideLoading();
                  
                    
                })
            }
                );
        }

       
        private async void ButtonUserSignIn_Clicked(object sender, EventArgs e)
        {
        
            try
            {


                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                string userRawEmail = UserEmail.Text.ToLower();
                string userRawPass = UserPassword.Text.ToLower();

                string userEmail = string.Concat(userRawEmail.Where(c => !char.IsWhiteSpace(c)));
                string userPass = string.Concat(userRawPass.Where(c => !char.IsWhiteSpace(c)));
              
                if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userPass))
                {

                    await DisplayAlert("Form Incomplete", "Please Enter the Password or Email Correctly.", "ok");


                }
                else
                {
                    var token = await _userRepository.SignIn(userEmail, userPass);
                    Preferences.Set("token", token);
                    Preferences.Set("userEmail", userEmail);
                    var userDashboard = new Views.User.UserDashboard();
                    await Navigation.PushAsync(userDashboard);
                    Debug.WriteLine("Signing in user dashboard...");
                    //await Navigation.PushModalAsync(new Views.User.UserDashboard());
                    Stopwatch stopwatch = new Stopwatch();
                   



               
                }
                   
          

            }
            catch (Exception err)
            {
                string errors = err.ToString().ToLower();
                
                if (errors.Contains("invalid_login_credentials"))
                {
                    await DisplayAlert("Invalid Login Credentials", "Please Enter the Password or Email Correctly", "ok");
                }
                else if(errors.Contains("object reference"))
                {
                     await DisplayAlert("Form Incomplete", "Please Enter the Password or Email Correctly.", "ok");
                }
                else if (errors.Contains("invalid_email"))
                {
                    await DisplayAlert("Invalid Email", "Please Enter Your Email Correctly.", "ok");
                }
                else if (errors.Contains("too_many_attempts_try_later"))
                {
                    await DisplayAlert("Too Many Login Attempts", "Access To This Account Has Been Temporarily Disabled Due To Many Failed Login Attempts. You Can Immediately Restore It " +
                        "By Resetting Your Password Or You Can Try Again Later", "ok");
                }
                else
                {
                    await DisplayAlert("Oh No, Something Is Off.", $"An error occurred: {err.Message}, {err.StackTrace}", "ok");
                }
                 //await DisplayAlert("Incomplete Filling up", "Please fill the Email or Password correctly", "ok");
           
                //Console.WriteLine($"An error occurred: {error.Message}");
                //Console.WriteLine($"Stack trace: {error.StackTrace}");
                
            }
            /*catch(Exception err)
            {
                await DisplayAlert("Incorrect Email or Password", "Please type it correctly", "ok");
                UserEmail.Text = string.Empty;
                UserPassword.Text = string.Empty;
                //await DisplayAlert("Opps somethings wrong", $"An error occurred: {error.Message}, {error.Data}", "Ok");
              
                
            }*/
            UserDialogs.Instance.HideLoading();

        }

        public async Task CheckCredentials()
        {
          
            bool hasKey = Preferences.ContainsKey("token");
            if (hasKey)
            {
                string token = Preferences.Get("token", "");
                if (!string.IsNullOrEmpty(token))
                {

                   
                   await Navigation.PushAsync(new Views.User.UserDashboard());

                }
                else
                {
                   await Navigation.PopToRootAsync();
                  
                }

            }
            

        }


        protected override bool OnBackButtonPressed()
        {

            DisplayAlert("exit??", "yes", "ok");
            // Implement your custom logic for handling the back button press
            // For example, check if there's an ongoing operation that shouldn't be interrupted

            // Return true to prevent the user from navigating back
            return true;
        }
    }
}