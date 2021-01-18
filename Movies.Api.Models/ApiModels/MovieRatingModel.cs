namespace Movies.Api.Models.ApiModels
{
    public class MovieRatingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public string runningTime { get; set; }
        public string Genres { get; set; }
        public double averageRating { get; set; }
    }
}
