using Microsoft.EntityFrameworkCore;
using ProvaMedGroup.DomainModel;
using ProvaMedGroup.DomainModel.Interfaces.Repositories;
using ProvaMedGroup.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaMedGroup.Infra.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        protected readonly ProvaMedGroupDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ProvaMedGroupDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity> Read(Guid id)
        {
            return await DbSet.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> ReadAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual void Create(TEntity entity)
        {
                 DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
                DbSet.Update(entity);
        }

        public virtual void Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
