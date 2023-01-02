using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            // TODO: Instanciar um objeto Cadastro

            while (menuForm.OpcPrincipal != 3)
            {
                Console.Clear();
                menuForm.ReadMenuPrincipal();

                // Cadastro de pacientes
                if (menuForm.OpcPrincipal == 1)
                {
                    Console.Clear();
                    menuForm.ReadMenuCadastro();

                    // Cadastrar novo paciente
                    if (menuForm.OpcCadastro == 1)
                    {
                        Console.Clear();
                        var paciente = ReadPaciente();
                        // Adicionar paciente ao cadastro
                        Console.ReadLine();
                    }

                    // Excluir paciente

                    // Listar pacientes (CPF)

                    // Listar pacientes (Nome)

                    // Voltar
                    if (menuForm.OpcCadastro == 5)
                        continue;
                }

                // Agenda
                if (menuForm.OpcPrincipal == 2)
                {
                    Console.Clear();
                    menuForm.ReadMenuAgenda();

                    // Agendar consulta

                    // Cancelar agendamento

                    // Listar agenda

                    // Voltar
                    if (menuForm.OpcAgenda == 4)
                        continue;
                }
            }
        }

        public static Paciente ReadPaciente()
        {
            bool isValid = false;
            var form = new PacienteForm();
            var validador = new PacienteValidator();
            Paciente? paciente = null;

            form.ReadData();

            while (!isValid)
            {
                isValid = validador.IsValid(form.Nome, form.CPF, form.DataNascimento);

                if (isValid)
                {
                    paciente = new Paciente(validador.Paciente.Nome, validador.Paciente.CPF, validador.Paciente.DataNascimento);
                }
                else
                {
                    form.ReadData(validador);
                }
            }

            return paciente;
        }
    }
}
