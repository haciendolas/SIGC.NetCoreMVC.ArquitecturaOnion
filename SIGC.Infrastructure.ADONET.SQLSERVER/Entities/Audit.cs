namespace SIGC.Infrastructure.CrossCutting.Dtos
{
    public class Audit
    {
        public string OperationType { get; set; } // Create, Update, Delete

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }

        public string? AffectedColumns { get; set; }
    }
}