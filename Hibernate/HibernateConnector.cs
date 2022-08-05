using CarApp.Models;
using NHibernate;
using NHibernate.Cfg;

namespace CarApp.Hibernate
{
    public class HibernateConnector : IHibernateConnector
    {


        private static ISessionFactory mySessionFactory = SetupSessionFactory();
        private static ISessionFactory SetupSessionFactory()
        {
            Configuration myConfiguration = new Configuration().Configure();
            ISessionFactory mySessionFactory = myConfiguration.BuildSessionFactory();
            return mySessionFactory;
        }

        public Owner SetOrChangeCarOwner(int ownerID, int carID)
        {
            Owner owner;
            NHibernate.ISession mySession = mySessionFactory.OpenSession();
            
                mySession.BeginTransaction();
                
                    owner = mySession.Get<Owner>(ownerID);
                    if (owner == null)
                    {
                        Console.WriteLine("No such Person in DataBase");
                        return null;
                    }
                    var car = mySession.Get<Car>(carID);
                    if (car == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return null;
                    }
  
                    car.Owner = owner;

                    mySession.Update(car);
                    mySession.Transaction.Commit();
                
                
            

            return owner;
        }

        public Car CreateCar(string plateNumber, string producer, string model)
        {
            var car = new Car
            {
                PlateNumber = plateNumber.ToUpper(),
                Producer = producer.ToUpper(),
                Model = model.ToUpper()
            };

            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    mySession.Save(car);
                    mySession.Transaction.Commit();
                }
                
            }

            return car;
        }

        public Owner CreateOwner(string firstName, string lastName)
        {

            var owner = new Owner
            {
                FirstName = firstName.ToUpper(),
                LastName = lastName.ToUpper(),
                ////////////////////////////////
                Cars = { }
            };
            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    mySession.Save(owner);
                    mySession.Transaction.Commit();
                }
                
            }

            return owner;
        }
        public List<Car> GetAllCars()
        {
            List<Car> output = null;

            NHibernate.ISession mySession = mySessionFactory.OpenSession();

            output = (List<Car>)mySession.CreateCriteria<Car>().List<Car>();


            if (!output.Any()) Console.WriteLine("No Cars in DB");


            return output;
        }


        public List<Owner> GetAllOwners()
        {

            List<Owner> output = null;

            NHibernate.ISession mySession = mySessionFactory.OpenSession();
            
                output = (List<Owner>)mySession.CreateCriteria<Owner>().List<Owner>();
            

            if (!output.Any()) Console.WriteLine("No Car Owners in DB");


            return output;
        }

        public Car GetCarByID(int carID)
        {
            Car car;
            NHibernate.ISession mySession = mySessionFactory.OpenSession();
            
                car = mySession.Get<Car>(carID);
                if (car == null)
                {
                    Console.WriteLine("No such Car in DataBase");
                    return null;
                }

            return car;
        }

        public Owner GetOwnerByID(int ownerID)
        {
            Owner owner;
            NHibernate.ISession mySession = mySessionFactory.OpenSession();
            
                owner = mySession.Get<Owner>(ownerID);
                if (owner == null)
                {
                    Console.WriteLine("No such Person in DataBase");
                    return null;
                }

            return owner;
        }

        

        

        public Car ChangeCarPlateNumber(int carID, string plateNumber)
        {
            Car car;
            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    car = mySession.Get<Car>(carID);
                    if (car == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return null;
                    }

                    car.PlateNumber = plateNumber.ToUpper();

                    mySession.Update(car);
                    mySession.Transaction.Commit();
                }
                
            }

            return car;
        }

        public Car ChangeCarProducer(int carID, string carProducer)
        {
            Car car;
            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    car = mySession.Get<Car>(carID);
                    if (car == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return null;
                    }

                    car.Producer = carProducer.ToUpper();

                    mySession.Update(car);
                    mySession.Transaction.Commit();
                }
                
            }

            return car;
        }

        public Car ChangeCarModel(int carID, string carModel)
        {
            Car car;
            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using(mySession.BeginTransaction())
                {
                    car = mySession.Get<Car>(carID);
                    if (car == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return null;
                    }

                    car.Model = carModel.ToUpper();

                    mySession.Update(car);
                    mySession.Transaction.Commit();
                }
                
            }

            return car;
        }

        public Owner ChangeOwnerFirstName(int ownerID, string ownerFirstName)
        {
            Owner owner;
            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    owner = mySession.Get<Owner>(ownerID);
                    if (owner == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return null;
                    }

                    owner.FirstName = ownerFirstName.ToUpper();

                    mySession.Update(owner);
                    mySession.Transaction.Commit();
                }
                
            }

            return owner;
        }

        public Owner ChangeOwnerLastName(int ownerID, string ownerLastName)
        {
            Owner owner;
            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    owner = mySession.Get<Owner>(ownerID);
                    if (owner == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return null;
                    }

                    owner.LastName = ownerLastName.ToUpper();

                    mySession.Update(owner);
                    mySession.Transaction.Commit();
                }
                
            }

            return owner;
        }

        

        public void DeleteCar(int carID)
        {
            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    var car = mySession.Get<Car>(carID);
                    if (car == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return;
                    }


                    mySession.Delete(car);
                    mySession.Transaction.Commit();
                }
                
            }
        }

        public void DeleteOwner(int ownerID)
        {

            using (NHibernate.ISession mySession = mySessionFactory.OpenSession())
            {
                using (mySession.BeginTransaction())
                {
                    var owner = mySession.Get<Owner>(ownerID);
                    if (owner == null)
                    {
                        Console.WriteLine("No such Person in DataBase");
                        return;
                    }
                    foreach (var car in owner.Cars)
                    {
                        car.Owner = null;
                        owner.Cars.Remove(car);
                        
                        mySession.Update(car);
                        mySession.Update(owner);
                    }

                    mySession.Delete(owner);
                    mySession.Transaction.Commit();
                }               
            }
        }

        public Car DisconnectCarAndOwner(int carID)
        {
            Car car;

            NHibernate.ISession mySession = mySessionFactory.OpenSession();


                mySession.BeginTransaction();
                
                    car = mySession.Get<Car>(carID);
                    if (car == null)
                    {
                        Console.WriteLine("No such Car in DataBase");
                        return null;
                    }
                    
                    car.Owner = null;

                    mySession.Update(car);
                    mySession.Transaction.Commit();

            
            return car;
        }

        
    }
}
