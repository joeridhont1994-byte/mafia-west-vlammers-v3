# Unity V6 - PC + Android controls

V6 replaces player/camera interaction input with a shared PC/mobile input layer.

PC:
- WASD / arrows = move
- Mouse = camera
- Shift = sprint
- E = enter/exit vehicle

Android UI:
1. Add MobileInput to GameManager.
2. Canvas: left joystick panel + knob, attach VirtualJoystick.
3. Add a transparent right-side panel, attach TouchLookArea.
4. Add an Interact button with MobileActionButton Action=Interact.
5. Add a Sprint button with MobileActionButton Action=Sprint.
6. Keep an EventSystem in the scene.

For Android builds install Android Build Support, SDK/NDK and OpenJDK through Unity Hub, switch Build Profile to Android, then Build APK.
