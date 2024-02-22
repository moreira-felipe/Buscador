namespace Buscador.Domain.Dtos.Requests
{
    public class PagenedSiteSearchDto
    {
        public string Title { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
    }
}
