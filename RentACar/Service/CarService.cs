using RentACar.Models;

namespace RentACar.Service
{
    public class CarService : ICarService
    {
        private readonly RentACarContext _context;

        public CarService(RentACarContext context)
        {
            _context = context;
        }
        public void AddCar(Car car)
        {
           _context.Car.Add(car);
           _context.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            Car car = _context.Car.Find(id);

            if (car != null)
            {
                _context.Car.Remove(car);
                _context.SaveChanges();

            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _context.Car.ToList();
            
        }

        public IEnumerable<Car> GetAllFilteredCars(int? carId, string make, string model, int year, int? minPrice, int? maxPrice, bool isAvailable, int? fuel, int? transmission, int? drive, int? type, int? seats, int? mileage, int? color)
        {
            
            var cars = _context.Car.AsQueryable();
            /*
         if(carId != null)
         {
             cars = cars.Where(c => c.CarId == carId);
         }

         if(make != null)
         {
             cars = cars.Where(c => c.Make.ToString().ToLower() == make.ToLower());
         }

         if(model != null)
         {
             cars = cars.Where(c => c.Model.ToString().ToLower() == model).ToList();
         }

         if (year != null)
         {
             cars = cars.Where(c => c.Year == year);
         }

         if (minPrice != null)
         {
             cars = cars.Where(c => c.PricePerDay > minPrice);
         }

         if (maxPrice != null)
         {
             cars = cars.Where(c => c.PricePerDay < maxPrice);
         }

         if (isAvailable != null)
         {
             cars = cars.Where(c => c.isAvailable == isAvailable).ToList();
         }

         if (fuel != null)
         {
             cars = cars.Where(c => c.Fuel.ToString == fuel).ToList();
         }

         if (transmission != null)
         {
             cars = cars.Where(c => c.Model.ToString().ToLower() == model).ToList();
         }

         if (drive != null)
         {
             cars = cars.Where(c => c.Model.ToString().ToLower() == model).ToList();
         }
         */
            return cars;
        }

        public Car GetCarById(int id)
        {
            return _context.Car.Find(id);
        }

        public void UpdateCar(Car car)
        {
            _context.Car.Update(car);
            _context.SaveChanges();
        }
    }
}
