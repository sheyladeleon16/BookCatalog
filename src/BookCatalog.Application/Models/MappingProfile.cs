using AutoMapper;
using BookCatalog.Application.Dtos;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Models.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            
            CreateMap<Keyword, KeywordDto>().ReverseMap();

        }
       
    }
}
