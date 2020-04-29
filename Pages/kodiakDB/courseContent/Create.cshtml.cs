using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.courseContent
{
    public class CreateModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public CreateModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "Name");
        ViewData["LessonId"] = new SelectList(_context.Lesson, "LessonId", "Name");
            return Page();
        }

        [BindProperty]
        public CourseContent CourseContent { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CourseContent.Add(CourseContent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
