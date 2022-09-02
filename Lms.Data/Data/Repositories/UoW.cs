using Lms.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Repositories
{
    public class UoW : IUoW
    {
        public ICourseRepository CourseRepository { get; }

        public IModuleRepository ModuleRepository { get; }

        private readonly LmsApiContext db;

        public UoW(LmsApiContext db)
        {
            this.db = db;
            CourseRepository = new CourseRepository(db);
            ModuleRepository = new ModuleRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }

        public void EnsureDeleted()
        {
            db.Database.EnsureDeleted();
        }
        public void Migrate()
        {
            db.Database.Migrate();
        }
        
    }
}
