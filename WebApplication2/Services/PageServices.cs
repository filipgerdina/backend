using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class PagesService
    {
        private readonly AppDbContext _db;

        public PagesService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<PageClass> GetPages()
        {
            return _db.Pages
                .Include(p => p.Group)
                .AsNoTracking()
                .ToList();
        }

        public PageClass? GetPageById(int id)
        {
            return _db.Pages
                .Include(p => p.Group) // Use navigation property
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
