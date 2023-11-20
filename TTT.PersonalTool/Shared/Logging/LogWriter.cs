namespace TTT.PersonalTool.Shared.Logging
{
    public class LogWriter
    {
        private readonly LogQueue _queue;

        public LogWriter(LogQueue queue)
        {
            _queue = queue;
        }

        public ValueTask Write(LogMessage message, CancellationToken token = default) =>
            _queue.WriteLog(message, token);
    }
}
