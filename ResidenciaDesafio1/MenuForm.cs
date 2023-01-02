using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class MenuForm
    {
        public int OpcPrincipal { get; private set; }
        public int OpcPacientes { get; private set; }
        public int OpcAgenda { get; private set; }

        public void ReadMenuPrincipal()
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
                    var opc = Int32.Parse(input);

                    if (opc < 1 || opc > 3)
                    {
                        Console.WriteLine("Opção inválida");
                        continue;
                    }

                    OpcPrincipal = opc;
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine("Opção inválida");
                    continue;
                }
            }
        }

        public void ReadMenuPacientes()
        {
            Console.WriteLine("Menu do Cadastro de Pacientes");
            Console.WriteLine("1-Cadastrar novo paciente");
            Console.WriteLine("2-Excluir paciente");
            Console.WriteLine("3-Listar pacientes (ordenado por CPF)");
            Console.WriteLine("4-Listar pacientes (ordenado por nome)");
            Console.WriteLine("5-Voltar p/ menu principal");

            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var opc = Int32.Parse(input);

                    if (opc < 1 || opc > 5)
                    {
                        Console.WriteLine("Opção inválida");
                        continue;
                    }

                    OpcPacientes = opc;
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine("Opção inválida");
                    continue;
                }
            }
        }

        public void ReadMenuAgenda()
        {
            Console.WriteLine("Agenda");
            Console.WriteLine("1-Agendar consulta");
            Console.WriteLine("2-Cancelar agendamento");
            Console.WriteLine("3-Listar agenda");
            Console.WriteLine("4-Voltar p/ menu principal");

            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var opc = Int32.Parse(input);

                    if (opc < 1 || opc > 4)
                    {
                        Console.WriteLine("Opção inválida");
                        continue;
                    }

                    OpcAgenda = opc;
                    return;
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
