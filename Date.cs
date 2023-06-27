using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN_PROJECT
{
    public class Date
    {
        private int dia;
        private int mes;
        private int ano;

        public int Dia { get => dia; set => dia = value; }
        public int Mes { get => mes; set => mes = value; }
        public int Ano { get => ano; set => ano = value; }


        static public bool Compare(Date firstDate, Date secondDate)
        {
            if( firstDate.Dia == secondDate.Dia && firstDate.Mes == secondDate.Mes && firstDate.Ano == secondDate.Ano)
                return true;
            return false;
        }
        public void PrintDate()
        {
            Console.WriteLine($"{Dia}/{Mes}/{Ano}");
        }

        public bool IsIqual(Date otherDate)
        {
            if (this.Dia == otherDate.Dia && this.Mes == otherDate.Mes && this.Ano == otherDate.Ano)
                return true;
            return false;
        }
    }
}
