using DynamicTimetable;

namespace DynamicTimetable
{
    public class TimetableService
    {
        private readonly TimetableContext _context;

        public TimetableService(TimetableContext context)
        {
            _context = context;
        }

        public void SaveTimetable(Timetable timetable, List<SubjectHour> subjectHours, List<TimetableEntry> entries)
        {
            _context.Timetables.Add(timetable);
            _context.SubjectHours.AddRange(subjectHours);
            _context.TimetableEntries.AddRange(entries);
            _context.SaveChanges();
        }

        public List<Timetable> GetTimetablesByClass(int classId)
        {
            return _context.Timetables.Where(t => t.ClassId == classId).ToList();
        }
    }

}
