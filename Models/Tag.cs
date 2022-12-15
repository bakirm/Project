using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Tag
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
