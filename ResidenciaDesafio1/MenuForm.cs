using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class MenuForm
    {
        public int ReadMenuPrincipal()
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1-Cadastro de pacientes");
            Console.WriteLine("2-Agenda");
            Console.WriteLine("3-Fim");

            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var res = Int32.Parse(input);
                    return res;
                }
                catch (Exception)
                {
                    Console.WriteLine("Opção inválida");
                    continue;
                }
            }
        }
    }
}
