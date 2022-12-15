using AutoMapper;
using Project.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services;
using Slugify;
using Microsoft.AspNetCore.Http;

namespace Project.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly BlogContext _ctx;
        private readonly IMapper _mapper;
        public PostsController(BlogContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] string tag)
        {
            if (tag != null)
            {
                var posts = await _ctx.Posts.Where(a => a.Tags.Any(g => g.Text == tag)).Include(a => a.Tags).OrderBy(a=>a.createdAt).ToListAsync();

                List<PostGetDto> results = PostGetMethod(posts);

                return Ok(results);
            }
            else
            {

                var posts = await _ctx.Posts.Include(a => a.Tags).ToListAsync();

                var postsGet = _mapper.Map<List<PostGetDto>>(posts);

                List<PostGetDto> results = PostGetMethod(posts);

                return Ok(new { blogPosts = results, postsCount = results.Count });
            }

        }

        private List<PostGetDto> PostGetMethod(List<Post> posts)
        {
            var results = new List<PostGetDto>();

            foreach (var post in posts)
            {
                var result = _mapper.Map<PostGetDto>(post);

                result.tagList = post.Tags.Select(tag => tag.Text).ToList();

                results.Add(result);
            }

            return results;
        }

        [Route("{slug}")]
        [HttpGet]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {

            var posts = await _ctx.Posts.Where(h => h.slug == slug).Include(a => a.Tags).FirstOrDefaultAsync();

            var result = _mapper.Map<PostGetDto>(posts);

            result.tagList = posts.Tags.Select(tag => tag.Text).ToList();

            if (result == null)
                return NotFound();

            return Ok(new { blogPost = result });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDto postBody)
        {
            SlugHelper helper = new SlugHelper();

            var post = _mapper.Map<Post>(postBody);

            post.slug = helper.GenerateSlug(postBody.Title.ToLower());
            post.createdAt = DateTime.Now;
            post.updatedAt = DateTime.Now;

            _ctx.Posts.Add(post);

            if (postBody.tagList != null)
            {
                foreach (var tag in postBody.tagList)
                {
                    var Tag = new Tag()
                    {
                        Post = post,
                        Text = tag
                    };

                    _ctx.Tags.Add(Tag);
                }

            }

            await _ctx.SaveChangesAsync();

            var postGet = _mapper.Map<PostGetDto>(post);

            if (post.Tags != null)
            {

                postGet.tagList = post.Tags.Select(tag => tag.Text).ToList();
            }
            return Ok(new { blogPost = postGet });

        }

        [HttpPut]
        [Route("{slug}")]
        public async Task<IActionResult> UpdatePost([FromBody] PostCreateDto updated, string slug)
        {
            SlugHelper helper = new SlugHelper();


            var post = await _ctx.Posts.FirstOrDefaultAsync(h => h.slug == slug);


            var toUpdate = _mapper.Map<Post>(post);

            toUpdate.slug = helper.GenerateSlug(updated.Title);

            toUpdate.updatedAt = DateTime.Now;

            _ctx.Posts.Update(toUpdate);

            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{slug}")]
        public async Task<IActionResult> DeletePost(string slug)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(h => h.slug == slug);

            if (post == null)
                return NotFound();

            _ctx.Posts.Remove(post);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("{slug}/comments")]
        public async Task<IActionResult> GetPostComments(string slug)
        {

            var post = await _ctx.Posts.Include(h => h.Comments)
                .FirstOrDefaultAsync(h => h.slug == slug);

            var comments = new List<CommentGetDto>();

            post.Comments.ForEach(comment => comments.Add(_mapper.Map<CommentGetDto>(comment)));

            return Ok(comments);
        }

        [HttpPost]
        [Route("{slug}/comments")]
        public async Task<IActionResult> CreatePostComments(string slug, [FromBody] CommentPutPostDto newComment)
        {
            var commentNew = _mapper.Map<Comment>(newComment);

            var comments = await _ctx.Posts.Include(h => h.Comments)
                .FirstOrDefaultAsync(h => h.slug == slug);

            commentNew.createdAt = DateTime.Now;
            commentNew.updatedAt = DateTime.Now;

            comments.Comments.Add(commentNew);

            await _ctx.SaveChangesAsync();

            var mappedComment = _mapper.Map<CommentGetDto>(commentNew);

            return Ok(mappedComment);
        }

        [HttpGet]
        [Route("tags")]
        public async Task<IActionResult> GetTags()
        {

            var dbTags = await _ctx.Tags.GroupBy(a=>a.Text).Select(a=>a.First()).ToListAsync();

            var tags = new List<string>();

            dbTags.ForEach(dbTag => tags.Add(dbTag.Text));

            return Ok(tags);
        }

        [HttpDelete]
        [Route("{slug}/comments/{id}")]
        public async Task<IActionResult> RemoveComment(string slug, int id)
        {

            var ids = await _ctx.Posts.Include(h => h.Comments)
               .FirstOrDefaultAsync(h => h.slug == slug);

            var comment = await _ctx.Comments.SingleOrDefaultAsync(r => slug == ids.slug && r.Id == id);

            if (comment == null)
                return NotFound("Comment not found");

            _ctx.Comments.Remove(comment);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
