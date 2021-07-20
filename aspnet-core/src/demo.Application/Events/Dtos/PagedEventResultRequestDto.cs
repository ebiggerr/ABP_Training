using Abp.Application.Services.Dto;

namespace demo.Events.Dtos
{
    public class PagedEventResultRequestDto: PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public string Keyword { get; set; }
        public bool? IsCancelled { get; set; }
        public string Sorting { get; set; }
    }
}