using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ProyectoTareas.Models;
using ProyectoTareas.Datos;
using ProyectoTareas.Conexion;
using Xamarin.Forms;
using System.Threading.Tasks;
using ProyectoTareas.Views;
using System.Windows.Input;
using System.Linq;

namespace ProyectoTareas.ViewModel
{
    public class VMListarTareas : BaseViewModel
    {
        #region VARIABLES
        private ObservableCollection<TareaModel> _ListaTareas;
        private bool _Cronometro = false;
        #endregion

        #region CONSTRUCTOR
        public VMListarTareas(INavigation navigation)
        {
            Navigation = navigation;
            MostrarTareas();
            Tiempo();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<TareaModel> ListaTareas
        {
            get { return _ListaTareas; }
            set
            {
                SetValue(ref _ListaTareas, value);
                OnpropertyChanged();
            }
        }
        #endregion

        #region PROCESOS
        public async Task MostrarTareas()
        {
            var funcion = new DTarea();
            ListaTareas = await funcion.MostrarTareasD();
        }

        public async Task Iraregistro()
        {
            await Navigation.PushAsync(new AgregarTarea());
        }

        public async Task EditarTarea(TareaModel tareaxd)
        {
            await Navigation.PushAsync(new EditarTarea(tareaxd));
        }
        public async Task EliminarTarea(TareaModel tarea)
        {
            var funcion = new DTarea();
            await funcion.EliminarTareaD(tarea.NroTarea);
            await MostrarTareas();
        }

        private void Tiempo()
        {
            if (!_Cronometro)
            {
                _Cronometro = true;
                Device.StartTimer(TimeSpan.FromSeconds(30), () =>
                {
                    ChecarElDatoVIVAMLO();
                    return _Cronometro;
                });
            }
        }

        //el fail xd
        private void ChecarElDatoVIVAMLO()
        {
            if (ListaTareas != null && ListaTareas.Any())
            {
                var finalizadas = ListaTareas.Where(t => t.Status == "Finalizada").ToList();

                foreach (var tarea in finalizadas)
                {
                    
                    Task.Run(async () => await EliminarTarea(tarea));
                }
            }
        }


        #endregion

        #region COMANDOS
        public ICommand Iraregistrocommmand => new Command(async () => await Iraregistro());
        public ICommand Editarpokemoncommand => new Command<TareaModel>(async (p) => await EditarTarea(p));
        public ICommand EliminarTareaCommand => new Command<TareaModel>(async (t) => await EliminarTarea(t));
        #endregion
    }
}
