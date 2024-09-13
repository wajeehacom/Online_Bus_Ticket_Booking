namespace Infrastructure1.Repositories
{
    public interface IConfiguration
    {
        string? GetConnectionString(string v);
    }
}