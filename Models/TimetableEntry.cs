using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DynamicTimetable
{
    public class TimetableEntry
    {
        [Key]
        public int EntryId { get; set; }
        [ForeignKey("Timetable")]
        public int TimetableId { get; set; }
        public Timetable Timetable { get; set; }
        [Required]
        public int DayNumber { get; set; }
        [Required]
        public int PeriodNumber { get; set; }
        [Required]
        public string SubjectName { get; set; }
    }
}
