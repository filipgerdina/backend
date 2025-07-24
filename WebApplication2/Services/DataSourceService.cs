using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services
{
    public class DataSourceService
    {
        private readonly AppDbContext _db;

        public DataSourceService(AppDbContext db)
        {
            _db = db;
        }

        public DataSourceClass GetDataSource(string name)
        {
            return _db.DataSources
                      .AsNoTracking()
                      .ToList().Find(ds => ds.Name.Equals(name));
        }
    }
}
