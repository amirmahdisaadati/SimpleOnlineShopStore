using OnlineStore.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Persistence.UnitOfWork
{
    public  class UnitOfWork: IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
