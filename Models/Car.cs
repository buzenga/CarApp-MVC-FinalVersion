namespace CarApp.Models
{
    public class Car
    {
        public virtual int ID { get; set; }
        public virtual string PlateNumber { get; set; }
        public virtual string Producer { get; set; }
        public virtual string Model { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
