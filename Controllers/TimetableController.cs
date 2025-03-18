using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicTimetable.Controllers
{
    public class TimetableController : Controller
    {
        public IActionResult CreateTimeTable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateTimetable(string[] subjectNames, int[] subjectHours, string className, int workingDays, int subjectsPerDay, int totalSubjects)
        {
            if (string.IsNullOrWhiteSpace(className) || workingDays < 1 || workingDays > 7 || subjectsPerDay < 1 || subjectsPerDay > 8 || totalSubjects < 1)
            {
                ModelState.AddModelError("InvalidInput", "Please enter valid inputs.");
                return View("CreateTimeTable");
            }

            int totalHoursForWeek = workingDays * subjectsPerDay;
            int totalEnteredHours = subjectHours.Sum();
            if (totalEnteredHours != totalHoursForWeek)
            {
                ModelState.AddModelError("InvalidHours", "Total subject hours must match total hours for the week.");
                return View("CreateTimeTable");
            }

            // Create subject pool according to total hours per subject
            List<string> subjectPool = new List<string>();
            for (int i = 0; i < subjectNames.Length; i++)
            {
                subjectPool.AddRange(Enumerable.Repeat(subjectNames[i], subjectHours[i]));
            }

            // Shuffle subjects for randomness
            Random rnd = new Random();
            subjectPool = subjectPool.OrderBy(x => rnd.Next()).ToList();

            // Generate Timetable
            string[,] timetable = new string[subjectsPerDay, workingDays];
            int index = 0;
            for (int i = 0; i < subjectsPerDay; i++)
            {
                for (int j = 0; j < workingDays; j++)
                {
                    timetable[i, j] = subjectPool[index++];
                }
            }

            ViewBag.ClassName = className;
            ViewBag.Timetable = timetable;
            ViewBag.WorkingDays = workingDays;
            ViewBag.SubjectsPerDay = subjectsPerDay;
            return View("ClassTimeTable");
        }
    }
}
