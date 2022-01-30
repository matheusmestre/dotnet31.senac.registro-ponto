using Microsoft.EntityFrameworkCore;
using Senac.RegistroPonto.Domain.Entidades;
using Senac.RegistroPonto.Domain.Enums;
using Senac.RegistroPonto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senac.RegistroPonto.Infra.Data.Repositories
{
    public class PontoRepository : BaseRepository<Ponto>, IPontoRepository
    {
        public PontoRepository(ApplicationContext context) 
            : base(context)
        {
        }
        public async Task<IEnumerable<Ponto>> ObterRelatorioHorasTrabalhadasAsync(Guid colaboradorId)
        {
            var model = await _dbSet
                .Where(x => x.ColaboradorId == colaboradorId)
                .Where(x => x.Tipo == ETipoPonto.ENTRADA)
                .Where(x => x.RelacionadoId != default)
                .Include(x => x.Relacionado)
                .OrderBy(x => x.DataHora)
                .ToListAsync();

            return model;
        }
        public async Task<Ponto> ObterUltimoPontoAsync(Guid colaboradorId)
        {
            var model = await _dbSet
                .Where(x => x.ColaboradorId == colaboradorId)
                .OrderByDescending(x => x.DataHora)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return model;
        }
    }
}
