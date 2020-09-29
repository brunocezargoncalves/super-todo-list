using Entities;

namespace Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Login(string email, string password);
    }
}
