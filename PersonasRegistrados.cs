using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MAIN_PROJECT.Personaje;

namespace MAIN_PROJECT
{
    abstract public class PersonasRegistrados : Personaje
    {
        private string login;
        private string clave;
        private string nombrePublico;

        protected string Login { get => login; set => login = value; }
        public string Clave { get => clave; set => clave = value; }
        public string NombrePublico { get => nombrePublico; set => nombrePublico = value; }

        public PersonasRegistrados(string ip, Estado estado, string login, string clave, string nombrePublico) : base(ip, estado)
        {
            Login = login;
            Clave = clave;
            NombrePublico = nombrePublico;
        }
        /// <summary>
        /// Metodo para verificar dificultad y seguridad de contrasena
        /// </summary>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        public bool VerificarContrasena(string contrasena)
        {
            
            if(contrasena.Length <= 8)
            {
                Console.WriteLine("Entra mas de 8 symbolos");
                return false;
            }
            else if(contrasena == Login)
            {
                Console.WriteLine("Login y Clave hay que ser diferentes");
                return false;
            }

            return true;
        }
        
    }
}
