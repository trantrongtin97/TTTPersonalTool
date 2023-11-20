using Microsoft.EntityFrameworkCore;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Server.DbContexts;

namespace TTT.PersonalTool.Server.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly IDbContextFactory<DbLoggingContext> _contextFactory;

        public DatabaseLogger(IDbContextFactory<DbLoggingContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            int userId = 0;

            Log log = new();
            log.Level = logLevel.ToString();
            log.UserId = Convert.ToInt32(userId);
            log.ExceptionMessage = exception?.Message;
            log.StackTrace = exception?.StackTrace;
            log.Source = "Server";
            log.CreateDate = DateTime.Now;

            using (var context = _contextFactory.CreateDbContext())
            {
                context.Logs.Add(log);
                context.SaveChanges();
            }

        }
    }
}
