#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public static class MafiaWorldBuilder
{
    [MenuItem("Mafia West-Vlammers/Build V5 Playable World")]
    public static void Build()
    {
        ClearGenerated();
        var root=new GameObject("MWV_V5_WORLD");

        // Ground
        MakeCube("Ground",new Vector3(0,-.5f,0),new Vector3(220,1,220),new Color(.18f,.28f,.16f),root.transform);

        // Roads
        for(int i=-2;i<=2;i++){
            MakeCube("Road_NS_"+i,new Vector3(i*40,0,0),new Vector3(12,.12f,220),new Color(.08f,.09f,.10f),root.transform);
            MakeCube("Road_EW_"+i,new Vector3(0,0,i*40),new Vector3(220,.12f,12),new Color(.08f,.09f,.10f),root.transform);
        }

        // Buildings
        for(int x=-2;x<2;x++)for(int z=-2;z<2;z++){
            float h=Random.Range(10f,28f);
            var b=MakeCube("Building_"+x+"_"+z,new Vector3(x*40+20,h/2,z*40+20),new Vector3(23,h,23),new Color(.24f,.27f,.30f),root.transform);
            AddWindows(b,h);
        }

        // Player
        var player=GameObject.CreatePrimitive(PrimitiveType.Capsule);player.name="Player";player.tag="Player";player.transform.position=new Vector3(5,1,5);
        Object.DestroyImmediate(player.GetComponent<Collider>());
        var cc=player.AddComponent<CharacterController>();cc.height=2;cc.radius=.45f;
        var move=player.AddComponent<ThirdPersonPlayer>();

        // Camera
        var camObj=new GameObject("Main Camera");camObj.tag="MainCamera";
        var cam=camObj.AddComponent<Camera>();camObj.AddComponent<AudioListener>();
        var follow=camObj.AddComponent<ThirdPersonCamera>();follow.target=player.transform;move.cameraTransform=camObj.transform;
        camObj.transform.position=new Vector3(0,5,-8);

        // Sun
        var sunObj=new GameObject("Sun");var sun=sunObj.AddComponent<Light>();sun.type=LightType.Directional;sun.intensity=1.1f;
        sunObj.transform.rotation=Quaternion.Euler(45,-35,0);
        var cycle=root.AddComponent<DayNightCycle>();cycle.sun=sun;

        // Car
        var car=MakeCube("StarterCar",new Vector3(10,.65f,5),new Vector3(1.8f,1.2f,4f),new Color(.55f,.04f,.05f),root.transform);
        var rb=car.AddComponent<Rigidbody>();rb.mass=1250;rb.constraints=RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ;
        car.AddComponent<ArcadeCar>();

        // Interaction manager
        var gm=new GameObject("GameManager");
        var enter=gm.AddComponent<VehicleEnterExit>();enter.cameraRig=follow;
        gm.AddComponent<WantedSystem>();

        Selection.activeGameObject=root;
        Debug.Log("Mafia West-Vlammers V5 playable world built. Press Play.");
    }

    static GameObject MakeCube(string n,Vector3 p,Vector3 s,Color c,Transform parent){
        var g=GameObject.CreatePrimitive(PrimitiveType.Cube);g.name=n;g.transform.position=p;g.transform.localScale=s;g.transform.SetParent(parent);
        var mat=new Material(Shader.Find("Standard"));mat.color=c;g.GetComponent<Renderer>().sharedMaterial=mat;return g;
    }

    static void AddWindows(GameObject building,float h){
        // Lightweight façade markers; replace later with real modular building assets.
        building.name += "_Facade";
    }

    static void ClearGenerated(){
        var old=GameObject.Find("MWV_V5_WORLD");if(old)Object.DestroyImmediate(old);
        var p=GameObject.Find("Player");if(p)Object.DestroyImmediate(p);
        var c=GameObject.Find("Main Camera");if(c)Object.DestroyImmediate(c);
        var g=GameObject.Find("GameManager");if(g)Object.DestroyImmediate(g);
        var s=GameObject.Find("Sun");if(s)Object.DestroyImmediate(s);
    }
}
#endif
