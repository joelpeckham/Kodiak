using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.lessonContent
{
    public class DetailsModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public DetailsModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
