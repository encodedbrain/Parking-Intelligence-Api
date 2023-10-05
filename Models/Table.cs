namespace Parking_Intelligence_Api.Models;

public class Tables
{
    public Tables()
    {
        Passengers = 20.00M;
        Freight = 10.00M;
        Mixed = 40.00M;
    }

    public int Id { get; private set; }
    public decimal Passengers { get; }
    public decimal Freight { get; }
    public decimal Mixed { get; }

    public DateTime Dayofweek { get; set; }

    public decimal informsTheValueOfTheVacancy(string type)
    {
        if (type == "passageiro") return Passengers;
        if (type == "misto") return Mixed;
        else return Freight;
    }
}