using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Neo.Extensions.Transaction
{
    internal class TransactionScopeProvider : ITransactionScopeProvider
    {
        private readonly DbContext _context;

        public TransactionScopeProvider(DbContext dbContext)
        {
            _context = dbContext;
        }

        public ITransactionScope CreateTransaction()
        {
            return new TransactionScope(_context);
        }
    }
}
