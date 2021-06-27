using System.Threading.Tasks;
using Neo.Extensions.Persistence.Entities;

namespace Neo.Extensions.LogSdk
{
    public class LogEmailService : ILogEmailService
    {
        private readonly ILogEmailRepository _logSapRepository;
        public LogEmailService(ILogEmailRepository logRepository)
        {
            _logSapRepository = logRepository;
        }

        public async Task InserirAsync(LogEmail request)
        {
            await _logSapRepository.AddAsync(request);

        }
    }
}
