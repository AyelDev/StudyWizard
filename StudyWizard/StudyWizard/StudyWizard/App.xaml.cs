using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;

namespace StudyWizard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());

        }

        protected override void OnStart()
        {
            //overriding start sound
                 var sound = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                 sound.Load("loginSound.mp3");
                 sound.Play();

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

       
    }
}
