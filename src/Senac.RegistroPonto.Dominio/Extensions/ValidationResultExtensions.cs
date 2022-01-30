using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senac.RegistroPonto.Domain.Extensions
{
    public static class ValidationResultExtensions
    {
        public static IEnumerable<string> GetErrorModel(this ValidationResult validation)
        {
            return validation.Errors.Select(x => x.ErrorMessage);
        }
        public static void AddError(this ValidationResult validation, string errorMessage)
        {
            validation.Errors.Add(new ValidationFailure(string.Empty, errorMessage));
        }
        public static void AddErrorRange(this ValidationResult validation, ValidationResult from)
        {
            validation.Errors.AddRange(from.Errors);
        }
    }
}
