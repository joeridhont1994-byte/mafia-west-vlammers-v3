using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCar : MonoBehaviour
{
    public float acceleration = 22f;
    public float maxSpeed = 22f;
    public float steerPower = 75f;
    public bool driving;
    Rigidbody rb;

    void Awake(){ rb=GetComponent<Rigidbody>(); rb.centerOfMass += Vector3.down*.45f; }

    void FixedUpdate()
    {
        if(!driving) return;
        float gas=Input.GetAxis("Vertical"), steer=Input.GetAxis("Horizontal");
        if(rb.velocity.magnitude < maxSpeed) rb.AddForce(transform.forward*gas*acceleration,ForceMode.Acceleration);
        float factor=Mathf.Clamp01(rb.velocity.magnitude/3f);
        transform.Rotate(0,steer*steerPower*factor*Time.fixedDeltaTime,0);
    }
}
