using AutoMapper;
using MVCStore.Application.ViewModels;
using MVCStore.Domain.Entities;

namespace MVCStore.Application.AutoMapper {
    public class AutoMapperConfig : Profile {
        public AutoMapperConfig() {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}