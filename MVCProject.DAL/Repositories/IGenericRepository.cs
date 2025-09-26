﻿using MVCProject.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity:BaseEntity
    {
        int Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        TEntity GetById(int id);
        int Remove(TEntity entity);
        int Update(TEntity entity);
    }
}
