# Unity V3 - Crime World

V3 adds the first GTA-style gameplay systems:
- Wanted level 0-5
- Police patrol/chase logic
- Roaming pedestrian NPCs
- Delivery mission with pickup and return-to-HQ stages
- Money/XP reward event
- HUD hooks for wanted stars and mission objective

Setup:
1. Add WantedSystem and MissionSystem to a GameManager object.
2. Add PoliceAI to police prefabs and assign player + WantedSystem.
3. Add PedestrianAI to NPC prefabs.
4. Assign pickup and HQ transforms to MissionSystem.
5. Add GameHUD to a Canvas and assign Text fields.
6. Trigger WantedSystem.AddWanted() from crimes and MissionSystem.StartDelivery() from the HQ mission marker.
