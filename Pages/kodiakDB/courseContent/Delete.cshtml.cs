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
    public class DeleteModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public DeleteModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseContent = await _context.CourseContent.FindAsync(id);

            if (CourseContent != null)
            {
                _context.CourseContent.Remove(CourseContent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
