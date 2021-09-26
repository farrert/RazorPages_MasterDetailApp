using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrisbeeGolfCourseMap.Data;
using FrisbeeGolfCourseMap.Models;

namespace FrisbeeGolfCourseMap.Pages.Robin
{
    public class IndexModel : PageModel
    {
        private readonly FrisbeeGolfCourseMap.Data.FrisbeeGolfCourseMapContext _context;

        public IndexModel(FrisbeeGolfCourseMap.Data.FrisbeeGolfCourseMapContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } 
        public SelectList Times { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CourseTime { get; set; }
        public string TimeSort { get; set; }


        public async Task OnGetAsync(string sortOrder)
        {
            TimeSort = sortOrder;
            // Use LINQ to get list of genres.
            //IQueryable<int> TimetoCompleteQuery = from c in _context.Course
                                            //orderby c.TimeToComplete
                                           // select c.TimeToComplete;
            var courses = from c in _context.Course
                          select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                courses = courses.Where(s => s.Name.Contains(SearchString));
            }

            if (TimeSort == "desc")
            {
                courses = courses.OrderByDescending(c => c.TimeToComplete);
            }
            else
            {
                courses = courses.OrderBy(c => c.TimeToComplete);
            }

           // if (!int.IsNullorEmpty(CourseTime))
           // {
               // courses = courses.Where(x => x.TimeToComplete == CourseTime);
            //}
             Course = await courses.ToListAsync();
            // Times = new SelectList(await TimetoCompleteQuery.Distinct().ToListAsync());
        }
    }
}
