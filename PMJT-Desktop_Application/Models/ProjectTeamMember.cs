using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMJT_Desktop_Application.Models
{
    public class ProjectTeamMember
    {
        public int ProjectId { get; set; }
        public int TeamMemberId { get; set; }
        public virtual Project Project { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}
