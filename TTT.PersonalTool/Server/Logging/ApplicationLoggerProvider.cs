using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TTT.PersonalTool.Server.DbContexts;

namespace TTT.PersonalTool.Server.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly IDbContextFactory<DbLoggingContext> _contextFactory;

        public ApplicationLoggerProvider(IDbContextFactory<DbLoggingContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_contextFactory);
        }

        public void Dispose()
        {

        }
    }
}
