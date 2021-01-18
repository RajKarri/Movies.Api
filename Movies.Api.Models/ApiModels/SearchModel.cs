namespace Movies.Api.Models.ApiModels
{
    public class SearchModel
    {
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public string[] Genres { get; set; }
    }
}
