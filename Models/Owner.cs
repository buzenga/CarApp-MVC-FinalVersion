namespace CarApp.Models
{
    public class Owner
    {
        public virtual int ID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual ISet<Car> Cars { get; set; }
    }
}
