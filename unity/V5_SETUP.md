# Unity V5 - One-click playable scene

V5 adds an Editor world builder so the project can move from loose scripts to a playable Unity scene.

## Build it
1. Open the `unity` project in Unity.
2. Wait for scripts to compile.
3. Open a new empty 3D scene.
4. Choose **Mafia West-Vlammers > Build V5 Playable World**.
5. Press **Play**.

The builder creates:
- 3D ground and a road grid
- solid city buildings
- player + CharacterController
- third-person camera
- starter car + Rigidbody driving
- enter/exit vehicle system
- directional sun + day/night controller
- GameManager + wanted system

Controls: WASD/arrow keys move, Shift sprints, mouse controls camera, E enters/exits a nearby car.

This is a functional prototype scene generated from primitives. Realistic GTA-level graphics require proper 3D models, materials, animations, lighting and environment assets; those are the next content layer.
