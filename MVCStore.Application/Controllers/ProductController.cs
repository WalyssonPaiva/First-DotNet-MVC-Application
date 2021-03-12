using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStore.Application.Extensions;
using MVCStore.Application.ViewModels;
using MVCStore.Domain.Entities;
using MVCStore.Domain.Interfaces;

namespace MVCStore.Application.Controllers {
    [Authorize]
    public class ProductController : BaseController {
        
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, 
            IMapper mapper, IProductService productService, INotificator notificator) : base(notificator) {
            _productRepository = productRepository;
            _mapper = mapper;
            _productService = productService;
        }

        [AllowAnonymous]
        [Route("/")]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index() {
            return View(_mapper.Map<ICollection<ProductViewModel>>(await _productRepository.GetAll()) );
        }
        
        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        public IActionResult Create() {
            return View();
        }
        
        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel) {
            if (!ModelState.IsValid) return View(productViewModel);

            var imgPreName = Guid.NewGuid() + "_";
            if (!await UploadFile(productViewModel.ImageUpload, imgPreName)) {
                return View(productViewModel);
            }

            productViewModel.Image = imgPreName + productViewModel.ImageUpload.FileName;
            
            var product = _mapper.Map<Product>(productViewModel);
            await _productService.Add(product);
            
            if(!ValidOperation()) return View(productViewModel);
            
            return RedirectToAction("Index");
        }
        
        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id) {

            var productViewModel = _mapper
                .Map<ProductViewModel>(await _productRepository.GetById(id));
            
            if (productViewModel == null)
                return NotFound();

            return View(productViewModel);
        }
        
        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel) {
            if (productViewModel.Id != id)  return NotFound();

            var productUpdate = _mapper
                .Map<ProductViewModel>(await _productRepository.GetById(id));
            productViewModel.Image = productUpdate.Image;

            if (!ModelState.IsValid) return View(productViewModel);

            if (productViewModel.ImageUpload != null) {
                var imgPreName = Guid.NewGuid() + "_";
                if (!await UploadFile(productViewModel.ImageUpload, imgPreName)) {
                    return View(productViewModel);
                }
                productUpdate.Image = imgPreName + productViewModel.ImageUpload.FileName;
            }

            productUpdate.Name = productViewModel.Name;
            productUpdate.Description = productViewModel.Description;
            productUpdate.Price = productViewModel.Price;
            
            var product = _mapper.Map<Product>(productUpdate);
            await _productService.Update(product);
            if(!ValidOperation()) return View(_mapper
                .Map<ProductViewModel>(await _productRepository.GetById(id)));
            return RedirectToAction("Index");
        }
        
        [ClaimsAuthorize("Produto", "Deletar")]
        [Route("deletar-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = _mapper
                .Map<ProductViewModel>(await _productRepository.GetById(id));

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }
        
        [ClaimsAuthorize("Produto", "Deletar")]
        [Route("deletar-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = _mapper
                .Map<ProductViewModel>(await _productRepository.GetById(id));

            if (product == null) return NotFound();

            await _productRepository.Remove(id);
            if(!ValidOperation()) return View(product);
            TempData["Sucesso"] = "Produto excluido com sucesso";
            return RedirectToAction("Index");
        }

        private async Task<bool> UploadFile(IFormFile file, string name) {
            if (file.Length <= 0) return false;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", name + file.FileName);

            if (System.IO.File.Exists(path)) {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
            }

            await using (var stream = new FileStream(path, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}