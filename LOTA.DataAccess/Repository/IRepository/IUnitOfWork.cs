﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        /*ICategoryRepository categoryRepository { get; }*/

        void Save();
    }
}