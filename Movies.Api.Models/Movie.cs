using System.Collections.Generic;

namespace Movies.Api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public string  runningTime { get; set; }
        public IList<string> Genres { get; set; }
     }
}
