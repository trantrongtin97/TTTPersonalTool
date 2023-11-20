namespace TTT.PersonalTool.Shared
{
    public sealed record LogMessage
    {
        public int Id { get; init; }
        public string? Level { get; init; }
        public string? Source { get; init; }
        public string? ExceptionMessage { get; init; }
        public string? StackTrace { get; init; }
        public DateTime? CreateDate { get; init; }
        public int? UserId { get; init; }
    }
}
