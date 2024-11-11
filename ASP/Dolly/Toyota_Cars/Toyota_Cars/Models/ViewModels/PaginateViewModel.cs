using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationGeo.Models.ViewModels;

public class PaginateViewModel
{
    public List <string> Columns { get; set; } = new List<string>();
    public string SortColumn { get; set; } = "Name";
    public SelectList SortColumnSelectedList { get; set; }
    
    public string SortDirection { get; set; } = "asc";
    public SelectList SortDirectionSelectedList { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 2;
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public int TotalItems { get; set; } = 1;
    
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}