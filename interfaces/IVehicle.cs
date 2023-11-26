using Parking_Intelligence_Api.Schemas.vehicle;

namespace Parking_Intelligence_Api.interfaces;

public interface IVehicle
{
    public object[] GetVehicles(GetVehicleSchema prop);
    public bool UpdateVehicle(UpdateVehicleSchema prop, string vacancy);
    public bool  DeleteVehicle(DeleteVehicleSchema prop);
}