using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyWizard.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSignUp : ContentPage
    {
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
    }
}