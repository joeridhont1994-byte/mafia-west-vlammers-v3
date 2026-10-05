using UnityEngine;

[CreateAssetMenu(menuName="Mafia West-Vlammers/Vehicle")]
public class VehicleData : ScriptableObject
{
    public string vehicleName="Street Car";
    public int price=25000;
    public float topSpeed=22f;
    public float acceleration=22f;
    public GameObject prefab;
}
