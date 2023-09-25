namespace Parking_Intelligence_Api.Models
{
    public class Vehicle
    {
        public Vehicle(
            string model,
            string color,
            int year,
            string brand,
            int bodywork,
            string licensePlate,
            string species
        )
        {
            this.model = model;
            this.color = color;
            this.year = year;
            this.brand = brand;
            this.bodywork = bodywork;
            this.licensePlate = licensePlate;
            this.species = species;
        }

        public int id { get; private set; }
        public string model { get; private set; }
        public string color { get; private set; }
        public int year { get; private set; }
        public string brand { get; private set; }
        public int bodywork { get; private set; }
        public string licensePlate { get; private set; }
        public string species { get; private set; }
        public int user_id { get; private set; }
        public User User { get; private set; }
    }
}
