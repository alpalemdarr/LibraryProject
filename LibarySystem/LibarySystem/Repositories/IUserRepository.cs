using LibarySystem.Models;

namespace LibarySystem.Repositories
{
    public interface IUserRepository
    {
        void AddUser(UserDTO user);
        List<UserDTO> GetAllUsers();
        UserDTO GetUserById(int userId);
        void UpdateUser(UserDTO user);
        void DeleteUser(UserDTO user);
    }
}
