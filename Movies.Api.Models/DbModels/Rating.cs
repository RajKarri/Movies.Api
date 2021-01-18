using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Api.Models.DbModels
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }        
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required, Range(1, 5)]
        public int Stars { get; set; }
    }
}
