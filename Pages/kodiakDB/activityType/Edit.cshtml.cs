using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.activityType
{
    public class EditModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public EditModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ActivityType ActivityType { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ActivityType = await _context.ActivityType.FirstOrDefaultAsync(m => m.ActivityTypeId == id);

            if (ActivityType == null)
            {
                return NotFound();
            }
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

            _context.Attach(ActivityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityTypeExists(ActivityType.ActivityTypeId))
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

        private bool ActivityTypeExists(string id)
        {
            return _context.ActivityType.Any(e => e.ActivityTypeId == id);
        }
    }
}
