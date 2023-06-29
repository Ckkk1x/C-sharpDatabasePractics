using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MAIN_PROJECT.Personaje;
using static MAIN_PROJECT.UserDeJuego;

namespace MAIN_PROJECT
{
    public class UserDeJuego : PersonasRegistrados
    {

        private int asesinatos;
        private int matados;
        private int asistentes;
        private int nivelDeCuenta;
        private NivelDeAcceso nivelDeAccesoParaUser; // En DB se llama nivelDeAcceso

        public int Asesinatos { get => asesinatos; set => asesinatos = value; }
        public int Matados { get => matados; set => matados = value; }
        public int Asistentes { get => asistentes; set => asistentes = value; }
        public int NivelDeCuenta { get => nivelDeCuenta; set => nivelDeCuenta = value; }
        public NivelDeAcceso NivelDeAccesoParaUser { get => nivelDeAccesoParaUser; set => nivelDeAccesoParaUser = value; }

        public enum NivelDeAcceso { Bloqueado, Basico, Premium, Admin };

        public UserDeJuego(string ip, Estado estado, string login, string clave, string nombrePublico) : base(ip, estado, login, clave, nombrePublico)
        {
            asesinatos = 0;
            matados = 0;
            asistentes = 0;
            nivelDeCuenta = 0;
            NivelDeAccesoParaUser = NivelDeAcceso.Basico;
        }
        public UserDeJuego(string ip, Estado estado, string login, string clave, string nombrePublico, int asesinatos, int matados, int asistentes, int nivelDeCuenta, NivelDeAcceso nivelDeAccesoParaUser) : base(ip, estado, login, clave, nombrePublico)
        {
            Asesinatos = asesinatos;
            Matados = matados;
            Asistentes = asistentes;
            NivelDeCuenta = nivelDeCuenta;
            NivelDeAccesoParaUser = nivelDeAccesoParaUser;
        }
        public void SubirNivelDeCuenta(int experiencia)
        {
            nivelDeCuenta += experiencia;
        }
        public void RenovarEstadisticas(int asesinatos, int matados, int asistentes)
        {
            Asesinatos = asesinatos;
            Matados = matados;
            Asistentes = asistentes;
        }
        public void CambiarAcceso(NivelDeAcceso nivel)
        {
            nivelDeAccesoParaUser = nivel;
        }
        public void CambiarLogin(string login)
        {
            if(IsLoginDisponible(login))
            {
                Login = login;
            }
            //Login no es disponible, tenemos que avisar el user por visual(algun pop-up)
            else
            {
                Console.WriteLine("Login no es diponible");
            }
        }
        
        //TODO - can cause problems, need to be overrited
        /// <summary>
        /// Insertar user actual en BD.
        /// </summary>
        public virtual void InsertarEnBD()
        {
            if(!HayUserEnBD() && IsLoginDisponible(Login))
            { 
                // hemos recibido nombre de classe en que estamos
                string nombreDeTabla = "useresDeJuego";
                string consulta = "INSERT INTO ";
                string estdo = EstadoDeUser.ToString();
                consulta += nombreDeTabla;
                consulta += "(Ip, estaBloqueado, estado, login, clave, nombrePublico, asesinatos, matados, asistentes, nivelDeCuenta, nivelDeAcceso) VALUES";
                consulta += $"('{Ip}', {EstaBloqueado}, '{estdo}', '{Login}', '{Clave}', '{NombrePublico}', {asesinatos}, {matados}, {asistentes}, {nivelDeCuenta}, '{nivelDeAccesoParaUser.ToString()}')";
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
            else
            {
                Console.WriteLine("Ya tenemos user con este id o login no esta disponible");
            }
        }

        public bool IsLoginDisponible(string login)
        {
            UseresDeJuego.RenovarUseres();
            UserDeJuego temp = UseresDeJuego.useres.Find(element => element.Login == login);
            if(temp == null)
            {
                return true;
            }
            return false;
        }
        public bool HayUserEnBD()
        {
            UseresDeJuego.RenovarUseres();
            UserDeJuego temp = UseresDeJuego.useres.Find(element => element.Id == Id);
            if (temp == null)
            {
                return false;
            }
            return true;
        }
        

        public void actualizarUserEnBD()
        {
            if(!this.HayUserEnBD())
            {
                this.InsertarEnBD();
            }
            else
            { 
                this.actualizarIpEnBD(Id, Ip);
                this.actualizarEstadoDeBloqueBD(Id, EstaBloqueado);
                this.actualizarEstadoBD(Id, EstadoDeUser);
                this.actualizarLoginEnBD();
                this.actualizarClaveEnBD();
                this.actualizarNombrePublicoEnBD();
                this.actualizarAsesinatosEnBD();
                this.actualizarMatadosEnBD();
                this.actualizarAsistentesEnBD();
                this.actualizarNivelDeCuentaBD();
                this.actualizarNivelDeAccesoBD();
            }
        }
        public void actualizarLoginEnBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET login" + $" = '{Login}' WHERE Id = {Id}";
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
        public void actualizarClaveEnBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET clave" + $" = '{Clave}' WHERE Id = {Id}";
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
        public void actualizarNombrePublicoEnBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET nombrePublico" + $" = '{NombrePublico}' WHERE Id = {Id}";
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
        public void actualizarAsesinatosEnBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET asesinatos" + $" = {Asesinatos} WHERE Id = {Id}";
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
        public void actualizarMatadosEnBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET matados" + $" = {Matados} WHERE Id = {Id}";
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
        public void actualizarAsistentesEnBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET asistentes" + $" = {Asistentes} WHERE Id = {Id}";
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
        public void actualizarNivelDeCuentaBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET nivelDeCuenta" + $" = {NivelDeCuenta} WHERE Id = {Id}";
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
        public void actualizarNivelDeAccesoBD()
        {
            string nombreDeTabla = "useresDeJuego";
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET nivelDeAcceso" + $" = '{NivelDeAccesoParaUser}' WHERE Id = {Id}";
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
        public virtual void EliminarDeBD()
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(UseresDeJuego);
            string nombreDeTabla = name.Name;
            string consulta = "DELETE FROM ";
            consulta += nombreDeTabla;
            consulta += " WHERE Id = " + Id;
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

        public virtual void Registrarse()
        {
            if (!HayUserEnBD() && IsLoginDisponible(Login))
            {
                this.VerificarContrasena(Clave);
                this.InsertarEnBD();
            }
        }

        public void Entrar()
        {
            if (HayUserEnBD() && !IsLoginDisponible(Login))
            {
                // damos el acceso a user
            }
            else
            {
                // User no esta registrado, avisamos
            }
        }


    }
}
