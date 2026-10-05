using UnityEngine;

public static class CrossPlatformInput
{
    public static Vector2 Move {
        get {
            Vector2 k=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
            if(k.sqrMagnitude>.01f)return Vector2.ClampMagnitude(k,1);
            return MobileInput.I?Vector2.ClampMagnitude(MobileInput.I.move,1):Vector2.zero;
        }
    }
    public static Vector2 Look {
        get {
            Vector2 m=new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
            if(m.sqrMagnitude>.001f)return m;
            return MobileInput.I?MobileInput.I.look:Vector2.zero;
        }
    }
    public static bool Sprint => Input.GetKey(KeyCode.LeftShift)||(MobileInput.I&&MobileInput.I.sprint);
    public static bool InteractDown => Input.GetKeyDown(KeyCode.E)||(MobileInput.I&&MobileInput.I.interactPressed);
}
