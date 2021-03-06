using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kodiak.Models;

namespace kodiak.Pages.kodiakDB.lesson
{
    public class IndexModel : PageModel
    {
        private readonly kodiak.Models.kodiakDBContext _context;

        public IndexModel(kodiak.Models.kodiakDBContext context)
        {
            _context = context;
        }

        public IList<Lesson> Lesson { get;set; }

        public async Task OnGetAsync()
        {
            Lesson = await _context.Lesson.ToListAsync();
        }
    }
}
