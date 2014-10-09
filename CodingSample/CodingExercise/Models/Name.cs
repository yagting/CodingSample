using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Models
{
    [BsonIgnoreExtraElements(true)]
    public class Name
    {
        [StringLength(50, MinimumLength=1, ErrorMessage = "{0} must be between {2} to {1} characters in length")]
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "First Name")]
        [Column("FirstName")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} must be between {2} to {1} characters in length")]
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Last Name")]
        [Column("LastName")]
        public string LastName { get; set; }
    }
}