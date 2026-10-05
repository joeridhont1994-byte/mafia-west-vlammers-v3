# Unity V4 - Vehicles & Police

V4 adds:
- Vehicle data assets with price, speed and acceleration
- Vehicle shop purchase logic
- Owned vehicle garage
- Vehicle spawning from the garage
- Enter/exit the nearest car with E
- Police units scale with wanted level (up to 8)

Unity setup:
1. Create VehicleData assets via Create > Mafia West-Vlammers > Vehicle.
2. Assign car prefabs and stats.
3. Add GarageSystem to the garage and assign its spawn point.
4. Add VehicleShop and connect GarageSystem.
5. Add VehicleEnterExit to GameManager and assign the third-person camera.
6. Add PoliceSpawner and assign WantedSystem, police prefab and player.
