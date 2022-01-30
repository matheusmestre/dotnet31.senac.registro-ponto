using MediatR;
using Microsoft.AspNetCore.Mvc;
using Senac.RegistroPonto.Domain.Entidades;
using Senac.RegistroPonto.Domain.Extensions;
using Senac.RegistroPonto.Domain.Interfaces;
using Senac.RegistroPonto.WebAPI.Commands;
using Senac.RegistroPonto.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.WebAPI.Controllers
{
    [ApiController]
    [Route("api/registro-ponto")]
    public class PontoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPontoRepository _repository;
        public PontoController(IMediator mediator, IPontoRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost]        
        public async Task<IActionResult> PostAsync([FromBody] CadastrarPontoCommand command)
        {
            if (command == null)
                return BadRequest();

            if (command.IsValid())
            {
                var result = await _mediator.Send(command);
                if (result)
                    return Created(string.Empty, command.AggregateId);
            }

            return BadRequest(command.Validation.GetErrorModel());
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReportAsync([FromQuery]Guid colaboradorId)
        {
            if (colaboradorId == Guid.Empty)
                return BadRequest();
           
            var model = await _repository.ObterRelatorioHorasTrabalhadasAsync(colaboradorId);
            var viewModel = ConverterParaRelatorioHorasTrabalhadasViewModel(model);

            return Ok(viewModel);
        }
        private IEnumerable<RelatorioHorasTrabalhadasViewModel> ConverterParaRelatorioHorasTrabalhadasViewModel(IEnumerable<Ponto> model)
        {
            //pega timezone do usuario configurado no sistema;
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var items = model
                .GroupBy(x => new
                {
                    x.ColaboradorId,
                    DataHora = x.DataHora.ConverterParaTimezone(timeZone).ToString("yyyy-MM-dd"),
                })
                .OrderBy(x => x.Key.DataHora)
                .Select(x => new RelatorioHorasTrabalhadasViewModel()
                {
                    NomeColaborador = x.First().NomeColaborador,
                    Dia = x.Key.DataHora,
                    HorasTrabalhadas = x.Sum(y => y.ObterHorasTrabalhadas())
                });

            return items;
        }
    }
}