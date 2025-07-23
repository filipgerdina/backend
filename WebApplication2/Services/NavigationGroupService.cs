using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class NavigationGroupService
    {
        private readonly AppDbContext _db;

        public NavigationGroupService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<NavigationGroupClass> GetNavigationGroups()
        {
            return _db.NavigationGroups
                      .AsNoTracking()
                      .ToList();
        }

        public NavigationGroupClass? GetNavigationGroupById(int id)
        {
            return _db.NavigationGroups
                      .AsNoTracking()
                      .FirstOrDefault(g => g.Id == id);
        }
    }
}