using RentACar.Models;

namespace RentACar.Service
{
    public class UserService : IUserService
    {
        private readonly RentACarContext _context;

        public UserService(RentACarContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();

        }

        public void DeleteUser(int id)
        {
            User user = _context.User.Find(id);

            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }

        }

        public IEnumerable<User> GetAllFilteredUsers(int? UserId, string firstName, string LastName, string Username, string Email, string PhoneNumber, string Address)
        {
            var users = _context.User.ToList();

            if (UserId != null)
            {
                users = users.Where(u => u.UserId == UserId).ToList();
            }

            if (firstName != null)
            {
                users = users.Where(u => u.FirstName.ToLower() == firstName.ToLower()).ToList();
            }

            if (LastName != null)
            {
                users = users.Where(u => u.LastName.ToLower() == LastName.ToLower()).ToList();
            }

            if (Username != null)
            {
                users = users.Where(u => u.Username.ToLower() == Username.ToLower()).ToList();
            }

            if (Email != null)
            {
                users = users.Where(u => u.Email.ToLower() == Email.ToLower()).ToList();
            }

            if (PhoneNumber != null)
            {
                users = users.Where(u => u.PhoneNumber.ToLower() == PhoneNumber.ToLower()).ToList();
            }

            if (Address != null)
            {
                users = users.Where(u => u.Address.ToLower() == Address.ToLower()).ToList();
            }

            return users;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.User.Find(id);
        }

        public void UpdateUser(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();
        }
    }
}
