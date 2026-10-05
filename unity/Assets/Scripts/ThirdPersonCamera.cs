using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0,3.2f,-5.5f);
    public float mouseSensitivity = 2f;
    public float smooth = 10f;
    float yaw, pitch = 12f;

    void LateUpdate()
    {
        if(!target) return;
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch - Input.GetAxis("Mouse Y") * mouseSensitivity, -10f, 55f);
        Quaternion rot = Quaternion.Euler(pitch,yaw,0);
        Vector3 wanted = target.position + rot * offset;
        transform.position = Vector3.Lerp(transform.position,wanted,smooth*Time.deltaTime);
        transform.LookAt(target.position + Vector3.up*1.4f);
    }
}
