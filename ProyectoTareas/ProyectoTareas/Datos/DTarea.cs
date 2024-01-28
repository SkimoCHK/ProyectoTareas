using System;
using System.Collections.Generic;
using System.Text;
using ProyectoTareas.Models;
using ProyectoTareas.Conexion;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Reactive.Linq;
using Firebase.Database;
using Xamarin.Essentials;

namespace ProyectoTareas.Datos
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string cancel);
    }


    public class DTarea
    {

        public async Task AgregarTarea(TareaModel parametros)
        {
            await CConexion.firebase
                .Child("Tarea")
                .PostAsync(new TareaModel()
                {
                    NroTarea = parametros.NroTarea,
                    Nombre = parametros.Nombre,
                    Status = parametros.Status,
                    
                }
                );
        }

        public async Task UpdateTarea(TareaModel _Tarea)
        {
            var UpdatePokemon = (await CConexion.firebase
              .Child("Tarea")
              .OnceAsync<TareaModel>()).Where(a => a.Object.NroTarea == _Tarea.NroTarea).FirstOrDefault();

            await CConexion.firebase
              .Child("Tarea")
              .Child(UpdatePokemon.Key)
              .PutAsync(new TareaModel() { NroTarea = _Tarea.NroTarea, Nombre = _Tarea.Nombre, Status = _Tarea.Status});
        }

        public async Task<ObservableCollection<TareaModel>> MostrarTareasD()
        {

            var data = await Task.Run(() => CConexion.firebase
                .Child("Tarea")
                .AsObservable<TareaModel>()
                .AsObservableCollection());
            return data;
        }
        public async Task EliminarTareaD(int tareaId)
        {
            var tarea = (await CConexion.firebase
                .Child("Tarea")
                .OnceAsync<TareaModel>()).FirstOrDefault(a => a.Object.NroTarea == tareaId);

            if (tarea != null)
            {
                await CConexion.firebase
                    .Child("Tarea")
                    .Child(tarea.Key)
                    .DeleteAsync();
            }
        }



    }
}
