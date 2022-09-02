using Lms.Core.Entities;
using Lms.Core.Repositories;
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
        public void Add(Module course)
        {
            throw new NotImplementedException();
        }
    }
}
