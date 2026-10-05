using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public int blocksX=5, blocksZ=5;
    public float blockSize=38f, roadWidth=12f;
    public Material roadMaterial, groundMaterial, buildingMaterial;

    void Start(){ Build(); }

    void Build()
    {
        MakeCube("Ground",new Vector3(0,-.6f,0),new Vector3(260,1,260),groundMaterial);
        for(int x=-blocksX;x<=blocksX;x++)
            MakeCube("Road_NS",new Vector3(x*blockSize,0,0),new Vector3(roadWidth,.12f,260),roadMaterial);
        for(int z=-blocksZ;z<=blocksZ;z++)
            MakeCube("Road_EW",new Vector3(0,0,z*blockSize),new Vector3(260,.12f,roadWidth),roadMaterial);

        for(int x=-blocksX;x<blocksX;x++) for(int z=-blocksZ;z<blocksZ;z++)
        {
            Vector3 center=new Vector3(x*blockSize+blockSize/2f,0,z*blockSize+blockSize/2f);
            float h=Random.Range(8f,25f);
            MakeCube("Building",center+Vector3.up*h/2f,new Vector3(blockSize-roadWidth-5,h,blockSize-roadWidth-5),buildingMaterial);
        }
    }

    GameObject MakeCube(string n,Vector3 p,Vector3 s,Material m)
    {
        GameObject g=GameObject.CreatePrimitive(PrimitiveType.Cube);g.name=n;g.transform.SetParent(transform);g.transform.position=p;g.transform.localScale=s;
        if(m)g.GetComponent<Renderer>().material=m;return g;
    }
}
