using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Senac.RegistroPonto.Domain.Extensions;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Senac.RegistroPonto.WebAPI.Commands
{
    public class CadastrarPontoCommand: IRequest<bool>
    {
        public CadastrarPontoCommand()
        {
            Validation = new ValidationResult();
        }
        public Guid ColaboradorId { get; set; }
        public string NomeColaborador { get; set; }
        public DateTimeOffset DataHora { get; set; }
        public string TipoPonto { get; set; }

        [JsonIgnore]
        public Guid AggregateId { get; set; }

        [JsonIgnore]
        public ValidationResult Validation { get; private set; }
        public bool IsValid()
        {
            var validator = new CadastrarPontoCommandValidator();
            Validation = validator.Validate(this);

            return Validation.IsValid;
        }
    }
    internal class CadastrarPontoCommandValidator: AbstractValidator<CadastrarPontoCommand>
    {
        public CadastrarPontoCommandValidator()
        {
            RuleFor(x => x.ColaboradorId).NotEmpty().WithMessage("Id do Colaborador é obrigatório");
            RuleFor(x => x.NomeColaborador).NotEmpty().WithMessage("Nome do Colaborador é obrigatório");
            RuleFor(x => x.DataHora).NotEmpty().WithMessage("Data/Hora é obrigatório");
            RuleFor(x => x.DataHora).LessThan(DateTimeOffset.UtcNow).WithMessage("Data/Hora deve estar no passado");            
            RuleFor(x => x.TipoPonto).Must(BeValidTipoPonto).WithMessage("Tipo de Ponto deve ser E = Entrada ou S = Saída");
        }
        private bool BeValidTipoPonto(string tipoPonto)
        {
            var tiposValidos = new string[] { "E", "S" };
            return tiposValidos.Contains(tipoPonto);
        }
    }
}