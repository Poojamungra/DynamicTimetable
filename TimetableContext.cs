

using System.Security.Claims;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace DynamicTimetable
{
    public class TimetableContext : DbContext
    {
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<SubjectHour> SubjectHours { get; set; }
        public DbSet<TimetableEntry> TimetableEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER07;Database=DynamicTimetable;Trusted_Connection=True;");
        }
    }

}
