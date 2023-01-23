using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ResidenciaDesafio1.Model.DTO;

namespace ResidenciaDesafio1.Model
{
    public class Cadastro
    {
        private List<Paciente> pacientes;
        private List<Agendamento> agenda;

        public Cadastro()
        {
            pacientes = new();
            agenda = new();
        }

        public List<Paciente> Pacientes { get { return pacientes; } }

        public void AddPaciente(Paciente paciente)
        {
            pacientes.Add(paciente);
        }

        public void RemovePaciente(string cpf)
        {
            var paciente = pacientes.Find(p => p.CPF == cpf);

            if (paciente != null)
                pacientes.Remove(paciente);
        }

        public bool IsCadastrado(string cpf)
        {
            return pacientes.Any(p => p.CPF == cpf);
        }

        public Paciente? GetPaciente(string cpf)
        {
            return pacientes.FirstOrDefault(p => p.CPF == cpf);
        }

        public void AddAgendamento(Agendamento agendamento)
        {
            agenda.Add(agendamento);
        }

        public void RemoveAgendamento(string cpf, DateOnly data, TimeOnly horaInicial)
        {
            // Verificar se esse cpf tem agendamento futuro
            if (GetAgendamento(cpf) != null)
            {
                var agendamento = agenda.Where(a => a.CPF == cpf && a.Data == data && a.HoraInicial == horaInicial).FirstOrDefault();
                if (agendamento != null)
                    agenda.Remove(agendamento);
            }
        }

        public List<Agendamento> GetAgenda()
        {
            // Lista de agendamentos futuros
            return agenda.Where(agendamento =>
            {
                return agendamento.Data > DateOnly.FromDateTime(DateTime.Now)
                || agendamento.Data == DateOnly.FromDateTime(DateTime.Now)
                && agendamento.HoraInicial > TimeOnly.FromDateTime(DateTime.Now);
            }).ToList();
        }

        public Agendamento? GetAgendamento(string cpf)
        {
            var agendamentosFuturos = GetAgenda();
            return agendamentosFuturos.Where(a => a.CPF == cpf).FirstOrDefault();
        }

        public bool Disponivel(AgendamentoDTO agendamento)
        {
            var agendandamentosFuturos = GetAgenda();
            // Verifica se existe alguma sobreposição
            return !agendandamentosFuturos.Any(a =>
                a.HoraInicial < agendamento.HoraFinal
                && agendamento.HoraInicial < a.HoraFinal);
        }

        public bool ExisteAgendamento(string cpf, DateOnly data, TimeOnly horaInicial)
        {
            return GetAgenda().Any(a => a.CPF == cpf && a.Data == data && a.HoraInicial == horaInicial);
        }
    }
}
