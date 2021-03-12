using System;
using System.Threading.Tasks;
using MVCStore.Domain.Entities;

namespace MVCStore.Domain.Interfaces {
    public interface IProductService {
        
        Task Add(Product product);
        
        Task Remove(Guid id);

        Task Update(Product product);
        
    }
}