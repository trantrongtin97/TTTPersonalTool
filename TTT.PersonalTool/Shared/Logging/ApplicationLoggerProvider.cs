﻿using Microsoft.Extensions.Logging;

namespace TTT.PersonalTool.Shared.Logging
{
    public sealed class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly LogWriter _logWriter;

        public ApplicationLoggerProvider(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_logWriter);
        }

        public void Dispose()
        {
        }
    }
}
