﻿namespace Movies.Api.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Stars { get; set; }
    }
}
