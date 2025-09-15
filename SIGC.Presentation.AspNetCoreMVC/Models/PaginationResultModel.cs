namespace SIGC.Presentation.AspNetCoreMVC.Models;

public sealed class PaginationResultModel<T> where T : class
{
    public int Count { get; set; }
    public List<T> Items { get; set; } = new List<T>();
}