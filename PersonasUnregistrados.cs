using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    abstract public class PersonasUnregistrados : Personaje
    {
        private string nombreTemporal;

        public PersonasUnregistrados() : base()
        {
            GenerarNombreTemporal();
        }
        public PersonasUnregistrados(string nombreTemporal, string ip, Estado estado) : base(ip, estado)
        {
            this.nombreTemporal = nombreTemporal;
        }

        public string NombreTemporal { get => nombreTemporal; set => nombreTemporal = value; }

        public string GenerarNombreTemporal()
        {
            Random rnd = new Random();
            int cantidadDeLetras = rnd.Next(11) + 8;
            string name = "user";
            bool isNumber;
            for(int i = 0; i < cantidadDeLetras; i++)
            {
                isNumber = rnd.Next(2) == 0;
                if(isNumber)
                    name += (char)(rnd.Next(25 + 1) + 97); // [97 - 122]
                else
                    name += rnd.Next(10); // [97 - 122]
            }

            NombreTemporal = name;
            return name;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nnombreTemporal = {NombreTemporal}";
        }
    }
}
