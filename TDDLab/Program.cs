using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witam w programie BANK TDD");
            Console.WriteLine("Wybierz odpowiednią funkcjonalność:");
            Console.WriteLine("1 - Wyciąg z konta");
            Console.WriteLine("2 - Histria transakcji");
            Console.WriteLine("3 - Generuj pismo");

            int selectedOption= 0;

            selectedOption = Convert.ToInt32(Console.ReadLine());

            switch (selectedOption)
            {
                case 1:
                    Console.WriteLine("Wybrano 1");
                    break;
                case 2:
                    Console.WriteLine("Wybrano 2");
                    break;
                default:
                    Console.WriteLine("Błędny wybór");
                    break;
            }


            for (;;) { }
        }
    }
}
