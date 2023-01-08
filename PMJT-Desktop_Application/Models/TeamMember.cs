using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMJT_Desktop_Application.Models
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
