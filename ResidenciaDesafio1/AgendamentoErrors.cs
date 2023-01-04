using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidenciaDesafio1
{
    public class AgendamentoErrors
    {
        private readonly Dictionary<AgendamentoField, string> errors;

        public AgendamentoErrors() 
        {
            errors = new();
        }

        public void AddError(AgendamentoField field, string message)
        {
            errors.Add(field, message);
        }

        public void Clear()
        {
            errors.Clear();
        }

        public bool IsEmpty => errors.Count == 0;

        public bool HasError(AgendamentoField field)
        {
            return errors.TryGetValue(field, out var _);
        }

        public string GetErrorMessage(AgendamentoField field)
        {
            return HasError(field) ? errors[field] : string.Empty;
        }
    }
}
