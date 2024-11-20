namespace WebApplicationGeo.Models.ViewModels;

public class ApiPaginateResponse<TypeName>
{
    public IEnumerable<TypeName> Data { get; set; }
    public PaginateViewModel Paginate { get; set; }
}