using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using RentACar.Service;
using RentACar.ViewModels.User;
using static RentACar.Models.Car;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Controllers
{
    public class CarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var cars = _unitOfWork.CarService.GetAllCars();   
            return View(cars);
        }

       
        public IActionResult ShowAllCars()
        {
            var cars = _unitOfWork.CarService.GetAllCars();
            return View(cars);
        }

        //GET create
        [HttpGet]
        public IActionResult Create()
        {
            PopulateViewBags();
            return View();
        }

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Cars");
                    path = $"{path}\\{car.Make.ToString()}_{car.Model}_{car.Year}.jpg";

                    FileStream stream = new FileStream(path, FileMode.Create);
                    file.CopyTo(stream);

                    car.ImageUrl = $"{car.Make.ToString()}_{car.Model}_{car.Year}.jpg";
                }

                _unitOfWork.CarService.AddCar(car);

                IEnumerable<Car> cars = _unitOfWork.CarService.GetAllCars();
 
                return View("ShowAllCars", cars);
            }

            return View(car);
        }

        // GET user Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Car singleCar = _unitOfWork.CarService.GetCarById(id);

            if (singleCar == null)
            {
                return NotFound();
            }

            var model = new Car
            {
                Make = singleCar.Make,
                Model = singleCar.Model,
                Year = singleCar.Year,
                PricePerDay = singleCar.PricePerDay,
                isAvailable = singleCar.isAvailable,
                Fuel = singleCar.Fuel,
                Transmission = singleCar.Transmission,
                Drive = singleCar.Drive,
                Type = singleCar.Type,
                Color = singleCar.Color,    
                Seats = singleCar.Seats,
                Mileage = singleCar.Mileage,
                VIN = singleCar.VIN,    
                LicensePlate = singleCar.LicensePlate,  
                ImageUrl = singleCar.ImageUrl,
            };

            return View(model);

        }
        // Post edit user
        [HttpPost]
        public IActionResult Edit(int id, Car model)
        {
            Car car = _unitOfWork.CarService.GetCarById(id);

            if (car == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            car.Make = model.Make;
            car.Model = model.Model;
            car.Year = model.Year;
            car.PricePerDay = model.PricePerDay;
            car.isAvailable = model.isAvailable;
            car.Fuel = model.Fuel;
            car.Transmission = model.Transmission;
            car.Drive = model.Drive;
            car.Type = model.Type;
            car.Color = model.Color;
            car.Seats = model.Seats;
            car.Mileage = model.Mileage;
            car.VIN = model.VIN;
            car.LicensePlate = model.LicensePlate;
            car.ImageUrl = model.ImageUrl;

            _unitOfWork.CarService.UpdateCar(car);

            return RedirectToAction("ShowAllCars");
        }

        // GET Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Car car = _unitOfWork.CarService.GetCarById(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST Delete
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.CarService.DeleteCar(id);

            return RedirectToAction("ShowAllCars");
        }

        private void PopulateViewBags()
        {
            ViewBag.CarMakes = Enum.GetValues(typeof(Car.CarMake));
            ViewBag.FuelTypes = Enum.GetValues(typeof(Car.FuelType));
            ViewBag.TransmissionTypes = Enum.GetValues(typeof(Car.TransmissionType));
            ViewBag.DriveTypes = Enum.GetValues(typeof(Car.DriveType));
            ViewBag.CarTypes = Enum.GetValues(typeof(Car.CarType));
            ViewBag.ColorTypes = Enum.GetValues(typeof(Car.ColorType));
            ViewBag.SeatOptions = Enum.GetValues(typeof(Car.NumberSeats));
        }
    }
}
