using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class AgendamentoDTO
    {
        public string CPF { get; set; } = string.Empty;
        public DateOnly Data { get; set; } = DateOnly.MinValue;
        public TimeOnly HoraInicial { get; set; } = TimeOnly.MinValue;
        public TimeOnly HoraFinal { get; set; } = TimeOnly.MinValue;
    }
}
