#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class MafiaWorldBuilder
{
    [MenuItem("Mafia West-Vlammers/Build V6 GTA Style City")]
    public static void Build()
    {
        ClearGenerated();
        Random.InitState(20261005);
        var root = new GameObject("MWV_V6_CITY");

        // Ground + 5x5 street grid
        MakeCube("Ground", new Vector3(0,-.65f,0), new Vector3(260,1,260), new Color(.12f,.20f,.12f), root.transform);
        for(int i=-2;i<=2;i++)
        {
            float p=i*48f;
            Road("Road_NS_"+i,new Vector3(p,0,0),new Vector3(14,.16f,260),true,root.transform);
            Road("Road_EW_"+i,new Vector3(0,0,p),new Vector3(260,.16f,14),false,root.transform);
        }

        // City blocks: sidewalks + varied buildings
        for(int bx=-2;bx<2;bx++) for(int bz=-2;bz<2;bz++)
        {
            Vector3 center=new Vector3(bx*48+24,.12f,bz*48+24);
            MakeCube("Sidewalk_"+bx+"_"+bz,center,new Vector3(31,.25f,31),new Color(.35f,.36f,.35f),root.transform);
            for(int n=0;n<4;n++)
            {
                float ox=(n%2==0?-8:8), oz=(n<2?-8:8);
                float h=Random.Range(8f,24f);
                Building("Block_"+bx+"_"+bz+"_"+n,center+new Vector3(ox,h/2,oz),new Vector3(12,h,12),
                    Color.Lerp(new Color(.20f,.22f,.24f),new Color(.42f,.34f,.27f),Random.value),root.transform);
            }
        }

        // Named locations
        Landmark("MAFIA HQ",new Vector3(-72,6,-72),new Vector3(24,12,22),new Color(.10f,.11f,.13f),root.transform);
        Landmark("BANK",new Vector3(72,7,-72),new Vector3(24,14,22),new Color(.48f,.46f,.39f),root.transform);
        Landmark("VELVET CLUB",new Vector3(-72,6,72),new Vector3(26,12,22),new Color(.28f,.05f,.20f),root.transform);
        Landmark("GARAGE",new Vector3(72,4,72),new Vector3(28,8,24),new Color(.16f,.19f,.22f),root.transform);

        // Street furniture
        for(int i=-110;i<=110;i+=22)
        {
            Lamp(new Vector3(-7.8f,0,i),root.transform); Lamp(new Vector3(7.8f,0,i),root.transform);
            Lamp(new Vector3(i,0,-7.8f),root.transform); Lamp(new Vector3(i,0,7.8f),root.transform);
        }

        // Player
        var player=GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name="Player"; player.tag="Player"; player.transform.position=new Vector3(5,1.1f,5);
        Object.DestroyImmediate(player.GetComponent<Collider>());
        var cc=player.AddComponent<CharacterController>(); cc.height=2f; cc.radius=.45f;
        var move=player.AddComponent<ThirdPersonPlayer>();

        // Third-person camera
        var oldCam=Camera.main; if(oldCam) Object.DestroyImmediate(oldCam.gameObject);
        var camObj=new GameObject("Main Camera"); camObj.tag="MainCamera";
        camObj.AddComponent<Camera>(); camObj.AddComponent<AudioListener>();
        var follow=camObj.AddComponent<ThirdPersonCamera>(); follow.target=player.transform; move.cameraTransform=camObj.transform;
        camObj.transform.position=player.transform.position+new Vector3(0,4,-7);

        // Sun/day-night
        var sunObj=new GameObject("Sun"); var sun=sunObj.AddComponent<Light>(); sun.type=LightType.Directional; sun.intensity=1.15f;
        sunObj.transform.rotation=Quaternion.Euler(48,-32,0);
        var cycle=root.AddComponent<DayNightCycle>(); cycle.sun=sun;

        // Driveable starter car
        var car=MakeCube("StarterCar",new Vector3(10,.7f,5),new Vector3(1.9f,1.2f,4.2f),new Color(.48f,.03f,.04f),root.transform);
        var rb=car.AddComponent<Rigidbody>(); rb.mass=1250; rb.constraints=RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ;
        car.AddComponent<ArcadeCar>();

        // Systems
        var gm=new GameObject("GameManager");
        var enter=gm.AddComponent<VehicleEnterExit>(); enter.cameraRig=follow;
        gm.AddComponent<WantedSystem>();

        Selection.activeGameObject=root;
        EditorUtility.SetDirty(root);
        Debug.Log("Mafia West-Vlammers V6 GTA-style city built. Save the scene and press Play.");
    }

    static void Road(string name,Vector3 pos,Vector3 scale,bool ns,Transform parent)
    {
        MakeCube(name,pos,scale,new Color(.055f,.06f,.065f),parent);
        for(int j=-120;j<=120;j+=12)
        {
            Vector3 p=ns?new Vector3(pos.x,.12f,j):new Vector3(j,.12f,pos.z);
            Vector3 s=ns?new Vector3(.18f,.025f,5):new Vector3(5,.025f,.18f);
            MakeCube(name+"_Line_"+j,p,s,new Color(.85f,.76f,.30f),parent);
        }
    }

    static void Building(string name,Vector3 pos,Vector3 scale,Color color,Transform parent)
    {
        var b=MakeCube(name,pos,scale,color,parent);
        // entrance
        MakeCube(name+"_Door",pos+new Vector3(0,-scale.y*.32f,-scale.z*.505f),new Vector3(2.2f,scale.y*.35f,.18f),new Color(.06f,.07f,.08f),parent);
        // simple window rows on front
        int floors=Mathf.Clamp(Mathf.FloorToInt(scale.y/3f),2,7);
        for(int y=0;y<floors;y++) for(int x=-1;x<=1;x++)
            MakeCube(name+"_Window",new Vector3(pos.x+x*3f,pos.y-scale.y*.35f+y*2.6f,pos.z-scale.z*.506f),
                new Vector3(1.35f,1.25f,.12f),new Color(.18f,.42f,.58f),parent);
    }

    static void Landmark(string name,Vector3 pos,Vector3 scale,Color color,Transform parent)
    {
        Building(name,pos,scale,color,parent);
        MakeCube(name+"_Sign",pos+new Vector3(0,scale.y*.28f,-scale.z*.53f),new Vector3(scale.x*.65f,1.2f,.25f),new Color(.75f,.58f,.12f),parent);
    }

    static void Lamp(Vector3 p,Transform parent)
    {
        MakeCube("LampPost",p+Vector3.up*2.8f,new Vector3(.18f,5.6f,.18f),new Color(.08f,.09f,.10f),parent);
        var bulb=MakeCube("Lamp",p+Vector3.up*5.65f,new Vector3(.65f,.22f,.65f),new Color(1f,.78f,.38f),parent);
        var light=bulb.AddComponent<Light>(); light.type=LightType.Point; light.range=10; light.intensity=.7f;
    }

    static GameObject MakeCube(string n,Vector3 p,Vector3 s,Color c,Transform parent)
    {
        var g=GameObject.CreatePrimitive(PrimitiveType.Cube); g.name=n; g.transform.position=p; g.transform.localScale=s; g.transform.SetParent(parent);
        var shader=Shader.Find("Universal Render Pipeline/Lit"); if(!shader) shader=Shader.Find("Standard");
        var mat=new Material(shader); mat.color=c; g.GetComponent<Renderer>().sharedMaterial=mat; return g;
    }

    static void ClearGenerated()
    {
        string[] names={"MWV_V5_WORLD","MWV_V6_CITY","Player","Main Camera","GameManager","Sun"};
        foreach(var n in names){var o=GameObject.Find(n);if(o)Object.DestroyImmediate(o);}
    }
}
#endif
