using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCStore.Domain.Entities;

namespace MVCStore.Domain.Interfaces {
    public interface IRepository<T> where T : BaseEntity{
        
        Task Add(T entity);
        
        Task Remove(Guid id);
        
        Task<T> GetById(Guid id);
        
        Task Update(T entity);
        
        Task<List<T>> GetAll();
        
        Task<int> SaveChanges();
    }
}