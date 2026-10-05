using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    public Transform[] route;
    public float speed=8f;
    int index;

    void Update()
    {
        if(route==null||route.Length==0)return;
        Transform t=route[index];Vector3 d=t.position-transform.position;d.y=0;
        if(d.magnitude<2f){index=(index+1)%route.Length;return;}
        transform.position+=d.normalized*speed*Time.deltaTime;
        if(d.sqrMagnitude>.1f)transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(d),5f*Time.deltaTime);
    }
}
