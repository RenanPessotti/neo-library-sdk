using System;
namespace Neo.Extensions.Transaction
{
    public interface ITransactionScope : IDisposable
    {
        bool IsCommited { get; set; }
        void Commit();
        void Rollback();
    }
}
