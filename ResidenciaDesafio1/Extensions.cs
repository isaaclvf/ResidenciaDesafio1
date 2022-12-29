using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public static class Extensions
    {
        public static bool IsValidCPF(this string cpf)
        {
            // 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Todos iguais
            if (cpf.All(digito => digito == cpf[0]))
                return false;

            // Checar primeiro dígito verificador (j)
            int j = 0;
            int somaJ = 0;
            for (int i = 0; i < 9; i++)
                somaJ += (int)Char.GetNumericValue(cpf[i]) * (10 - i);

            if (!(somaJ % 11 == 0 || somaJ % 11 == 1))
                j = 11 - (somaJ % 11);

            if ((int)Char.GetNumericValue(cpf[9]) != j)
                return false;

            // Checar segundo dígito verificador (k)
            int k = 0;
            int somaK = 0;
            for (int i = 0; i < 9; i++)
                somaK += (int)Char.GetNumericValue(cpf[i]) * (11 - i);

            if (!(somaK % 11 == 0 || somaK % 11 == 1))
                k = 11 - (somaK % 11);

            if ((int)Char.GetNumericValue(cpf[10]) != k)
                return false;

            return true;
        }
    }
}
