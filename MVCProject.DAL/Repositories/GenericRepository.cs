using Microsoft.EntityFrameworkCore;
using MVCProject.DAL.Data.Context;
using MVCProject.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Repositories
{
    public class GenericRepository<TEntity>(ApplicationDbContext dbContext):IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return dbContext.Set<TEntity>().ToList();
            }
            else
            {
                return dbContext.Set<TEntity>().AsNoTracking().ToList();
            }
        }
        //get by id
        public TEntity? GetById(int id)
        => dbContext.Set<TEntity>().Find(id);

        //add
        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
           
        }

        //update
        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
          
        }

        //Delete or Remove
        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
           
        }
    }
}
