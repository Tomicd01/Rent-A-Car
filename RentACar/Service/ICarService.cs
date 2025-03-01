using RentACar.Models;

namespace RentACar.Service
{
    public interface ICarService
    {
        IEnumerable<Car> GetAllCars();

        IEnumerable<Car> GetAllFilteredCars(int? carId, string make, string model,int year,  int? minPrice, int? maxPrice, bool isAvailable, int? fuel, int? transmission, int? drive, int? type, int? seats, int? mileage, int? color);

        Car GetCarById(int id);

        void AddCar(Car car);

        void UpdateCar(Car car);

        void DeleteCar(int id);
    }
}
