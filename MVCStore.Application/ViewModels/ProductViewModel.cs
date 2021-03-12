using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVCStore.Application.ViewModels {
    public class ProductViewModel {
        
        [Key]
        public Guid Id { get; set; }
        
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0}  é obrigatório")]
        [MinLength(2, ErrorMessage = "O campo precisa ter no mínimo {0} caracteres")]
        public string Name { get; set; }
        
        [DisplayName("Preço")]
        [Required(ErrorMessage = "O campo {0}  é obrigatório")]
        public decimal Price { get; set; }
        
        [DisplayName("Imagem do Produto")]
        public IFormFile ImageUpload { get; set; }
        
        public string Image { get; set; }
        
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0}  é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Description { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }
    }
}