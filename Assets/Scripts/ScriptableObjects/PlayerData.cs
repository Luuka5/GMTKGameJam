using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{ 
   public PlayerCore playerCore;
    public int startHealth;
    public int healFormCrossSide;
}
