namespace Parking_Intelligence_Api.Models
{
    public class Vehicle
    {
        public Vehicle() { }

        public Vehicle(
            string model,
            string year,
            string type,
            string species,
            string bodywork,
            string licensePlate
        )
        {
            var rnd = new Random().Next();
            Id = rnd;
            Model = model;
            Year = year;
            Type = type;
            Species = species;
            Bodywork = bodywork;
            LicensePlate = licensePlate;
            VehicleId = rnd;
            ParkingId = rnd;
        }

        public int Id { get; private set; }
        public int VehicleId { get; private set; }
        public int ParkingId { get; private set; }
        public string Color { get; private set; }
        public string Model { get; private set; }
        public string Year { get; private set; }
        public string Type { get; private set; }
        public string Species { get; private set; }
        public string Bodywork { get; private set; }
        public string LicensePlate { get; private set; }
        public IEnumerable<Shopping> Shopping { get; private set; }
        public Parking Parkings { get; private set; }
    }
}
