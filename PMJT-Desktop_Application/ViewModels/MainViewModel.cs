using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using Microsoft.EntityFrameworkCore;
using PMJT_Desktop_Application.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace PMJT_Desktop_Application.ViewModels
{
    [POCOViewModel]
    public class MainViewModel
    {
        #region CTOR
        public MainViewModel(PmjtDbContext context)
        {
            _context = context;
            Projects = new ObservableCollection<Project>();
        }
        #endregion

        #region Properties
        public ObservableCollection<Project> Projects { get; set; }

        private readonly PmjtDbContext _context;

        #endregion

        #region Methods
        public async Task LoadProjectsAsync()
        {
            // Query the database using Entity Framework
            var projects = await _context.Projects.ToListAsync();

            // Convert the result to an ObservableCollection
            Projects = projects.ToObservableCollection();
        }
        #endregion

        #region Commands

        public void Initialized()
        {
            //  RepresenativeSID = Configuration.GetValue<string>("RepresentativeSID");
            //HostUrl = Configuration.GetValue<string>("HostUrl");
            Console.WriteLine("Initialized");

        }


        public void Loaded()
        {
            Console.WriteLine("Loaded");
        }
        #endregion
    }
}
