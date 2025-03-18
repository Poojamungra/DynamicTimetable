using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DynamicTimetable
{
    public class Timetable
    {
        [Key]
        public int TimetableId { get; set; }
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; }
        [Required]
        public int WorkingDays { get; set; }
        [Required]
        public int SubjectsPerDay { get; set; }
        [Required]
        public int TotalSubjects { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
