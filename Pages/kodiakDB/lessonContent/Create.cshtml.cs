using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.lessonContent
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
        ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityId", "ActivityTypeId");
        ViewData["LessonId"] = new SelectList(_context.Lesson, "LessonId", "Name");
            return Page();
        }

        [BindProperty]
        public LessonContent LessonContent { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LessonContent.Add(LessonContent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
