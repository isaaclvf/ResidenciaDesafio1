using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class PacienteForm
    {
        public string? Nome { get; private set; }
        public string? CPF { get; private set; }
        public string? DataNascimento { get; private set; }

        public void ReadData()
        {
            ReadData(null);
        }

        public void ReadData(PacienteValidator? validator)
        {
            if (validator != null)
            {
                foreach (PacienteField field in Enum.GetValues(typeof(PacienteField)))
                {
                    var msg = validator.Errors.GetErrorMessage(field);

                    if (msg.Length > 0)
                        Console.WriteLine("{0}: {1}", field.ToString(), msg);
                }
                Console.WriteLine();
            }

            if (validator == null || validator.Errors.HasError(PacienteField.NOME))
            {
                Console.Write("Nome: ");
                Nome = Console.ReadLine();
            }

            if (validator == null || validator.Errors.HasError(PacienteField.CPF))
            {
                Console.Write("CPF: ");
                CPF = Console.ReadLine();
            }

            if (validator == null || validator.Errors.HasError(PacienteField.DATA_NASCIMENTO))
            {
                Console.Write("Data de Nascimento (DD/MM/AAAA): ");
                DataNascimento = Console.ReadLine();
            }
        }
    }
}
