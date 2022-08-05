using CarApp.Models;

namespace CarApp.Hibernate
{
    public interface IHibernateConnector
    {
        public Car CreateCar(string plateNumber, string producer, string model);
        public Owner CreateOwner(string firstName, string lastName);
        public List<Car> GetAllCars();
        public List<Owner> GetAllOwners();
        public Owner SetOrChangeCarOwner(int ownerID, int carID);
        public Car DisconnectCarAndOwner(int carID);
        public Owner GetOwnerByID(int ownerID);
        
        
        public Car GetCarByID(int carID);
        
        public Car ChangeCarPlateNumber(int carID, string plateNumber);
        public Car ChangeCarProducer(int carID, string carProducer);
        public Car ChangeCarModel(int carID, string carModel);
        public Owner ChangeOwnerFirstName(int ownerID, string ownerFirstName);
        public Owner ChangeOwnerLastName(int ownerID, string ownerLastName);

        public void DeleteCar(int carID);
        public void DeleteOwner(int ownerID);
    }
}
