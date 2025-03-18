using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DynamicTimetable
{
    public class SubjectHour
    {
        [Key]
        public int SubjectHoursId { get; set; }
        [ForeignKey("Timetable")]
        public int TimetableId { get; set; }
        public Timetable Timetable { get; set; }
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public int TotalHours { get; set; }
    }
}
