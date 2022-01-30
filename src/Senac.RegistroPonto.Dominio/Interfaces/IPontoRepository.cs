using Senac.RegistroPonto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.Domain.Interfaces
{
    public interface IPontoRepository : IBaseRepository<Ponto>
    {
        Task<Ponto> ObterUltimoPontoAsync(Guid colaboradorId);
        Task<IEnumerable<Ponto>> ObterRelatorioHorasTrabalhadasAsync(Guid colaboradorId);
    }
}
