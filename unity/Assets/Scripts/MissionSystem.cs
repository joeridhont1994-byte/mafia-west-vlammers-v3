using UnityEngine;
using System;

public class MissionSystem : MonoBehaviour
{
    public enum Stage{None,GoToPickup,ReturnToHQ,Complete}
    public Stage stage;
    public Transform player,pickup,hq;
    public float reachDistance=4f;
    public int rewardMoney=5000,rewardXP=250;
    public event Action<int,int> OnReward;

    public void StartDelivery(){stage=Stage.GoToPickup;}
    void Update()
    {
        if(!player)return;
        if(stage==Stage.GoToPickup&&pickup&&Vector3.Distance(player.position,pickup.position)<reachDistance)stage=Stage.ReturnToHQ;
        else if(stage==Stage.ReturnToHQ&&hq&&Vector3.Distance(player.position,hq.position)<reachDistance){stage=Stage.Complete;OnReward?.Invoke(rewardMoney,rewardXP);Debug.Log("Mission complete");}
    }
}
