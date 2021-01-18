using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models.DbModels
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public string runningTime { get; set; }
        public string Genres { get; set; }
    }
}
