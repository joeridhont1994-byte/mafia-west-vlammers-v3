using UnityEngine;

public class PedestrianAI : MonoBehaviour
{
    public float walkSpeed=1.8f;
    public float roamRadius=14f;
    Vector3 origin,target;

    void Start(){origin=transform.position;PickTarget();}
    void Update(){Vector3 d=target-transform.position;d.y=0;if(d.magnitude<.8f){PickTarget();return;}transform.position+=d.normalized*walkSpeed*Time.deltaTime;if(d.sqrMagnitude>.1f)transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(d),4f*Time.deltaTime);}
    void PickTarget(){Vector2 r=Random.insideUnitCircle*roamRadius;target=origin+new Vector3(r.x,0,r.y);}
}
