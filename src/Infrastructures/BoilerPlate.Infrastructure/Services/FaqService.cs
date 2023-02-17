using BoilerPlate.Core.Abstractions;
using BoilerPlate.Core.Models;
using BoilerPlate.Domain.Entities;
using BoilerPlate.Domain.Entities.Faq;
using BoilerPlate.Shared.Abstraction.Databases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Infrastructure.Services
{
    internal class FaqService : IFaqService
    {
        private readonly IDbContext _dbContext;
        public FaqService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<FaqHead> GetBaseFaqHeadQuery()
            => _dbContext.Set<FaqHead>()
            .Include(p => p.FaqContent)
            .Where(p => p.DeletedByDt == null)
            .AsQueryable();
        public IQueryable<FaqContent> GetBaseFaqContentQuery()
            => _dbContext.Set<FaqContent>().AsQueryable();

        public Task<FaqHead?> GetFaqHeadById(Guid FqHeadId, CancellationToken cancellation)
            => GetBaseFaqHeadQuery().Where(p => p.FqHeadId == FqHeadId && p.DeletedByDt == null).FirstOrDefaultAsync(cancellation);

        public Task<FaqContent?> GetFaqContentById(Guid FqActiveContentId, CancellationToken cancellation)
            => GetBaseFaqContentQuery().Where(p => p.FqContentId == FqActiveContentId && p.DeletedByDt == null).FirstOrDefaultAsync(cancellation);

        public Task<List<FaqHead>> GetFaqHeadList(CancellationToken cancellation)
            => GetBaseFaqHeadQuery().ToListAsync(cancellation);
    }
}
