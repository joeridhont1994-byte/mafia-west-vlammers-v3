using UnityEngine;
using System.Collections.Generic;

public class GarageSystem : MonoBehaviour
{
    public Transform spawnPoint;
    public List<VehicleData> ownedVehicles=new List<VehicleData>();
    GameObject activeVehicle;

    public bool Owns(VehicleData v)=>ownedVehicles.Contains(v);
    public void AddVehicle(VehicleData v){if(v&&!Owns(v))ownedVehicles.Add(v);}
    public void SpawnVehicle(VehicleData v)
    {
        if(!v||!Owns(v)||!v.prefab||!spawnPoint)return;
        if(activeVehicle)Destroy(activeVehicle);
        activeVehicle=Instantiate(v.prefab,spawnPoint.position,spawnPoint.rotation);
        ArcadeCar car=activeVehicle.GetComponent<ArcadeCar>();
        if(car){car.maxSpeed=v.topSpeed;car.acceleration=v.acceleration;}
    }
}
