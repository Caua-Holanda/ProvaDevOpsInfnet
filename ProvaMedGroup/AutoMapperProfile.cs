using AutoMapper;
using ProvaMedGroup.DomainModel.Entities;
using ProvaMedGroup.ViewModel;
using ProvaMedGroup.ViewModels;

namespace ProvaMedGroup
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapDomainToViewModel();

        }

        public void MapDomainToViewModel()
        {
            CreateMap<Contato, ContatoIdViewmodel>().ReverseMap();
            CreateMap<Contato, ContatoViewModel>().ReverseMap();
        }


    }
}
