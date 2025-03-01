using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using RentACar.Service;
using RentACar.ViewModels.User;

namespace RentACar.Controllers
{
    public class UserController : Controller
    {

        /*private readonly RentACarContext _context;

        public UserController(RentACarContext context)
        {
            _context = context;
        }*/

        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int page = 1)
        {
            //var users = _context.User.AsQueryable();
            var users = _unitOfWork.UserService.GetAllUsers();
            var pagesize = 5;

            PaginatedUsersViewModel puvm = new PaginatedUsersViewModel();
            puvm.PageSize = pagesize;
            puvm.CurrentPage = page;
            if(users.Count() % pagesize == 0)
            {
                puvm.TotalPages = users.Count() / pagesize;
            }
            else
            {
                puvm.TotalPages = users.Count() / pagesize + 1;
            }
            puvm.HasPreviousPage = puvm.CurrentPage > 1;
            puvm.HasNextPage = puvm.CurrentPage < puvm.TotalPages; 

            users = users.Skip(pagesize * (page - 1)).Take(pagesize).ToList();

            puvm.Users = users;

            return View(puvm);
        }

        // GET create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        // POST create
        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            ModelState.Remove("Username");

            if (!ModelState.IsValid)
            {
                foreach(var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Modelstate greska: " + error.ErrorMessage);
                }
                return View(model);
            }

            if(ModelState.IsValid)
            {
                Console.WriteLine("Model validan");
                
                string username = string.IsNullOrWhiteSpace(model.Username) ? model.Email : model.Username;

                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    PasswordHash = HashPassword(model.PasswordHash),
                    Username = username
                };

                _unitOfWork.UserService.AddUser(newUser);

                return RedirectToAction("ShowAllUsers");
            }

            return View(model);
        }

        // GET user Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User singleUser = _unitOfWork.UserService.GetUserById(id);

            if (singleUser == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                FirstName = singleUser.FirstName,
                LastName = singleUser.LastName, 
                Email = singleUser.Email,
                PhoneNumber = singleUser.PhoneNumber,
                Address = singleUser.Address,
                Username = singleUser.Username,
                PasswordHash=HashPassword(singleUser.PasswordHash),
            };

            return View(model);

        }
        // Post edit user
        [HttpPost]
        public IActionResult Edit(int id, UserViewModel model)
        {
            User user = _unitOfWork.UserService.GetUserById(id);

            if (user == null) 
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.Username = string.IsNullOrWhiteSpace(model.Username) ? model.Email : model.Username;
            user.PasswordHash = HashPassword(model.PasswordHash);

            _unitOfWork.UserService.UpdateUser(user);

            return RedirectToAction("ShowAllUsers");
        }

        // GET Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            User user = _unitOfWork.UserService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST Delete
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {                    
            _unitOfWork.UserService.DeleteUser(id);

            return RedirectToAction("ShowAllUsers");
        }

        public IActionResult ShowAllUsers(int? UserId, string firstName, string LastName, string Username, string Email, string PhoneNumber, string Address)
        {
            var users = _unitOfWork.UserService.GetAllFilteredUsers(UserId, firstName, LastName, Username, Email, PhoneNumber, Address);
            return View(users);
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
