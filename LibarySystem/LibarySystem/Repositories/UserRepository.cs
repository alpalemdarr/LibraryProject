using LibarySystem.Data;
using LibarySystem.Models;

namespace LibarySystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<UserDTO> _users = new List<UserDTO>();
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public void AddUser(UserDTO _user)
        {
            User user = new User();

            user.Name = _user.Name;
            user.Email = _user.Email;
            user.MembershipDate = _user.MembershipDate;
            
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(UserDTO _user)
        {
            var user = _context.Users.Find(_user.UserId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public UserDTO GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return null; // Kullanıcı bulunamazsa null döndür
            }

            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                MembershipDate = user.MembershipDate
            };
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = _context.Users.ToList(); // Veritabanından tüm arabaları çekmek
            var userDTO = users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                MembershipDate = user.MembershipDate
            }).ToList();

            return userDTO;
        }

        public void UpdateUser(UserDTO _user)
        {
            var user = _context.Users.Find(_user.UserId);
            if (user != null)
            {
                user.Name = _user.Name;
                user.Email = _user.Email;
                user.MembershipDate= _user.MembershipDate;

                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }
    }
}
