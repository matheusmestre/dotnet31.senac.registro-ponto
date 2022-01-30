using Ardalis.GuardClauses;
using Senac.RegistroPonto.Domain.Enums;
using Senac.RegistroPonto.Domain.Extensions;
using Senac.RegistroPonto.Domain.Interfaces;
using System;

namespace Senac.RegistroPonto.Domain.Entidades
{
    public class Ponto : Entity, IAggregateRoot
    {
        public Ponto(Guid colaboradorId, string nomeColaborador, DateTimeOffset dataHora, ETipoPonto tipo)
        {
            Guard.Against.NullOrEmpty(colaboradorId, nameof(ColaboradorId));
            Guard.Against.NullOrWhiteSpace(nomeColaborador, nameof(NomeColaborador));

            ColaboradorId = colaboradorId;
            NomeColaborador = nomeColaborador;
            DataHora = dataHora.ConverterParaUTC();
            Tipo = tipo;
        }
        protected Ponto() { }
        public Guid ColaboradorId { get; private set; }
        public string NomeColaborador { get; private set; }
        public DateTimeOffset DataHora { get; private set; }
        public ETipoPonto Tipo { get; private set; }
        public Guid? RelacionadoId { get; private set; }
        public virtual Ponto Relacionado { get; private set; }
        public void Relacionar(Ponto relacionado)
        {
            Guard.Against.RelacionadoTemMesmoTipo(Tipo, relacionado.Tipo);
            Guard.Against.RelacionadoTemDataHoraInvalida(Tipo, DataHora, relacionado.DataHora);
            Guard.Against.RelacionadoTemColaboradorIdDiferente(ColaboradorId, relacionado.ColaboradorId);

            RelacionadoId = relacionado.Id;
        }
        public decimal ObterHorasTrabalhadas()
        {
            if (Relacionado == default)
                return 0;

            TimeSpan diff;

            if (Tipo == ETipoPonto.ENTRADA) 
                diff = Relacionado.DataHora.Subtract(DataHora);
            else 
                diff = DataHora.Subtract(Relacionado.DataHora);

            var horas = diff.Hours;
            var minutos = diff.Minutes;

            var horasTrabalhadas = horas +  minutos / 100.0m;

            return Convert.ToDecimal(horasTrabalhadas);
        }
        public override string ToString()
        {
            return $"Ponto {Tipo} - {NomeColaborador} ({ColaboradorId}) ás {DataHora}";
        }
    }
}