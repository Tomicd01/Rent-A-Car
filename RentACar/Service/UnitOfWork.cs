namespace RentACar.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentACarContext _context;
        public IUserService UserService { get; private set; }

        public ICarService CarService { get; private set; }

        public UnitOfWork(RentACarContext context)
        {
            _context = context;

            UserService = new UserService(context);

            CarService = new CarService(context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
