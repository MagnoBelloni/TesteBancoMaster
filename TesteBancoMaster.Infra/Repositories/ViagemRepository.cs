using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TesteBancoMaster.Infra.Data;
using TesteBancoMaster.Infra.Entities;

namespace TesteBancoMaster.Infra.Repositories
{
    public class ViagemRepository : IViagemRepository
    {
        private readonly ViagensContext _context;

        public ViagemRepository(ViagensContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Viagem> ObterViagem(Expression<Func<Viagem, bool>> query)
        {
            return await _context.Viagens.FirstOrDefaultAsync(query);
        }

        public async Task<List<Viagem>> ObterViagens(Expression<Func<Viagem, bool>> query)
        {
            return await _context.Viagens.Where(query).ToListAsync();
        }

        public async Task<List<Viagem>> ObterTodos()
        {
            return await _context.Viagens.AsNoTracking().ToListAsync();
        }

        public async Task CadastrarViagem(Viagem request)
        {
            await _context.Viagens.AddAsync(request);
        }

        public void AtualizarViagem(Viagem request)
        {
            _context.Viagens.Update(request);
        }

        public bool DeletarViagem(Viagem request)
        {
            try
            {
                _context.Viagens.Remove(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
