using CarApp.Models;

namespace CarApp.ViewModels
{
    public class SetOwnerViewModel
    {
        public Car Car { get; set; }
        public int CarID {get; set; }
        public List<Owner> AllOwners { get; set; }
        public Owner Owner { get; set; }
    }
}
