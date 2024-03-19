using YoloHomeAPI.Data;

namespace YoloHomeAPI.Contexts;

public class YoloHomeDbContext
{
    public List<User> Users { get; private set; } = new(); 
    
    
    public YoloHomeDbContext()
    {
        
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}