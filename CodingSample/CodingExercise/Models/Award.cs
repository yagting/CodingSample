using System;
using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Models
{
    public class Award : IData
    {
        public int AwardID { get; set; }

        public string RootID { get; set; }

        public Name DevName { get; set; }

        public Nullable<DateTime> Birth { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "{0} must be between {2} to {1} characters in length")]
        [Display(Name = "Award Name")]
        public string AwardName { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "{0} must be between {2} to {1} characters in length")]
        [Display(Name = "Award By")]
        public string AwardBy { get; set; }
        
        [Display(Name = "Award Year")]
        public Nullable<int> AwardYear { get; set; }
    }
}
