using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;public Vector3 offset=new Vector3(0,3.2f,-5.5f);
    public float mouseSensitivity=2f,smooth=10f;float yaw,pitch=12f;
    void LateUpdate(){
        if(!target)return;Vector2 l=CrossPlatformInput.Look;
        yaw+=l.x*mouseSensitivity;pitch=Mathf.Clamp(pitch-l.y*mouseSensitivity,-10,55);
        Quaternion r=Quaternion.Euler(pitch,yaw,0);Vector3 wanted=target.position+r*offset;
        transform.position=Vector3.Lerp(transform.position,wanted,smooth*Time.deltaTime);
        transform.LookAt(target.position+Vector3.up*1.4f);
    }
}
