﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface IDevUoW : IUoW
    {
        void EnsureDeleted();
        void Migrate();
    }
}
