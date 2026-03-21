using AutoMapper;
using BookCatalog.Domain.Entities;

namespace BookCatalog.API.Models
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
