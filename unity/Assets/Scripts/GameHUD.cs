using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public WantedSystem wanted;
    public MissionSystem missions;
    public Text wantedText,missionText;

    void Start(){if(wanted)wanted.OnWantedChanged+=DrawWanted;}
    void Update()
    {
        if(missionText&&missions)missionText.text=missions.stage==MissionSystem.Stage.GoToPickup?"Ga naar het pakket":missions.stage==MissionSystem.Stage.ReturnToHQ?"Breng het pakket naar Mafia HQ":missions.stage==MissionSystem.Stage.Complete?"Missie voltooid":"Vrij rondrijden";
    }
    void DrawWanted(int level){if(wantedText)wantedText.text=new string('★',level)+new string('☆',5-level);}
}
