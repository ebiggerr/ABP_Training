namespace demo.TaskAppService.Dto
{
    public class RequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public RequestDto(string title, string description)
        {
            Title = title;
            Description = description;
        }



    }
}