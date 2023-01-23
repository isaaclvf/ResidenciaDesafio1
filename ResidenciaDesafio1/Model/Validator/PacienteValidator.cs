using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResidenciaDesafio1.Model;
using ResidenciaDesafio1.Model.DTO;

namespace ResidenciaDesafio1.Model.Validator
{
    public class PacienteValidator
    {
        private readonly PacienteErrors errors = new PacienteErrors();
        private readonly Cadastro cadastro;

        public PacienteValidator(Cadastro cadastro)
        {
            Paciente = new();
            // Usado na validação de CPF
            this.cadastro = cadastro;
        }

        public PacienteDTO Paciente { get; private set; }

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
            if (cadastro.IsCadastrado(cpf))
                errors.AddError(PacienteField.CPF, "CPF já cadastrado");
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
