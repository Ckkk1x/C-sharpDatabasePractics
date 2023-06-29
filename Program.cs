using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    public class Program
    {
        static void Main(string[] args)
        {
            //necesitamos llamar UseresDeJuego, para inicializar lista .useres con valor
            UseresDeJuego listaDeUseres = new UseresDeJuego();
            UserDeJuego user = new UserDeJuego("123.3.2.21", Personaje.Estado.online, "DimaXia", "clave", "Dimon");
            user.Id = 1;
            user.InsertarEnBD();
            user.CambiarLogin("DestroyMySelf");
            user.actualizarUserEnBD();
            //user.CambiarLogin("Dimochka");
            //user.actualizarUserEnBD();




            Console.ReadLine();
        }
    }
}
