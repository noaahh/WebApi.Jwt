using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Jwt.Core.Interfaces.Gateways.Repositories;
using WebApi.Jwt.Core.Shared;

namespace WebApi.Jwt.Infrastructure.Data.Repositories
{
    public abstract class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext AppDbContext;

        protected EfRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await AppDbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListAll()
        {
            return await AppDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetSingleBySpec(ISpecification<T> spec)
        {
            var result = await List(spec);
            return result.FirstOrDefault();
        }

        public async Task<List<T>> List(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(AppDbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                .Where(spec.Criteria)
                .ToListAsync();
        }


        public async Task<T> Add(T entity)
        {
            AppDbContext.Set<T>().Add(entity);
            await AppDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            AppDbContext.Entry(entity).State = EntityState.Modified;
            await AppDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            AppDbContext.Set<T>().Remove(entity);
            await AppDbContext.SaveChangesAsync();
        }
    }
}