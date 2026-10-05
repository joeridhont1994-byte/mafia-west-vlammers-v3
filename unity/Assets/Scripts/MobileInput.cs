using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput I;
    public Vector2 move;
    public Vector2 look;
    public bool sprint;
    public bool interactPressed;

    void Awake(){I=this;}
    public void SetMoveX(float v){move.x=v;}
    public void SetMoveY(float v){move.y=v;}
    public void SetLookX(float v){look.x=v;}
    public void SetLookY(float v){look.y=v;}
    public void SetSprint(bool v){sprint=v;}
    public void Interact(){interactPressed=true;}
    void LateUpdate(){interactPressed=false;look=Vector2.zero;}
}
