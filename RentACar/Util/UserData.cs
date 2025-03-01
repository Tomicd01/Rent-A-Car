using RentACar.Models;

namespace RentACar.Util
{
    public class UserData
    {
        private static List<User> _users = null;

        public static IEnumerable<User> InitUsers()
        {
            if (_users == null)
            {
                _users = new List<User>()
                {
                    new User
                    {
                        UserId = 1,
                        FirstName = "Marko",
                        LastName = "Markovic",
                        Email = "marko@gmail.com",
                        PhoneNumber = "064255613",
                        Address = "Beograd",
                        Username = "marko123",
                        PasswordHash = "hashpass"
                    },

                    new User
                    {
                        UserId = 2,
                        FirstName = "Jovan",
                        LastName = "Jovanovic",
                        Email = "jovan@gmail.com",
                        PhoneNumber = "0651234567",
                        Address = "Novi Sad",
                        Username = "jovan99",
                        PasswordHash = "hashpass2"
                    },
                };
            }

            return _users;
        }


        public static void AddUser(User user)
        {
            if(_users == null)
            {
                InitUsers();
            }

            _users.Add(user);
        }

        public static void RemoveUser(int userId)
        {
            if (_users == null)
            {
                InitUsers();
            }

            User deleteUser = _users.FirstOrDefault(u => u.UserId == userId);
            if (deleteUser != null)
            {
                _users.Remove(deleteUser);
            }
        }
    }
}
