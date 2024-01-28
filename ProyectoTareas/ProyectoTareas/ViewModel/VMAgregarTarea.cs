using ProyectoTareas.Datos;
using ProyectoTareas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace ProyectoTareas.ViewModel
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string cancel);
    }

    public class DialogService : IDialogService
    {
        public Task DisplayAlert(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }

        public class VMAgregarTarea : BaseViewModel
        {
            private readonly IDialogService _dialogService;

            #region VARIABLES
            private int _NroTarea;
            private string _Nombre;
            private string _selectedEstado;
            private DateTime _fechaFinalizacion = DateTime.Now;
            private ObservableCollection<string> _estados;
            #endregion

            #region CONSTRUCTOR
            public VMAgregarTarea(INavigation navigation)
            {
                Navigation = navigation;
                _dialogService = new DialogService();
                CargarEstados();
                CargarUltimoNumeroTarea();
            }
            #endregion

            #region OBJETOS

            public DateTime FechaFinalizacion
            {
                get { return _fechaFinalizacion; }
                set { SetValue(ref _fechaFinalizacion, value); }
            }

            public int NroTarea
            {
                get { return _NroTarea; }
                set { SetValue(ref _NroTarea, value); }
            }
            public string Nombre
            {
                get { return _Nombre; }
                set { SetValue(ref _Nombre, value); }
            }
            public string SelectedEstado
            {
                get { return _selectedEstado; }
                set { SetValue(ref _selectedEstado, value); }
            }

            public ObservableCollection<string> Estados
            {
                get { return _estados; }
                set { SetValue(ref _estados, value); }
            }
            #endregion

            #region PROCESOS

            private void CargarUltimoNumeroTarea()
            {
                if (Application.Current.Properties.ContainsKey("UltimoNumeroTarea"))
                {
                    NroTarea = (int)Application.Current.Properties["UltimoNumeroTarea"];
                }
                else
                {
                    NroTarea = 1;
                }
            }

            private void CargarEstados()
            {
                Estados = new ObservableCollection<string> { "Sin empezar", "En proceso", "Finalizada" };
                SelectedEstado = Estados[0];
            }

            public async Task Insertar()
            {
                var funcion = new DTarea();
                var parametros = new TareaModel();

                parametros.NroTarea = NroTarea;
                parametros.Nombre = Nombre;
                parametros.Status = SelectedEstado;

                await funcion.AgregarTarea(parametros);

                NroTarea++;

                Application.Current.Properties["UltimoNumeroTarea"] = NroTarea;
                await Application.Current.SavePropertiesAsync();

                await Volver();
            }

            public async Task Volver()
            {
                await Navigation.PopAsync();
            }
            #endregion

            #region COMANDOS
            public ICommand Insertarcommand => new Command(async () => await Insertar());
            public ICommand Volvercommand => new Command(async () => await Volver());
            #endregion
        }
}


