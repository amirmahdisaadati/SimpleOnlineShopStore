using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.DomainModel.Repositories
{
    public interface IRepository<T> where T : class?
    {
        Task<T> Get(long id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
    }
}
