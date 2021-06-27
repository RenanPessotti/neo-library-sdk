using System;
using System.Collections.Generic;
using System.Text;
using Neo.Extensions.Persistence.Entities;

namespace Neo.Extensions.LogSdk
{
    public interface ILogEmailRepository : Persistence.Repositories.Interface.IBaseRepository<LogEmail>
    {
    }
}
