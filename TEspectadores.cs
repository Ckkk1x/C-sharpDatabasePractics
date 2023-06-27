using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    public class TEspectadores
    {
        public List<Espectador> listaDeEspectadores;

        public Espectador EncontrarPorID(int id)
        {
            return listaDeEspectadores.Find( element => element.Id == id);
        }
    }
}
