using System;
using System.Linq;
using System.Threading.Tasks;
using MVCStore.Domain.Entities;
using MVCStore.Domain.Interfaces;
using MVCStore.Domain.Validations;

namespace MVCStore.Domain.Services {
    public class ProductService : BaseService, IProductService {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotificator notificator) : base(notificator) {
            _productRepository = productRepository;
        }
        public async Task Add(Product product) {
            if (!Validate(new ProductValidation(), product)) return;
            await _productRepository.Add(product);
        }

        public async Task Remove(Guid id) {
            await _productRepository.Remove(id);
        }

        public async Task Update(Product product) {
            if (!Validate(new ProductValidation(), product)) return;
            await _productRepository.Update(product);
        }
    }
}