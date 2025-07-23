using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class ModuleService
    {
        private readonly AppDbContext _db;

        public ModuleService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ModuleClass> GetModules()
        {
            return _db.Modules
                      .AsNoTracking()
                      .ToList();
        }

        public ModuleClass? GetModuleById(int id)
        {
            return _db.Modules
                      .AsNoTracking()
                      .FirstOrDefault(m => m.Id == id);
        }
    }
}
