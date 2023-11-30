using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TTT.PersonalTool.Server.DbContexts;

namespace TTT.PersonalTool.Server.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly IDbContextFactory<DbLoggingContext> _contextFactory;
        public IHttpContextAccessor _httpContextAccessor { get; }

        public ApplicationLoggerProvider(IDbContextFactory<DbLoggingContext> contextFactory, IHttpContextAccessor httpContextAccessor)
        {
            _contextFactory = contextFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_contextFactory, _httpContextAccessor);
        }

        public void Dispose()
        {

        }
    }
}
