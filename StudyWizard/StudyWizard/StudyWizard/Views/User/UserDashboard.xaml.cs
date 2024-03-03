using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyWizard.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDashboard : TabbedPage
    {
        public UserDashboard()
        {
            InitializeComponent();
            



        }
        private void ButtonLogoutUser_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            Navigation.PopToRootAsync();
            
        }

        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("logout??", "yes", "ok");
            // Implement your custom logic for handling the back button press
            // For example, check if there's an ongoing operation that shouldn't be interrupted

            // Return true to prevent the user from navigating back
            return true;
        }
    }
}