using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.lesson
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
            return Page();
        }

        [BindProperty]
        public Lesson Lesson { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Lesson.Add(Lesson);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
