using Microsoft.EntityFrameworkCore;
using TTT.PersonalTool.Shared.Models;
using TTT.PersonalTool.Server.DbContexts;
using System.Security.Claims;

namespace TTT.PersonalTool.Server.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly IDbContextFactory<DbLoggingContext> _contextFactory;
        public IHttpContextAccessor _httpContextAccessor { get; }

        public DatabaseLogger(IDbContextFactory<DbLoggingContext> contextFactory, IHttpContextAccessor httpContextAccessor)
        {
            _contextFactory = contextFactory;
            _httpContextAccessor = httpContextAccessor;
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
            var user = _httpContextAccessor?.HttpContext?.User;
            int userid = 0;
            if (user.Identity.IsAuthenticated)
            {
                int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier).Value, out int uid);
                userid = uid;
            }

            Log log = new();
            log.Level = logLevel.ToString();
            log.EventName = eventId.Name;
            log.UserId = userid;
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
