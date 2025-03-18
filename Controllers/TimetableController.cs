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

            // Generate Timetable ensuring subjects are properly distributed based on hours
            string[,] timetable = new string[subjectsPerDay, workingDays];
            int index = 0;

            for (int j = 0; j < workingDays; j++)
            {
                HashSet<string> usedSubjects = new HashSet<string>();
                for (int i = 0; i < subjectsPerDay; i++)
                {
                    // Ensure subjects are distributed according to their weekly hours
                    string subject = subjectPool.FirstOrDefault(s => !usedSubjects.Contains(s)) ?? subjectPool.First();
                    timetable[i, j] = subject;
                    usedSubjects.Add(subject);
                    subjectPool.Remove(subject);
                }

                // Refill the pool if subjects run out to maintain distribution
                if (subjectPool.Count < subjectsPerDay)
                {
                    for (int i = 0; i < subjectNames.Length; i++)
                    {
                        int remainingHours = subjectHours[i] - timetable.Cast<string>().Count(s => s == subjectNames[i]);
                        subjectPool.AddRange(Enumerable.Repeat(subjectNames[i], remainingHours));
                    }
                    subjectPool = subjectPool.OrderBy(x => rnd.Next()).ToList();
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
