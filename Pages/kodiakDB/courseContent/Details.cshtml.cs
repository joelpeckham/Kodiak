using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.courseContent
{
    public class DetailsModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public DetailsModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        public CourseContent CourseContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseContent = await _context.CourseContent
                .Include(c => c.Course)
                .Include(c => c.Lesson).FirstOrDefaultAsync(m => m.CourseId == id);

            if (CourseContent == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
