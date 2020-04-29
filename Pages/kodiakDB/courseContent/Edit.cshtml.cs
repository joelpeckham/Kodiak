using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.courseContent
{
    public class EditModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public EditModel(kodiak.Models.kodiakDBContext context)
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
           ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Name");
           ViewData["LessonId"] = new SelectList(_context.Lesson, "LessonId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CourseContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseContentExists(CourseContent.CourseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CourseContentExists(int id)
        {
            return _context.CourseContent.Any(e => e.CourseId == id);
        }
    }
}
