using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    public class UseresDeJuego
    {
        public static List<UserDeJuego> useres;

        public UseresDeJuego()
        {
            useres = new List<UserDeJuego>();
            RenovarUseres();
        }
        public static void RenovarUseres()
        {
            useres.Clear();
            Type name = typeof(UseresDeJuego);
            string nombreDeTabla = name.Name;
            string consulta = "SELECT * FROM ";
            consulta += nombreDeTabla;
            using (OleDbConnection conexion = new OleDbConnection(Personaje.ruta))
            {
                OleDbCommand comando = new OleDbCommand(consulta, conexion);
                try
                {
                    conexion.Open();
                    OleDbDataReader miTabla = comando.ExecuteReader();
                    Console.WriteLine("Database MainDB, tabla " + nombreDeTabla);
                    while (miTabla.Read())
                    {
                        int id = Convert.ToInt32(miTabla["Id"].ToString());
                        string ip = miTabla["Ip"].ToString();
                        Personaje.Estado estado = (Personaje.Estado)Enum.Parse(typeof(Personaje.Estado), miTabla["estado"].ToString());
                        bool estaBloqueado = Convert.ToBoolean(miTabla["estaBloqueado"]);
                        string login = miTabla["login"].ToString();
                        string clave = miTabla["clave"].ToString();
                        string nombrePublico = miTabla["nombrePublico"].ToString();
                        int asesinatos = Convert.ToInt32(miTabla["asesinatos"].ToString());
                        int matados = Convert.ToInt32(miTabla["matados"].ToString());
                        int asistentes = Convert.ToInt32(miTabla["asistentes"].ToString());
                        int nivelDeCuenta = Convert.ToInt32(miTabla["nivelDeCuenta"].ToString());
                        UserDeJuego.NivelDeAcceso nivelDeAcceso = (UserDeJuego.NivelDeAcceso)Enum.Parse(typeof(UserDeJuego.NivelDeAcceso), miTabla["nivelDeAcceso"].ToString());


                        UserDeJuego tempUser = new UserDeJuego(ip, estado, login, clave, nombrePublico, asesinatos, matados, asistentes, nivelDeCuenta, nivelDeAcceso);
                        tempUser.Id = id;
                        useres.Add(tempUser);
                    }
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void actualizarTodosUseresEnBD()
        {
            foreach( var elem in useres)
            {
                elem.actualizarUserEnBD();
            }
        }
    }
}
