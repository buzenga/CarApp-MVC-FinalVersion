using CarApp.Hibernate;
using CarApp.Models;
using CarApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly IHibernateConnector _connector;

        public CarsController(IHibernateConnector connector)
        {
            _connector = connector;
        }
        public IActionResult Index()
        {
            List<Car> cars = _connector.GetAllCars();
            return View(cars);
        }
        public IActionResult Detail(int id)
        {
            var car = _connector.GetCarByID(id);
            return View(car);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCarViewModel());
        }
        [HttpPost]
        public IActionResult Create(CreateCarViewModel carVM)
        {
            _connector.CreateCar(carVM.PlateNumber, carVM.Producer, carVM.Model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var car = _connector.GetCarByID(id);
            if (car == null) return View("Error");
            return View(car);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCar(int id)
        {
            _connector.DeleteCar(id);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult SetOwner(int id)
        {
            var car = _connector.GetCarByID(id);
            var allOwners = _connector.GetAllOwners();

            var setOwnerVM = new SetOwnerViewModel
            {
                Car = car,
                CarID = car.ID,
                AllOwners = allOwners
            };

            return View(setOwnerVM);
        }


        [HttpPost]
        public IActionResult SetOwner(int id, SetOwnerViewModel setOwerVM)
        {

            int ownerID = setOwerVM.Owner.ID;
            int carID = id;

            _connector.SetOrChangeCarOwner(ownerID, carID);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = _connector.GetCarByID(id);
            if (car == null) return View("Error");

            var carVM = new CreateCarViewModel
            {
                PlateNumber = car.PlateNumber,
                Producer = car.Producer,
                Model = car.Model
            };
            return View(carVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateCarViewModel carVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", carVM);
            }
            var oldCar = _connector.GetCarByID(id);


            //oldOwner.LastName = owerVM.LastName;

            _connector.ChangeCarPlateNumber(id, carVM.PlateNumber);
            _connector.ChangeCarProducer(id, carVM.Producer);
            _connector.ChangeCarModel(id, carVM.Model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DisconnectOwner(int id)
        {
            
            var car = _connector.GetCarByID(id);

            if(car.Owner == null) return RedirectToAction("Index");


            _connector.DisconnectCarAndOwner(id);
            

            return RedirectToAction("Index");
        }
    }
    
}

