using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Inta.ERP.Authorization.Models
{
    public class User: IdentityUser<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public bool Active { get; set; }
        public bool IsApiUser { get; set; }
        public decimal MaximumApproveAmount { get; set; }
        public decimal MaximumPettyCashApproveAmount { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsForcedLogoutPending { get; set; }
        public int Status { get; set; }
        public int CreatedUser { get; set; }
        public int LastModifiedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        // public virtual ICollection<ApplicationRole> Roles { get; } = new List<ApplicationRole>();
    }
}
