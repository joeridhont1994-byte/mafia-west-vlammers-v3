using UnityEngine;

public class WorldSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject carPrefab;
    public ThirdPersonCamera followCamera;

    void Start()
    {
        if(!playerPrefab)return;
        GameObject p=Instantiate(playerPrefab,new Vector3(4,1,4),Quaternion.identity);
        if(followCamera)followCamera.target=p.transform;
        if(carPrefab)Instantiate(carPrefab,new Vector3(10,1,4),Quaternion.identity);
    }
}
