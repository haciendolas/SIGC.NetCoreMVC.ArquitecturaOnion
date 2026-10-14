namespace SIGC.Presentation.AspNetCoreMVC.Models
{
    public class OptionResultModel<T>
    {
        public string Label { get; set; } = null!;
        public required T Value { get; set; }
    }

    public class OptionResultModel : OptionResultModel<int>
    {
    }
}
