namespace Blog.Domain.Posts.Repositories;

public interface IPostRepository
{
    public void Add(Post post);
    public Task<List<Post>> GetAll();
    public Task<Post> GetById(int id);
    public void Update(Post post);
    public void Remove(Post post);
}
