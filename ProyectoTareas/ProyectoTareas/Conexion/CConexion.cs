using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;


namespace ProyectoTareas.Conexion
{
    public class CConexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://tareasxd-f860d-default-rtdb.firebaseio.com/");
    }
}
