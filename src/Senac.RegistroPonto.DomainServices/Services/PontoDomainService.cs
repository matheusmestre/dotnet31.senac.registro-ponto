using FluentValidation.Results;
using Senac.RegistroPonto.Domain.Entidades;
using Senac.RegistroPonto.Domain.Enums;
using Senac.RegistroPonto.Domain.Extensions;
using Senac.RegistroPonto.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.DomainServices.Services
{
    public class PontoDomainService : IPontoDomainService
    {        
        public ValidationResult ValidarLancamentoPonto(Ponto pontoAtual, Ponto pontoAnterior)
        {
            var validation = new ValidationResult();
            
            if (pontoAtual.Tipo == ETipoPonto.ENTRADA && pontoAnterior == default)
                return validation;

            if (pontoAtual.Tipo == ETipoPonto.SAIDA && pontoAnterior == default)
                validation.AddError("Ponto de Entrada não encontrado para este Ponto de Saída");

            if (pontoAtual.Tipo == pontoAnterior?.Tipo)
            {
                if (pontoAtual.Tipo == ETipoPonto.ENTRADA)
                    validation.AddError("Cadastre um Ponto de Saída antes de cadastrar um de Entrada");
                else
                    validation.AddError("Cadastre um Ponto de Entrada antes de cadastrar um de Saída");
            }

            if (pontoAtual.DataHora <= pontoAnterior?.DataHora)
                validation.AddError("Data/Hora do Ponto não deve ser menor ou igual á Data/Hora do ponto anterior");

            return validation;
        }        
    }
}
