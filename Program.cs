using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PersonasUnregistrados man = new PersonasUnregistrados();
            Console.WriteLine(man);
            PersonasUnregistrados man2 = new PersonasUnregistrados("DimaEspectulador", "192.168.1.1", Personaje.Estado.online);
            Console.WriteLine(man2);
            man2.GenerarNombreTemporal();
            Console.WriteLine(man2);
            
            Console.ReadLine();
        }
    }
}
