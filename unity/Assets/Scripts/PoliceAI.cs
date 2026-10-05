using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    public Transform target;
    public WantedSystem wanted;
    public float patrolSpeed=5f,chaseSpeed=11f,arrestDistance=2.5f;
    public Transform[] patrolPoints;
    int patrolIndex;

    void Update()
    {
        if(wanted && wanted.wantedLevel>0 && target){MoveTo(target.position,chaseSpeed);if(Vector3.Distance(transform.position,target.position)<arrestDistance)Debug.Log("PLAYER ARRESTED");return;}
        if(patrolPoints==null||patrolPoints.Length==0)return;
        Transform p=patrolPoints[patrolIndex];MoveTo(p.position,patrolSpeed);
        if(Vector3.Distance(transform.position,p.position)<2f)patrolIndex=(patrolIndex+1)%patrolPoints.Length;
    }

    void MoveTo(Vector3 pos,float speed){Vector3 d=pos-transform.position;d.y=0;if(d.sqrMagnitude<.2f)return;transform.position+=d.normalized*speed*Time.deltaTime;transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(d),5f*Time.deltaTime);}
}
