using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.activity
{
    public class DetailsModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public DetailsModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        public Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Activity = await _context.Activity
                .Include(a => a.ActivityType).FirstOrDefaultAsync(m => m.ActivityId == id);

            if (Activity == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
