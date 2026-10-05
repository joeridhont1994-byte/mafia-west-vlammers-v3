using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float dayLengthSeconds=300f;
    [Range(0,1)] public float time=.3f;

    void Update()
    {
        time=(time+Time.deltaTime/dayLengthSeconds)%1f;
        if(!sun)return;
        sun.transform.rotation=Quaternion.Euler(time*360f-90f,170f,0);
        float daylight=Mathf.Clamp01(Vector3.Dot(sun.transform.forward,Vector3.down));
        sun.intensity=Mathf.Lerp(.08f,1.1f,daylight);
        RenderSettings.ambientIntensity=Mathf.Lerp(.25f,1f,daylight);
    }
}
