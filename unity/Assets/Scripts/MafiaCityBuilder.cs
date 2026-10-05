using UnityEngine;

public class MafiaCityBuilder : MonoBehaviour
{
    [ContextMenu("Build Mafia West-Vlammers City")]
    public void BuildCity()
    {
        if (GameObject.Find("MAFIA_CITY_V1") != null)
        {
            Debug.Log("MAFIA_CITY_V1 bestaat al.");
            return;
        }

        var root = new GameObject("MAFIA_CITY_V1");

        CreateBox("CityGround", new Vector3(0,-0.5f,0), new Vector3(100,1,100), new Color(0.16f,0.28f,0.15f), root.transform);

        CreateBox("MainRoad", new Vector3(0,0.02f,0), new Vector3(14,0.15f,100), new Color(0.08f,0.08f,0.09f), root.transform);
        CreateBox("CrossRoad", new Vector3(0,0.03f,0), new Vector3(100,0.15f,14), new Color(0.08f,0.08f,0.09f), root.transform);

        for(int z=-45; z<=45; z+=10)
            CreateBox("RoadLine", new Vector3(0,0.12f,z), new Vector3(0.25f,0.04f,5), Color.white, root.transform);
        for(int x=-45; x<=45; x+=10)
            CreateBox("RoadLine", new Vector3(x,0.13f,0), new Vector3(5,0.04f,0.25f), Color.white, root.transform);

        Building("Mafia HQ", new Vector3(-25,4,-25), new Vector3(18,8,14), new Color(0.12f,0.12f,0.14f), root.transform);
        Building("Bank", new Vector3(25,4,-25), new Vector3(18,8,14), new Color(0.48f,0.48f,0.5f), root.transform);
        Building("Garage", new Vector3(25,3,25), new Vector3(20,6,16), new Color(0.2f,0.22f,0.25f), root.transform);
        Building("Velvet Club", new Vector3(-25,4,25), new Vector3(20,8,16), new Color(0.28f,0.08f,0.22f), root.transform);

        var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0,1,8);
        player.transform.SetParent(root.transform);
        player.AddComponent<CharacterController>();
        player.AddComponent<SimpleThirdPersonPlayer>();

        var cam = Camera.main;
        if(cam != null)
        {
            cam.transform.position = new Vector3(0,7,16);
            cam.transform.rotation = Quaternion.Euler(18,180,0);
            var follow = cam.gameObject.AddComponent<SimpleFollowCamera>();
            follow.target = player.transform;
        }

        Debug.Log("Mafia West-Vlammers City V1 gebouwd.");
    }

    void Building(string name, Vector3 pos, Vector3 scale, Color color, Transform parent)
    {
        CreateBox(name, pos, scale, color, parent);
        CreateBox(name+"_Door", pos + new Vector3(0,-scale.y*0.25f,-scale.z*0.505f), new Vector3(2.2f,scale.y*0.5f,0.25f), new Color(0.08f,0.06f,0.04f), parent);
    }

    GameObject CreateBox(string name, Vector3 pos, Vector3 scale, Color color, Transform parent)
    {
        var o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.name=name; o.transform.position=pos; o.transform.localScale=scale; o.transform.SetParent(parent);
        var r=o.GetComponent<Renderer>();
        var m=new Material(Shader.Find("Universal Render Pipeline/Lit"));
        m.color=color; r.material=m;
        return o;
    }
}

public class SimpleThirdPersonPlayer : MonoBehaviour
{
    public float speed=7f;
    void Update()
    {
        float h=Input.GetAxis("Horizontal"), v=Input.GetAxis("Vertical");
        Vector3 move=new Vector3(h,0,v);
        if(move.sqrMagnitude>0.01f)
        {
            transform.position += move.normalized*speed*Time.deltaTime;
            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(move),12f*Time.deltaTime);
        }
    }
}

public class SimpleFollowCamera : MonoBehaviour
{
    public Transform target;
    Vector3 offset=new Vector3(0,7,10);
    void LateUpdate()
    {
        if(!target)return;
        transform.position=Vector3.Lerp(transform.position,target.position+offset,8f*Time.deltaTime);
        transform.LookAt(target.position+Vector3.up);
    }
}
