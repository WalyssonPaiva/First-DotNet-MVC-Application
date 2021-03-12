using System.Collections.Generic;
using System.Threading.Tasks;
using MVCStore.Domain.Entities;
using MVCStore.Domain.Interfaces;
using MVCStore.Infra.Data.Context;

namespace MVCStore.Infra.Data.Repository {
    public class ProductRepository : Repository<Product>, IProductRepository {
        
        public ProductRepository(PgDbContext db) : base(db) {
        }
    }
}