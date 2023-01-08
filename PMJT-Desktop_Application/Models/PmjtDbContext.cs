using DevExpress.DataProcessing.InMemoryDataProcessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMJT_Desktop_Application.Models
{
    public class PmjtDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use the connection string from the app config file
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PjmtDbContext"].ConnectionString);
        }



    }
}
