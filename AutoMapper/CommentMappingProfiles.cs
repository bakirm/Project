using AutoMapper;
using Project.Dtos;
using Project.Models;

namespace Project.Automapper
{
    public class CommentMappingProfiles : Profile
    {
        public CommentMappingProfiles()
        {
            CreateMap<Comment, CommentGetDto>();
            CreateMap<CommentPutPostDto, Comment>();
        }
    }
}
