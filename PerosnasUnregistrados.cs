using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    public class PerosnasUnregistrados : Personaje
    {
        private string nombreTemporal;

        public PerosnasUnregistrados()
        {
            this.nombreTemporal = string.Empty;
        }
        public PerosnasUnregistrados(string nombreTemporal)
        {
            this.nombreTemporal = nombreTemporal;
        }

        public override void Eliminarse()
        {
            // Eliminar de bases de datos
        }
    }
}
