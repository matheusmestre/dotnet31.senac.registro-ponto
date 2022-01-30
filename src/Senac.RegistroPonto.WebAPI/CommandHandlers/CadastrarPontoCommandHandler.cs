using MediatR;
using Senac.RegistroPonto.Domain.Entidades;
using Senac.RegistroPonto.Domain.Enums;
using Senac.RegistroPonto.Domain.Extensions;
using Senac.RegistroPonto.Domain.Interfaces;
using Senac.RegistroPonto.WebAPI.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.WebAPI.CommandHandlers
{
    public class CadastrarPontoCommandHandler : IRequestHandler<CadastrarPontoCommand, bool>
    {
        private readonly IPontoRepository _repository;
        private readonly IPontoDomainService _domainService;
        private readonly IUnitOfWork _uow;
        public CadastrarPontoCommandHandler(IPontoRepository repository, IPontoDomainService domainService, IUnitOfWork uow)
        {
            _repository = repository;
            _domainService = domainService;
            _uow = uow;
        }
        public async Task<bool> Handle(CadastrarPontoCommand request, CancellationToken cancellationToken)
        {
            var tipoPonto = request.TipoPonto == "E" ? ETipoPonto.ENTRADA : ETipoPonto.SAIDA;

            var ultPonto = await _repository.ObterUltimoPontoAsync(request.ColaboradorId);
            var ponto = new Ponto(request.ColaboradorId, request.NomeColaborador, request.DataHora, tipoPonto);            

            var validation = _domainService.ValidarLancamentoPonto(ponto, ultPonto);
            if (!validation.IsValid)
            {
                request.Validation.AddErrorRange(validation);
                return false;
            }

            if (ponto.Tipo == ETipoPonto.SAIDA) { 
                ultPonto.Relacionar(ponto);
                ponto.Relacionar(ultPonto);

                _repository.Alterar(ultPonto);
            }

            _repository.Adicionar(ponto);

            await _uow.CommitAsync();

            request.AggregateId = ponto.Id;

            return true;
        }        
    }
}
