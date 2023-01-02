using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class PacienteValidator
    {
        private readonly PacienteErrors errors = new PacienteErrors();
        public PacienteDTO Paciente { get; private set; }

        public PacienteValidator()
        {
            Paciente = new();
        }

        public PacienteErrors Errors { get { return errors; } }

        public bool IsValid(string nome, string cpf, string strDataNascimento)
        {
            errors.Clear();

            // Nome 
            nome = nome.Trim();
            if (nome.Length < 5)
                errors.AddError(PacienteField.NOME, "Nome deve ter ao menos 5 letras");
            else
                Paciente.Nome = nome;

            // CPF
            cpf = cpf.Trim();
            if (!cpf.IsValidCPF())
                errors.AddError(PacienteField.CPF, "CPF inválido");
            // TODO: Fazer uma condição se o paciente já estiver no cadastro
            else
                Paciente.CPF = cpf;

            // Data de Nascimento
            try
            {
                Paciente.DataNascimento = DateTime.ParseExact(
                    strDataNascimento, 
                    "dd/MM/yyyy", 
                    System.Globalization.CultureInfo.InvariantCulture);

                if (Paciente.DataNascimento > DateTime.Now.AddYears(-13))
                    errors.AddError(PacienteField.DATA_NASCIMENTO, 
                        "O paciente deve ter pelo menos 13 anos no momento do cadastro");
            }
            catch (Exception)
            {
                errors.AddError(PacienteField.DATA_NASCIMENTO, 
                    "Data de nascimento deve estar no formato DD/MM/AAAA");
            }

            return errors.IsEmpty;
        }
    }
}
