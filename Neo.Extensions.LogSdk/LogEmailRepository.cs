using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo.Extensions.Persistence.Context;
using Neo.Extensions.Persistence.Entities;
using Neo.Extensions.Persistence.Repositories.Base;

namespace Neo.Extensions.LogSdk
{
    public class LogEmailRepository : BaseRepository<LogEmail>, ILogEmailRepository
    {
        public LogEmailRepository(SDKDbContext context) : base(context) { }

    }
}
