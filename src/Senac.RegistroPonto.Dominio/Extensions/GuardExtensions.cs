using Ardalis.GuardClauses;
using Senac.RegistroPonto.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senac.RegistroPonto.Domain.Extensions
{
    public static class GuardExtensions
    {
        public static void RelacionadoTemMesmoTipo(this IGuardClause guard, ETipoPonto tipoModel, ETipoPonto tipoRelacionado)
        {
            if (tipoModel == tipoRelacionado)
                throw new ArgumentException("Registro de Ponto relacionado não deve ter o tipo de ponto igual ao Registro de Ponto referência");
        }
        public static void RelacionadoTemDataHoraInvalida(this IGuardClause guard, ETipoPonto tipoModel, DateTimeOffset dataHoraModel, DateTimeOffset dataHoraRelacionado)
        {
            var isValid = false;
            if (tipoModel == ETipoPonto.ENTRADA)
                isValid = dataHoraModel < dataHoraRelacionado;
            else
                isValid = dataHoraModel > dataHoraRelacionado;

            if (!isValid)
                throw new ArgumentException("Data do Registro de Ponto relacionado é inválida");
        }
        public static void RelacionadoTemColaboradorIdDiferente(this IGuardClause guard, Guid modelColabId, Guid relacionadoColabId)
        {
            if (modelColabId != relacionadoColabId)
                throw new ArgumentException("Registros de Ponto relacionados devem ter o mesmo Id de Colaborador");
        }
    }
}
