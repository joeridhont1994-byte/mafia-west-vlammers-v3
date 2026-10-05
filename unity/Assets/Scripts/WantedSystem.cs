using UnityEngine;
using System;

public class WantedSystem : MonoBehaviour
{
    [Range(0,5)] public int wantedLevel;
    public float decayDelay=25f;
    float lastCrime;
    public event Action<int> OnWantedChanged;

    public void AddWanted(int amount=1)
    {
        wantedLevel=Mathf.Clamp(wantedLevel+amount,0,5);
        lastCrime=Time.time; OnWantedChanged?.Invoke(wantedLevel);
    }

    void Update()
    {
        if(wantedLevel>0 && Time.time-lastCrime>decayDelay)
        { wantedLevel--; lastCrime=Time.time; OnWantedChanged?.Invoke(wantedLevel); }
    }
}
