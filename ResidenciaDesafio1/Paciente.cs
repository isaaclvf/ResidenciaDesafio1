using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class Paciente
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public Paciente(string nome, string cPF, DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cPF;
            DataNascimento = dataNascimento;
        }
    }
}
