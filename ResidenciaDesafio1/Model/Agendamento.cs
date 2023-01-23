using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1.Model
{
    public class Agendamento
    {
        public string CPF { get; private set; }
        public DateOnly Data { get; private set; }
        public TimeOnly HoraInicial { get; private set; }
        public TimeOnly HoraFinal { get; private set; }

        public Agendamento(string cpf, DateOnly data, TimeOnly horaInicial, TimeOnly horaFinal)
        {
            CPF = cpf;
            Data = data;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
        }
    }
}
