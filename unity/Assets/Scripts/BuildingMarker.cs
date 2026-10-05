using UnityEngine;

public class BuildingMarker : MonoBehaviour
{
    public string buildingName="Mafia HQ";
    public Transform entrance;
    public float triggerDistance=3f;
    public GameObject prompt;

    void Update()
    {
        GameObject p=GameObject.FindGameObjectWithTag("Player");
        bool near=p && Vector3.Distance(p.transform.position,entrance?entrance.position:transform.position)<=triggerDistance;
        if(prompt)prompt.SetActive(near);
    }
}
