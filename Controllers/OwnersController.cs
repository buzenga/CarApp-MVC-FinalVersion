using CarApp.Hibernate;
using CarApp.Models;
using CarApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IHibernateConnector _connector;

        public OwnersController(IHibernateConnector connector)
        {
            _connector = connector;
        }
        public IActionResult Index()
        {
            List<Owner> owners = _connector.GetAllOwners();
            return View(owners);
        }

        public IActionResult Detail(int id)
        {
            var owner = _connector.GetOwnerByID(id);
            return View(owner);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateOwnerViewModel());
        }
        [HttpPost]
        public IActionResult Create(CreateOwnerViewModel ownerVM)
        {
            _connector.CreateOwner(ownerVM.FirstName, ownerVM.LastName);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var owner = _connector.GetOwnerByID(id);
            if (owner == null) return View("Error");
            return View(owner);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteOwner(int id)
        {
            _connector.DeleteOwner(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var owner = _connector.GetOwnerByID(id);
            if (owner == null) return View("Error");

            var ownerVM = new CreateOwnerViewModel
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName
            };
            return View(ownerVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateOwnerViewModel owerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", owerVM);
            }
            var oldOwner = _connector.GetOwnerByID(id);

            
            oldOwner.LastName = owerVM.LastName;

            _connector.ChangeOwnerFirstName(id, owerVM.FirstName);
            _connector.ChangeOwnerLastName(id, owerVM.LastName);

            return RedirectToAction("Index");
        }
    }
}
