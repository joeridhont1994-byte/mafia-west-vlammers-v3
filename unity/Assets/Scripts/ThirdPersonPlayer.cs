using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayer : MonoBehaviour
{
    public Transform cameraTransform;
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float turnSmooth = 12f;
    CharacterController controller;

    void Awake(){ controller = GetComponent<CharacterController>(); }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(h,0,v).normalized;
        if(input.sqrMagnitude < .01f) return;

        float yaw = cameraTransform ? cameraTransform.eulerAngles.y : 0f;
        Vector3 dir = Quaternion.Euler(0,yaw,0) * input;
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        controller.Move(dir * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSmooth * Time.deltaTime);
    }
}
