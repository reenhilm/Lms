using Lms.Core.Entities;
using Lms.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly LmsApiContext db;
        public ModuleRepository(LmsApiContext db)
        {
            this.db = db;
        }

        public void Add(Module module)
        {
            db.AddAsync(module);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db.Module.AnyAsync(c => c.Id == id);
        }

        public async Task<Module> FindAsync(int? id)
        {
            return await db.Module.FindAsync(id);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module> GetModule(int? id)
        {
            return await db.Module.FirstAsync(c => c.Id == id);
        }

        public void Remove(Module module)
        {
            db.Module.Remove(module);
        }

        public void Update(Module module)
        {
            db.Entry(module).State = EntityState.Modified;
        }
    }
}
