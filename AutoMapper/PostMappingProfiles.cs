using AutoMapper;
using Project.Dtos;
using Project.Models;

namespace Project.Automapper
{
    public class PostMappingProfiles : Profile
    {
        public PostMappingProfiles()
        {
            CreateMap<PostCreateDto, Post>();
            CreateMap<Post, PostGetDto>();
        }
    }
}
