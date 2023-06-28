using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MAIN_PROJECT.Personaje;

namespace MAIN_PROJECT
{
    public class userDeJuego : PersonasRegistrados
    {

        private int asesinatos;
        private int matados;
        private int asistentes;
        private int nivelDeCuenta;
        private NivelDeAcceso nivelDeAccesoParaUser;

        public int Asesinatos { get => asesinatos; set => asesinatos = value; }
        public int Matados { get => matados; set => matados = value; }
        public int Asistentes { get => asistentes; set => asistentes = value; }
        public int NivelDeCuenta { get => nivelDeCuenta; set => nivelDeCuenta = value; }
        public NivelDeAcceso NivelDeAccesoParaUser { get => nivelDeAccesoParaUser; set => nivelDeAccesoParaUser = value; }

        public enum NivelDeAcceso { Bloqueado, Basico, Premium, Admin };

        public userDeJuego(string ip, Estado estado, string login, string clave, string nombrePublico) : base(ip, estado, login, clave, nombrePublico)
        {
            asesinatos = 0;
            matados = 0;
            asistentes = 0;
            nivelDeCuenta = 0;
            NivelDeAccesoParaUser = NivelDeAcceso.Basico;
        }






        public virtual void Registrarse(string ip, bool estaBloqueado, Estado estado, string login, string clave, string nombrePublico)
        {

        }

        //TODO - can cause problems, need to be overrited
        public void InsertarEnBD(string ip, bool estaBloqueado, Estado estado, string login, string clave, string nombrePublico)
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(PersonasRegistrados);
            string nombreDeTabla = name.Name;
            string consulta = "INSERT INTO ";
            string estdo = estado.ToString();
            consulta += nombreDeTabla;
            consulta += "(Ip, estaBloqueado, estado, login, clave, nombrePublico) VALUES";
            consulta += $"('{ip}', {estaBloqueado}, '{estdo}, {login}', {clave}, '{nombrePublico}')";
            using (OleDbConnection conexion = new OleDbConnection(ruta))
            {
                OleDbCommand comando = new OleDbCommand(consulta, conexion);
                try
                {
                    conexion.Open();
                    OleDbDataReader miTabla = comando.ExecuteReader();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public bool IsLoginDisponible(string login)
        {
            List<PersonasRegistrados> listaDePersonas;


        }
    }
}
