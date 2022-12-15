using Project.Models;

namespace Project.Dtos
{
    public class PostGetDto
    {
        public string slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<string> tagList { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
