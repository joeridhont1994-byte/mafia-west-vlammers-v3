using UnityEngine;

public class VehicleShop : MonoBehaviour
{
    public GarageSystem garage;
    public int playerMoney=100000;

    public bool Buy(VehicleData vehicle)
    {
        if(!vehicle||garage==null||garage.Owns(vehicle)||playerMoney<vehicle.price)return false;
        playerMoney-=vehicle.price;
        garage.AddVehicle(vehicle);
        return true;
    }
}
