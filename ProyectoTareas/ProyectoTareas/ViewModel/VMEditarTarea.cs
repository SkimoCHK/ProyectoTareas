using ProyectoTareas.Datos;
using ProyectoTareas.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoTareas.ViewModel
{
    public class VMEditarTarea : BaseViewModel
    {
        #region VARIABLES
        private int _NroTarea;
        private string _Nombre;
        private string _selectedEstado;
        private ObservableCollection<string> _estados;
        public TareaModel _tarea { get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMEditarTarea(INavigation navigation, TareaModel tarea)
        {
            Navigation = navigation;
            _tarea = tarea;
            CargarEstados();
            AsignarValoresIniciales();
        }
        #endregion

        #region OBJETOS
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
        private void CargarEstados()
        {
           
            Estados = new ObservableCollection<string> { "Sin empezar", "En proceso", "Finalizada" };
            SelectedEstado = Estados[0];
        }

        private void AsignarValoresIniciales()
        {
            NroTarea = _tarea.NroTarea;
            Nombre = _tarea.Nombre;
            SelectedEstado = _tarea.Status;
            
        }

        public async Task Editar()
        {
            var funcion = new DTarea();
            _tarea.NroTarea = NroTarea;
            _tarea.Nombre = Nombre;
            _tarea.Status = SelectedEstado;

            await funcion.UpdateTarea(_tarea);
            await Volver();
        }

        public async Task Volver()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand Editarcommand => new Command(async () => await Editar());
        public ICommand Volvercommand => new Command(async () => await Volver());
        #endregion
    }
}
