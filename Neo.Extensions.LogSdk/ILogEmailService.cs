using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Neo.Extensions.Persistence.Entities;

namespace Neo.Extensions.LogSdk
{
    public interface ILogEmailService
    {
        Task InserirAsync(LogEmail request);
    }
}
