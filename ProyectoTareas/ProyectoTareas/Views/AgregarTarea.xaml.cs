using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTareas.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoTareas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarTarea : ContentPage
    {
        public AgregarTarea()
        {
            InitializeComponent();
            BindingContext = new VMAgregarTarea(Navigation);
        }
    }
}