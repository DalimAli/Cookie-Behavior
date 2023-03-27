using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cookie_Behavior.Models.IdentityInfos
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser<Int64>
    {

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(512)]
        public string Address1 { get; set; }

        [MaxLength(512)]
        public string Address2 { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public string FullName => string.Concat(FirstName + " " + MiddleName + " " + LastName).Replace("  ", " ");


    }
}
