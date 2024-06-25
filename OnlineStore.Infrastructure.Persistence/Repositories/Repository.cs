using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.DomainModel.Repositories;
using OnlineStore.Infrastructure.Persistence.Context;

namespace OnlineStore.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class?
    {
        protected readonly DatabaseContext Context;

        public Repository(DatabaseContext context)
        {

            Context = context;
        }
        public async Task Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public  void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public async Task<T> Get(long id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

       
    }
}
