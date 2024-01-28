using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTareas.ViewModel;
using ProyectoTareas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoTareas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarTarea : ContentPage
    {
        public EditarTarea(TareaModel tareaxd)
        {
            InitializeComponent();
            BindingContext = new VMEditarTarea(Navigation,tareaxd);

        }
    }
}