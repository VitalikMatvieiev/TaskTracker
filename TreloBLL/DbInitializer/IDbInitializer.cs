namespace TreloBLL.DbInitializer
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedData();
    }
}