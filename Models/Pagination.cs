namespace BackendInterviewProblem.Models;

public class Pagination<T> where T : class
{
    public Pagination(
        int currentPage,
        int totalNumberOfPages,
        int totalNumberOfItems,
        IReadOnlyList<T> data)
    {
        CurrentPage = currentPage;
        TotalNumberOfPages = totalNumberOfPages;
        TotalNumberOfItems = totalNumberOfItems;
        Data = data;
    }

    public Pagination()
    {
        
    }

    public int CurrentPage { get; set; }
    public int TotalNumberOfPages { get; set; }
    public int TotalNumberOfItems { get; set; }
    public IReadOnlyList<T> Data { get; set; }
}
