using UnityEngine;

public class VehicleInteraction : MonoBehaviour
{
    public ThirdPersonPlayer player;
    public ThirdPersonCamera followCamera;
    public ArcadeCar car;
    public Transform driverSeat;
    public Transform exitPoint;
    public float enterDistance=3f;
    bool inside;

    void Update()
    {
        if(!player || !car) return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!inside && Vector3.Distance(player.transform.position,car.transform.position)<=enterDistance) Enter();
            else if(inside) Exit();
        }
    }

    void Enter()
    {
        inside=true; car.driving=true;
        player.gameObject.SetActive(false);
        if(followCamera) followCamera.target=car.transform;
    }

    void Exit()
    {
        inside=false; car.driving=false;
        player.transform.position=exitPoint?exitPoint.position:car.transform.position+car.transform.right*2f;
        player.gameObject.SetActive(true);
        if(followCamera) followCamera.target=player.transform;
    }
}
