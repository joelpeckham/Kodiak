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
    public class IndexModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public IndexModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        public IList<ActivityType> ActivityType { get;set; }

        public async Task OnGetAsync()
        {
            ActivityType = await _context.ActivityType.ToListAsync();
        }
    }
}
