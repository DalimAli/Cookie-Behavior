using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cookie_Behavior.Models.IdentityInfos
{
    [Table("AspNetRoles")]
    public class ApplicationRole : IdentityRole<long>
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }

        [Required]
        public int StatusId { get; set; }
        public string Description { get; set; }

        [Required]
        public long CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateUtc { get; set; }

        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
    }
}
