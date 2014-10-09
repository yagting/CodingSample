using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Models
{
    public class Developer : IData
    {
        private List<Award> _Awards = new List<Award>();
        private List<Contrib> _Contribs = new List<Contrib>();

        public int DeveloperID { get; set; }

        public string RootID { get; set; }

        public Name DevName { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "{0} must be between {2} to {1} characters in length")]
        [Display(Name = "Aka")]
        public string AKA { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be between 0 to 50 characters in length")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public Nullable<DateTime> Birth { get; set; }

        public Nullable<DateTime> Death { get; set; }

        [NotMapped()]
        public List<Award> Awards { get { return _Awards; } set { _Awards = value; } }

        [NotMapped()]
        public List<Contrib> Contribs { get { return _Contribs; } set { _Contribs = value; } }
    }
}