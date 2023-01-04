using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class AgendamentoValidator
    {
        private readonly AgendamentoErrors errors = new AgendamentoErrors();
        private readonly Cadastro cadastro;

        public AgendamentoValidator(Cadastro cadastro) 
        {
            Agendamento = new();
            this.cadastro = cadastro;
        }

        public AgendamentoDTO Agendamento { get; private set; }
        public AgendamentoErrors Errors { get { return errors; } }

        public bool IsValid(string cpf, string strData, string strHoraInicial, string strHoraFinal)
        {
            errors.Clear();

            // CPF
            cpf = cpf.Trim();
            if (!cadastro.IsCadastrado(cpf))
            {
                errors.AddError(AgendamentoField.CPF, "Paciente não cadastrado");
                return false;
            }
            else if (cadastro.GetAgendamento(cpf) != null)
            {
                errors.AddError(AgendamentoField.CPF, "Paciente já tem um agendamento");
                return false;
            }
            else
            {
                Agendamento.CPF = cpf;
            }

            // Hora inicial
            strHoraInicial = strHoraInicial.Trim();
            try
            {
                Agendamento.HoraInicial = TimeOnly.ParseExact(strHoraInicial, "HHmm");

                if (Agendamento.HoraInicial.Minute % 15 != 0)
                    errors.AddError(AgendamentoField.HORA_INICIAL,
                        "Hora inicial é sempre definida de 15 em 15 minutos");

                if (Agendamento.HoraInicial < new TimeOnly(08, 00) || Agendamento.HoraInicial > new TimeOnly(19, 00))
                    errors.AddError(AgendamentoField.HORA_INICIAL,
                        "O horário de funcionamento do consultório é das 8:00h às 19:00h");
            }
            catch (Exception)
            {
                errors.AddError(AgendamentoField.HORA_INICIAL,
                    "Hora deve estar no formato HHMM");
            }

            // Hora final
            strHoraFinal = strHoraFinal.Trim();
            try
            {
                Agendamento.HoraFinal = TimeOnly.ParseExact(strHoraFinal, "HHmm");

                if (Agendamento.HoraFinal.Minute % 15 != 0)
                    errors.AddError(AgendamentoField.HORA_FINAL,
                        "Hora final é sempre definida de 15 em 15 minutos");

                if (Agendamento.HoraFinal < Agendamento.HoraInicial)
                    errors.AddError(AgendamentoField.HORA_FINAL,
                        "Hora final não pode ser anterior a hora inicial");

                if (Agendamento.HoraFinal > new TimeOnly(19, 00))
                    errors.AddError(AgendamentoField.HORA_FINAL,
                        "O horário de funcionamento do consultório é das 8:00h às 19:00h");

                if (!cadastro.Disponivel(Agendamento))
                    errors.AddError(AgendamentoField.HORA_FINAL,
                        "Já existe uma consulta agendada nesse horário");
            }
            catch (Exception)
            {
                errors.AddError(AgendamentoField.HORA_FINAL,
                    "Hora deve estar no formato HHMM");
            }

            // Data
            try
            {
                Agendamento.Data = DateOnly.ParseExact(
                    strData,
                    "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);

                if (Agendamento.Data < DateOnly.FromDateTime(DateTime.Now))
                    errors.AddError(AgendamentoField.DATA,
                        "O agendamento deve ser para um período futuro");

                if (Agendamento.Data == DateOnly.FromDateTime(DateTime.Now)
                    && Agendamento.HoraInicial <= TimeOnly.FromDateTime(DateTime.Now))
                    errors.AddError(AgendamentoField.DATA,
                        "O agendamento deve ser para um período futuro");
            }
            catch (Exception)
            {
                errors.AddError(AgendamentoField.DATA,
                    "Data deve estar no formato DD/MM/AAAA");
            }

            return errors.IsEmpty;
        }

        public bool IsValidCancelamento(string cpf, string strData, string strHoraInicial)
        {
            errors.Clear();

            // CPF
            cpf = cpf.Trim();
            if (!cadastro.IsCadastrado(cpf))
            {
                errors.AddError(AgendamentoField.CPF, "Paciente não cadastrado");
                return false;
            }

            if (cadastro.GetAgendamento(cpf) == null)
            {
                errors.AddError(AgendamentoField.CPF, "Paciente não tem agendamento");
                return false;
            }

            // Data
            try
            {
                Agendamento.Data = DateOnly.ParseExact(
                    strData,
                    "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                errors.AddError(AgendamentoField.DATA,
                    "Data deve estar no formato DD/MM/AAAA");
            }

            // Hora inicial
            strHoraInicial = strHoraInicial.Trim();
            try
            {
                Agendamento.HoraInicial = TimeOnly.ParseExact(strHoraInicial, "HHmm");
            }
            catch (Exception)
            {
                errors.AddError(AgendamentoField.HORA_INICIAL,
                    "Hora deve estar no formato HHMM");
            }

            // Agendado
            if (!cadastro.ExisteAgendamento(Agendamento.CPF, Agendamento.Data, Agendamento.HoraInicial))
            {
                errors.AddError(AgendamentoField.DATA,
                    "Agendamento não encontrado");
            }

            return errors.IsEmpty;
        }
    }
}
