using UnityEngine;
using System.Collections.Generic;

public class PoliceSpawner : MonoBehaviour
{
    public WantedSystem wanted;
    public GameObject policePrefab;
    public Transform player;
    public float spawnRadius=45f;
    public int maxPolice=8;
    readonly List<GameObject> units=new List<GameObject>();

    void Update()
    {
        units.RemoveAll(x=>!x);
        int desired=wanted?Mathf.Min(maxPolice,wanted.wantedLevel*2):0;
        while(units.Count<desired)Spawn();
        while(units.Count>desired){Destroy(units[units.Count-1]);units.RemoveAt(units.Count-1);}
    }
    void Spawn()
    {
        if(!policePrefab||!player)return;
        Vector2 r=Random.insideUnitCircle.normalized*spawnRadius;
        GameObject g=Instantiate(policePrefab,player.position+new Vector3(r.x,1,r.y),Quaternion.identity);
        PoliceAI ai=g.GetComponent<PoliceAI>();if(ai){ai.target=player;ai.wanted=wanted;}
        units.Add(g);
    }
}
