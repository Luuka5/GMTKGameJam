using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CheckPointData", order = 1)]
public class CheckPointData : ScriptableObject
{
    public string lastCheckPointName = "";
}