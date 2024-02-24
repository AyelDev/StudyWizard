using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
namespace StudyWizard.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSignUp : ContentPage
    {

        UserRepository _userRepository = new UserRepository();
        public UserSignUp()
        {
            InitializeComponent();
        }

        internal class FadeTo : Page
        {
            private int v;

            public FadeTo(int v)
            {
                this.v = v;
            }
        }

        private async void ButtonSignUp_Clicked(object sender, EventArgs e)
        {

            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                bool checkBxs = checkBx.IsChecked;
                string rawName = lblName.Text.ToLower();
                string rawEmail = lblEmail.Text.ToLower();
                string rawPass = lblPass.Text.ToLower();
                string rawCnfrmPass = lblConfrmPass.Text.ToLower();

                string Name = string.Concat(rawName.Where(c => !char.IsWhiteSpace(c)));
                string Email = string.Concat(rawEmail.Where(c => !char.IsWhiteSpace(c)));
                string Pass = string.Concat(rawPass.Where(c => !char.IsWhiteSpace(c)));
                string CnfrmPass = string.Concat(rawCnfrmPass.Where(c => !char.IsWhiteSpace(c)));

                int MaxName = Name.Length;

                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Pass) || string.IsNullOrEmpty(CnfrmPass))
                {
                    await DisplayAlert("Form Incomplete", "Please Enter Correctly.", "ok");

                }
                else if(!Email.Contains("gmail.com") && !Email.Contains("yahoo.com") && !Email.Contains("outlook.com"))
                {
                    await DisplayAlert("Wrong Email", "Please Enter Email Correctly.", "ok");
                }
                else if (!Pass.Equals(CnfrmPass))
                {
                    await DisplayAlert("Pasword And Confirm Password Not Matched", "Please Check Correctly", "ok");
                }
                else if (checkBxs != true)
                {
                    await DisplayAlert("Terms and Conditions Error", "Please Check, Thank You", "ok");
                }
                else if (MaxName <= 1)
                {
                    await DisplayAlert("Name Too Short", "Should Atleast 5 Characters Or More", "ok");
                }
                else
                {
                    await _userRepository.SignUp(Email, Pass, Name);
                    await DisplayAlert("SignUp Successful", "To fully Confirm Your Account We Sent You Confirmation Link To Your Email. Please Verify", "ok");
                    await Navigation.PopModalAsync();
                    //await Navigation.PushModalAsync(new Login());
                }
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Form Incomplete", "Please Enter Correctly.", "ok");

                //await DisplayAlert("Opps somethings wrong", $"An error occurred: {error.Message}, {error.StackTrace}", "Ok");
            }
            catch (Exception err)
            {
       
                if (err.Message.Contains("WEAK_PASSWORD"))
                {
                    await DisplayAlert("Weak Password", "Password Should Be At Least 6 Characters", "ok");
                }
                else if (err.Message.Contains("INVALID_EMAIL"))
                {
                    await DisplayAlert("Invalid Email", "Email Should Entered Correctly", "ok");
                }
                else if (err.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Email Already Exist", "Try Another Email Instead", "ok");
                }
                else
                {
                    //await DisplayAlert("Invalid Email", "Emter The Right Email And Try Again", "ok");
                    await DisplayAlert("Opps somethings wrong", $"An error occurred: {err.Message}, {err.StackTrace}", "Ok");
                }

               
            }

            UserDialogs.Instance.HideLoading();

        }
        
        public async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            if (e.Value)
            {
                await DisplayAlert("Terms And Conditions", "Lorem ipsum lorem ipsum lorem ipsum", "I Agree");
            }
         
        }

        public async void ButtonLogin_Clicked(object obj, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}