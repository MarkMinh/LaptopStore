using AutoMapper;
using LaptopStore.Models.DataModels;
using LaptopStore.Models.ViewModels;

namespace LaptopStore.Models.ProfileModels
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}
