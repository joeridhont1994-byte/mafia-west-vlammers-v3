using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayer : MonoBehaviour
{
    public Transform cameraTransform;
    public float walkSpeed=4f,sprintSpeed=7f,turnSmooth=12f;
    CharacterController controller;
    void Awake(){controller=GetComponent<CharacterController>();}
    void Update(){
        Vector2 m=CrossPlatformInput.Move;Vector3 input=new Vector3(m.x,0,m.y).normalized;if(input.sqrMagnitude<.01f)return;
        float yaw=cameraTransform?cameraTransform.eulerAngles.y:0;Vector3 dir=Quaternion.Euler(0,yaw,0)*input;
        controller.Move(dir*(CrossPlatformInput.Sprint?sprintSpeed:walkSpeed)*Time.deltaTime);
        transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(dir),turnSmooth*Time.deltaTime);
    }
}
