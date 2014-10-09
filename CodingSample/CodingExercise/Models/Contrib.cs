using System;
using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Models
{
    public class Contrib : IData
    {
        public int ContribID { get; set; }

        public string RootID { get; set; }

        public Name DevName { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "{0} must be between {2} to {1} characters in length")]
        [Display(Name = "Contrib Name")]
        public string Name { get; set; }

        public Nullable<DateTime> Birth { get; set; }
    }
}