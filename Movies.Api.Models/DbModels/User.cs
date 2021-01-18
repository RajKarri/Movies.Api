using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models.DbModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
