using Microsoft.EntityFrameworkCore;
using OnlineShopStore.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Infrastructure.Persistence.Exceptions;

namespace OnlineShopStore.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            try
            {
                return _context.SaveChangesAsync(cancellationToken);

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new EFConcurrencyException();
            }

        }
    }
}
