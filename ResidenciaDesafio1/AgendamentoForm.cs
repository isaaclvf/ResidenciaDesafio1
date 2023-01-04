using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class AgendamentoForm
    {
        public string? CPF { get; private set; }
        public string? Data { get; private set; }

        public string? HoraInicial { get; private set; }
        public string? HoraFinal { get; private set; }

        public void ReadData()
        {
            ReadData(null);
        }

        public void ReadCancelamento()
        {
            ReadCancelamento(null);
        }

        public void ReadData(AgendamentoValidator? validator)
        {
            if (validator != null)
            {
                foreach (AgendamentoField field in Enum.GetValues(typeof(AgendamentoField)))
                {
                    var msg = validator.Errors.GetErrorMessage(field);

                    if (msg.Length > 0)
                        Console.WriteLine("Erro: {0}", msg);
                }
                Console.WriteLine();
            }

            if (validator == null || validator.Errors.HasError(AgendamentoField.CPF))
            {
                Console.Write("CPF: ");
                CPF = Console.ReadLine();
            }

            if (validator == null || validator.Errors.HasError(AgendamentoField.DATA))
            {
                Console.Write("Data: ");
                Data = Console.ReadLine();
            }

            if (validator == null || validator.Errors.HasError(AgendamentoField.HORA_INICIAL))
            {
                Console.Write("Hora inicial (HHMM): ");
                HoraInicial = Console.ReadLine();
            }

            if (validator == null || validator.Errors.HasError(AgendamentoField.HORA_FINAL))
            {
                Console.Write("Hora final (HHMM): ");
                HoraFinal = Console.ReadLine();
            }
        }

        public void ReadCancelamento(AgendamentoValidator? validator)
        {
            if (validator != null)
            {
                foreach (AgendamentoField field in Enum.GetValues(typeof(AgendamentoField)))
                {
                    var msg = validator.Errors.GetErrorMessage(field);

                    if (msg.Length > 0)
                        Console.WriteLine("Erro: {0}", msg);
                }
                Console.WriteLine();
            }

            if (validator == null || validator.Errors.HasError(AgendamentoField.CPF))
            {
                Console.Write("CPF: ");
                CPF = Console.ReadLine();
            }

            if (validator == null || validator.Errors.HasError(AgendamentoField.DATA))
            {
                Console.Write("Data: ");
                Data = Console.ReadLine();
            }

            if (validator == null || validator.Errors.HasError(AgendamentoField.HORA_INICIAL))
            {
                Console.Write("Hora inicial: ");
                HoraInicial = Console.ReadLine();
            }
        }
    }
}
