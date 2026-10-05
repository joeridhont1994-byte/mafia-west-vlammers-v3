using UnityEngine;

public class VehicleEnterExit : MonoBehaviour
{
    public float range=3f;public ThirdPersonCamera cameraRig;GameObject player;ArcadeCar currentCar;
    void Update(){if(!CrossPlatformInput.InteractDown)return;if(currentCar){Exit();return;}player=GameObject.FindGameObjectWithTag("Player");if(!player)return;
        ArcadeCar[] cars=FindObjectsOfType<ArcadeCar>();float best=range;ArcadeCar nearest=null;
        foreach(var c in cars){float d=Vector3.Distance(player.transform.position,c.transform.position);if(d<best){best=d;nearest=c;}}if(nearest)Enter(nearest);}
    void Enter(ArcadeCar car){currentCar=car;car.driving=true;player.SetActive(false);if(cameraRig)cameraRig.target=car.transform;}
    void Exit(){player.transform.position=currentCar.transform.position+currentCar.transform.right*2.2f;player.SetActive(true);currentCar.driving=false;if(cameraRig)cameraRig.target=player.transform;currentCar=null;}
}
