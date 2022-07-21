using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings", order = 2)]

public class EnemySettings : ScriptableObject
{
   public float damageTreasHold;
   public  PlayerData playerData;
   public float damagePauseTime = 0.2f;
   public float timeToDespawn;
   public float distanceToDespawn;
}
