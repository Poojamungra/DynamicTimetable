using System.ComponentModel.DataAnnotations;

namespace DynamicTimetable
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        [Required]
        public string ClassName { get; set; }
    }
}
