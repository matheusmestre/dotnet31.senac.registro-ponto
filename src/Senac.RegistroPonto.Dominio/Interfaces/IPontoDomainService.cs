using FluentValidation.Results;
using Senac.RegistroPonto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.Domain.Interfaces
{
    public interface IPontoDomainService
    {
        ValidationResult ValidarLancamentoPonto(Ponto pontoAtual, Ponto pontoAnterior);
    }
}
