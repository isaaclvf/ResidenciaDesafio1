using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class PacienteErrors
    {
        private readonly Dictionary<PacienteField, string> errors;

        public PacienteErrors()
        {
            errors = new();
        }

        public void AddError(PacienteField field, string message)
        {
            errors.Add(field, message);
        }

        public void Clear()
        {
            errors.Clear();
        }
        public bool IsEmpty => errors.Count == 0;

        public bool HasError(PacienteField field)
        {
            return errors.TryGetValue(field, out var _);
        }

        public string GetErrorMessage(PacienteField field)
        {
            return HasError(field) ? errors[field] : string.Empty;
        }
    }
}
