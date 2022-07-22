using GTANetworkAPI;

namespace GVMP
{
    public static class VehicleDb
    {
        public static DbVehicle GetVehicle(this Vehicle vehicle)
        {
            if (vehicle == null)
                return null;
            if (!vehicle.HasData("vehicle") || vehicle.GetData("vehicle") == null)
                return null;
            return vehicle.GetData("vehicle") is DbVehicle data ? data : null;
        }
        public static bool IsValid(this DbVehicle iVehicle)
        {
            if (iVehicle == null || iVehicle.Vehicle.IsNull || iVehicle.Vehicle == null || !NAPI.Pools.GetAllVehicles().Contains(iVehicle.Vehicle))
                return false;
            return true;
        }

    }
}
