using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    public class Espectador : PersonasUnregistrados
    {
        private Date tiempoDeAcceso;

        public Date TiempoDeAcceso { get => tiempoDeAcceso; set => tiempoDeAcceso = value; }

        public void CambiarTiempoDeAcceso(Date tiempo)
        {
            TiempoDeAcceso = tiempo;
        }
    }
}
