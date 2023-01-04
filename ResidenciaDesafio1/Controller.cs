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
            var cadastro = new Cadastro();
            var cadastroUI = new CadastroUI(cadastro);

            while (menuForm.OpcPrincipal != 3)
            {
                Console.Clear();
                menuForm.ReadMenuPrincipal();

                // Cadastro de pacientes
                if (menuForm.OpcPrincipal == 1)
                {
                    Console.Clear();
                    menuForm.ReadMenuPacientes();

                    // Cadastrar novo paciente
                    if (menuForm.OpcPacientes == 1)
                    {
                        Console.Clear();
                        CadastrarPaciente(cadastro);
                        Console.ReadLine();
                    }

                    // Excluir paciente
                    if (menuForm.OpcPacientes == 2)
                    {
                        Console.Clear();
                        ExcluirPaciente(cadastro);
                        Console.ReadLine();
                    }

                    // Listar pacientes (CPF)
                    if (menuForm.OpcPacientes == 3)
                    {
                        Console.Clear();
                        cadastroUI.ListarPacientes(porCPF: true);
                        Console.ReadLine();
                    }

                    // Listar pacientes (Nome)
                    if (menuForm.OpcPacientes == 4)
                    {
                        Console.Clear();
                        cadastroUI.ListarPacientes();
                        Console.ReadLine();
                    }

                    // Voltar
                    if (menuForm.OpcPacientes == 5)
                        continue;
                }

                // Agenda
                if (menuForm.OpcPrincipal == 2)
                {
                    Console.Clear();
                    menuForm.ReadMenuAgenda();

                    // Agendar consulta
                    if (menuForm.OpcAgenda == 1)
                    {
                        Console.Clear();
                        AgendarConsulta(cadastro);
                        Console.ReadLine();
                    }

                    // Cancelar agendamento
                    if (menuForm.OpcAgenda == 2)
                    {
                        Console.Clear();
                        CancelarAgendamento(cadastro);
                        Console.ReadLine();
                    }

                    // Listar agenda
                    if (menuForm.OpcAgenda == 3)
                    {
                        Console.Clear();
                        ListarAgenda(cadastroUI);
                        Console.ReadLine();
                    }

                    // Voltar
                    if (menuForm.OpcAgenda == 4)
                        continue;
                }
            }
        }

        public static void CadastrarPaciente(Cadastro cadastro)
        {
            bool isValid = false;
            var form = new PacienteForm();
            var validator = new PacienteValidator(cadastro);
            form.ReadData();

            while (!isValid)
            {
                isValid = validator.IsValid(form.Nome, form.CPF, form.DataNascimento);

                if (isValid)
                {
                    var paciente = new Paciente(validator.Paciente.Nome, validator.Paciente.CPF, validator.Paciente.DataNascimento);
                    cadastro.AddPaciente(paciente);
                }
                else
                {
                    form.ReadData(validator);
                }
            }

            Console.WriteLine("Paciente cadastrado com sucesso!");
        }

        public static void ExcluirPaciente(Cadastro cadastro)
        {
            while (true)
            {
                Console.Write("CPF: ");
                var cpf = Console.ReadLine().Trim();

                if (cadastro.IsCadastrado(cpf))
                {
                    if (cadastro.GetAgendamento(cpf) != null)
                    {
                        Console.WriteLine("Erro: paciente está agendado");
                        return;

                    }
                    cadastro.RemovePaciente(cpf);
                    Console.WriteLine("Paciente excluído com sucesso!");
                    return;
                }
                else
                {
                    Console.WriteLine("Erro: paciente não cadastrado");
                }
            }
        }

        public static void AgendarConsulta(Cadastro cadastro)
        {
            bool isValid = false;
            var form = new AgendamentoForm();
            var validator = new AgendamentoValidator(cadastro);
            form.ReadData();

            while (!isValid)
            {
                isValid = validator.IsValid(form.CPF, form.Data, form.HoraInicial, form.HoraFinal);

                if (isValid)
                {
                    var agendamento = new Agendamento(
                        validator.Agendamento.CPF, 
                        validator.Agendamento.Data, 
                        validator.Agendamento.HoraInicial,
                        validator.Agendamento.HoraFinal);

                    cadastro.AddAgendamento(agendamento);
                    Console.WriteLine("Agendamento realizado com sucesso!");
                }
                else
                {
                    form.ReadData(validator);
                }
            }
        }

        public static void CancelarAgendamento(Cadastro cadastro)
        {
            bool isValid = false;
            var form = new AgendamentoForm();
            var validator = new AgendamentoValidator(cadastro);
            form.ReadCancelamento();

            while (!isValid)
            {
                isValid = validator.IsValidCancelamento(form.CPF, form.Data, form.HoraInicial);

                if (isValid)
                {
                    cadastro.RemoveAgendamento(
                        validator.Agendamento.CPF,
                        validator.Agendamento.Data,
                        validator.Agendamento.HoraInicial);
                    Console.WriteLine("Agendamento cancelado com sucesso!");
                    return;
                }
                else
                {
                    form.ReadData(validator);
                }
            }
        }

        public static void ListarAgenda(CadastroUI cadastroUI)
        {
            var form = new AgendamentoForm();
            form.ReadOpcListagem();

            // Agenda (t)oda
            if (form.OpcListagem == 't') 
            {
                cadastroUI.ListarAgenda();
                return;
            }

            // Agenda (p)eríodo
            // TODO: Criar classe(s) para essa leitura e validação
            string? strDataInicial = null;
            string? strDataFinal = null;
            DateOnly dataInicial;
            DateOnly dataFinal;

            while (true)
            {
                Console.Write("Data inicial: ");
                strDataInicial = Console.ReadLine().Trim();

                Console.Write("Data final: ");
                strDataFinal = Console.ReadLine().Trim();

                try
                {
                    dataInicial = DateOnly.ParseExact(strDataInicial, "dd/MM/yyyy");
                    dataFinal = DateOnly.ParseExact(strDataFinal, "dd/MM/yyyy");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Data deve ser no formato DD/MM/AAAA");
                }
            }

            cadastroUI.ListarAgenda(dataInicial, dataFinal);
        }
    }
}
