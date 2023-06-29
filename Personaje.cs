using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    abstract public class Personaje
    {
        private int id;
        private string ip;
        private bool estaBloqueado;
        private Estado estadoDeUser; // Atencion! En DB se llama estado.

        public static readonly string ruta = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Public\Documents\game2\C-sharpDatabasePractics\MainDB.mdb";

        private static int cantidadDeUsers;
        public int Id { get => id; set => id = value; }
        public string Ip { get => ip; set => ip = value; }
        public Estado EstadoDeUser { get => estadoDeUser; set => estadoDeUser = value; }
        public static int CantidadDeUsers { get => cantidadDeUsers; set => cantidadDeUsers = value; }
        public bool EstaBloqueado { get => estaBloqueado; set => estaBloqueado = value; }

        public enum Estado { offline, online,  hidden};

        public Personaje()
        {
            id = cantidadDeUsers;
            ip = null;
            EstaBloqueado = false;
            EstadoDeUser = Estado.hidden;

            cantidadDeUsers++;
        }
        public Personaje(string ip, Estado estado) 
        {
            id = cantidadDeUsers;
            this.ip = ip;
            EstaBloqueado = false;
            this.estadoDeUser = estado;

            cantidadDeUsers++;
        }
        public void Bloquearse()
        {
            EstaBloqueado = true;
        }
        public void Desbloquearse()
        {
            EstaBloqueado = false;
        }
        public bool GetEstadoDeBloque()
        {
            return EstaBloqueado;
        }
        

        public virtual void LeerBD()
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(Personaje);
            string nombreDeTabla = name.Name;
            string consulta = "SELECT * FROM ";
            consulta += nombreDeTabla;
            using (OleDbConnection conexion = new OleDbConnection(ruta))
            {    
                OleDbCommand comando = new OleDbCommand(consulta, conexion);
                try
                {
                    conexion.Open();
                    OleDbDataReader miTabla = comando.ExecuteReader();
                    Console.WriteLine("Database MainDB, tabla " + nombreDeTabla);
                    while (miTabla.Read())
                    {
                        Console.WriteLine("{0} {1} {2} {3}", miTabla["Id"].ToString(), miTabla["Ip"].ToString(), miTabla["estaBloqueado"].ToString(), miTabla["estado"].ToString()); //TODO
                    }
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        
        public virtual void EliminarDeBD(int id)
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(Personaje);
            string nombreDeTabla = name.Name;
            string consulta = "DELETE FROM ";
            consulta += nombreDeTabla;
            consulta += " WHERE Id = " + id;
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
        public virtual void InsertarEnBD(string ip, bool estaBloqueado, Estado estado)
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(Personaje);
            string nombreDeTabla = name.Name;
            string consulta = "INSERT INTO ";
            string estdo = estado.ToString();
            consulta += nombreDeTabla;
            consulta += "(Ip, estaBloqueado, estado) VALUES";
            consulta += $"('{ip}', {estaBloqueado}, '{estdo}')";
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

        /*
         * UPDATE Clientes SET Nombre = '{nombre}' WHERE Id = {id}
         */
        public void actualizarIpEnBD(int id, string ip)
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(Personaje);
            string nombreDeTabla = name.Name;
            string consulta = "UPDATE ";
            consulta += nombreDeTabla; 
            consulta += " SET Ip" + $" = '{ip}' WHERE Id = {id}";
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

        public void actualizarEstadoDeBloqueBD(int id, bool bloque)
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(Personaje);
            string nombreDeTabla = name.Name;
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            consulta += " SET estaBloqueado" + $" = {bloque} WHERE Id = {id}";
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

        public void actualizarEstadoBD(int id, Estado estado)
        {
            // hemos recibido nombre de classe en que estamos
            Type name = typeof(Personaje);
            string nombreDeTabla = name.Name;
            string consulta = "UPDATE ";
            consulta += nombreDeTabla;
            string estadoStr = estado.ToString();
            consulta += " SET estado" + $" = '{estadoStr}' WHERE Id = {id}";
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
        public override string ToString()
        {
            return $"id = {this.id}\nip = {this.ip ?? "null"}\nestaBloqueado = {this.EstaBloqueado}\nestado = {this.estadoDeUser}";
        }

    }
}
