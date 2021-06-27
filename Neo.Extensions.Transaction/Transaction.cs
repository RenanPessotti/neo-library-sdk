using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Neo.Extensions.Transaction
{
    internal class TransactionScope : ITransactionScope
    {
        private readonly IDbContextTransaction _transaction;

        public TransactionScope(DbContext dbContext)
        {
            _transaction = dbContext.Database.BeginTransaction();
            IsCommited = false;
        }

        public bool IsCommited { get; set; }

        public void Commit()
        {
            _transaction.Commit();
            IsCommited = true;
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Ocorreu um erro ao tentar da um rollback na transação: {ex.Message}");
            }
        }        
    }
}
