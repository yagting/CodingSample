using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodingExercise.Models
{
    public class ReportBatch
    {
        public int ID { get; set; }

        [DisplayName("Batch Run Date")]
        public DateTime RunDate { get; set; }

        [DisplayName("Total Document Read")]
        public long TotalDocumentRead { get; set; }

        [DisplayName("Total Developer Written")]
        public long TotalDeveloperWritten { get; set; }

        [DisplayName("Total Award Written")]
        public long TotalAwardWritten { get; set; }

        [DisplayName("Total Contrib Written")]
        public long TotalContribsWritten { get; set; }

        [Required( ErrorMessage="{0} is required")]
        [DisplayName("User Name")]
        [StringLength(50, ErrorMessage="{0} must be 1 to 50 characters in length")]
        public string  User{ get; set; }
    }
}