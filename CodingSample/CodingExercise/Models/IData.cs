using System;
using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Models
{
    interface IData
    {
        [StringLength(38, ErrorMessage = "{0} must be between 1 to 38 characters in length")]
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Root ID")]
        string RootID { get; set; }

        Nullable<DateTime> Birth { get; set; }

        Name DevName { get; set; }
    }
}
