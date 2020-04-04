using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G09projectenII.Views.testing
{
    public class IndexModel : PageModel
    {
        private readonly G09projectenII.Models.AppDbContext _context;

        public IndexModel(G09projectenII.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Session> Sessions { get; set; }

        public async Task OnGetAsync()
        {
            Sessions = await _context.Sessions.ToListAsync();
        }
    }
}
