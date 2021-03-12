using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCStore.Domain.Entities;
using MVCStore.Domain.Interfaces;
using MVCStore.Infra.Data.Context;

namespace MVCStore.Infra.Data.Repository {
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity, new(){
        
        protected readonly PgDbContext Db;
        protected readonly DbSet<T> DbSet;

        public Repository(PgDbContext db) {
            Db = db;
            DbSet = db.Set<T>();
        }

        public async Task Add(T entity) {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Remove(Guid id) {
            var entity = new T {Id = id};
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<T> GetById(Guid id) {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);;
        }

        public async Task Update(T entity) {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<List<T>> GetAll() {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveChanges() {
            return await Db.SaveChangesAsync();
        }
    }
}