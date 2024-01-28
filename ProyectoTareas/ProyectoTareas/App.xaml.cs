using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoTareas.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;

namespace ProyectoTareas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ListaTareas());
        }

        protected override void OnStart()
        {

            AppCenter.Start("android={26966f18-9bd0-4516-9d90-95f66c5fe139};" +
            "uwp={Your UWP App secret here};" +
            "ios={Your iOS App secret here};" +
            "macos={Your macOS App secret here};",
            typeof(Analytics), typeof(Crashes));

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
