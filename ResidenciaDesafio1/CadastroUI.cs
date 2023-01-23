using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResidenciaDesafio1.Model;

namespace ResidenciaDesafio1
{
    public class CadastroUI
    {
        private readonly Cadastro cadastro;

        public CadastroUI(Cadastro cadastro)
        {
            this.cadastro = cadastro;
        }

        public void ListarPacientes(bool porCPF = false)
        {
            Func<Paciente, string> filter;

            if (porCPF)
                filter = p => p.CPF;
            else
                filter = p => p.Nome;

            // https://learn.microsoft.com/en-us/dotnet/api/system.string.format
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("{0,-11} {1,-32} {2,-10} {3,-5}", "CPF", "Nome", "Dt.Nasc.", "Idade");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var p in cadastro.Pacientes.OrderBy(filter))
            {
                // Calculo de idade:
                // https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-based-on-a-datetime-type-birthday
                Console.WriteLine("{0,-11} {1,-32} {2,-10} {3,-5}",
                    p.CPF,
                    p.Nome,
                    p.DataNascimento.ToShortDateString(),
                    (int)((DateTime.Now - p.DataNascimento).TotalDays / 365.242199));

                var agendamento = cadastro.GetAgendamento(p.CPF);
                if (agendamento != null)
                    Console.WriteLine("{0,-11} Agendado para: {1} \n{2,-11} {3} às {4}",
                        null,
                        agendamento.Data,
                        null,
                        agendamento.HoraInicial, 
                        agendamento.HoraFinal);
            }

            Console.WriteLine("-------------------------------------------------------------");
        }

        public void ListarAgenda(DateOnly? dataInicial = null, DateOnly? dataFinal = null)
        {
            // Agenda completa
            var agenda = cadastro.GetAgenda();

            // Agenda período
            if (dataInicial != null && dataFinal != null)
                agenda = agenda.Where(a => a.Data >= dataInicial && a.Data <= dataFinal).ToList();

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("{0,-11} {1,5} {2,5} {3,5} {4,-21} {5,-11}", "Data", "H.Ini", "H.Fim", "Tempo", "Nome", "Dt.Nasc.");
            Console.WriteLine("-------------------------------------------------------------");

            List<DateOnly> datasListadas = new();

            foreach (var agendamento in agenda.OrderBy(a => a.Data).ThenBy(a => a.HoraInicial))
            {
                var paciente = cadastro.GetPaciente(agendamento.CPF);

                // Se tiver mais de um agendamento no mesmo dia, mostra a data apenas uma vez
                var strData = agendamento.Data.ToString();
                if (datasListadas.Contains(agendamento.Data))
                    strData = null;

                Console.WriteLine("{0,-11} {1,5} {2,5} {3,5} {4,-21} {5,-11}",
                    strData,
                    agendamento.HoraInicial,
                    agendamento.HoraFinal,
                    agendamento.HoraFinal - agendamento.HoraInicial,
                    paciente.Nome,
                    paciente.DataNascimento);

                datasListadas.Add(agendamento.Data);
            }

            Console.WriteLine("-------------------------------------------------------------");
        }
    }
}
