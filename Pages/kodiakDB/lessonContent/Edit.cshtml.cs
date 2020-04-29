using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.lessonContent
{
    public class EditModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public EditModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LessonContent LessonContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LessonContent = await _context.LessonContent
                .Include(l => l.Activity)
                .Include(l => l.Lesson).FirstOrDefaultAsync(m => m.LessonId == id);

            if (LessonContent == null)
            {
                return NotFound();
            }
           ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityId", "ActivityTypeId");
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

            _context.Attach(LessonContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonContentExists(LessonContent.LessonId))
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

        private bool LessonContentExists(int id)
        {
            return _context.LessonContent.Any(e => e.LessonId == id);
        }
    }
}
