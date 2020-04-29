using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.activityType
{
    public class DetailsModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public DetailsModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

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
    }
}
