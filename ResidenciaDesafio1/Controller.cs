using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class Controller
    {
        public static void Start()
        {
            var menuForm = new MenuForm();
            var escolha = menuForm.ReadMenuPrincipal();
            Console.WriteLine(escolha);
        }
    }
}
