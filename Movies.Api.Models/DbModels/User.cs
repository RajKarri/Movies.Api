using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models.DbModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
