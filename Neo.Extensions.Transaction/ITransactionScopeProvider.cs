using System;
namespace Neo.Extensions.Transaction
{
    public interface ITransactionScopeProvider
    {
        ITransactionScope CreateTransaction();
    }
}
