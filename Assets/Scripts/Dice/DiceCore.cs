using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceCore : MonoBehaviour
{
   public int ActiveDiceSide = 1;
    private GravityGun _gravityGun;
   public  PlayerData playerData;
   [SerializeField] private EnemySettings enemySettings;
    private GrabThrowObject grabThrowObject;

    private void Awake()
    {
        grabThrowObject = GetComponent<GrabThrowObject>();
    }
    public void OnKill()
    { grabThrowObject.OnKill(); }

    public int GetCurrentDiceSide()
    {
        Transform DiceTransform = GetComponent<Transform>();
        Vector3[] rotationVectors = new Vector3[6];
        rotationVectors[2] = DiceTransform.forward;
        rotationVectors[1] = DiceTransform.right;
        rotationVectors[0] = DiceTransform.up;

        float highest = 0;
        int side = -1;

        for (int i = 0; i < rotationVectors.Length; i++)
        {
            if (rotationVectors[i].y > highest)
            {
                highest = rotationVectors[i].y;
                side = i + 1;
            }
            if (-rotationVectors[i].y > highest)
            {
                highest = -rotationVectors[i].y;
                side = 6 - i;
            }
        }

        return side;
    }

   public void OnGrab()
   {
        //IF Player has low HP more chance of cross side
        if (playerData.playerCore._health < playerData.healthTreasHoldForCrossSide)
            if (Random.Range(0, playerData.healthTreasHoldForCrossSide) > playerData.playerCore._health*1.5f)
            { ActiveDiceSide = 3; return; }
       

            

        ActiveDiceSide = GetCurrentDiceSide();

        if (SideToNumber(ActiveDiceSide) == 0) return;


        List<EnemyCore> _enemyList = enemySettings.enemySpawner.enemyList;
        if (_enemyList.Count == 0) return;

      
        int diceNumber = SideToNumber(ActiveDiceSide);

        //Check if enemy with side color exists
        foreach (EnemyCore enemy in _enemyList)
            if (enemy.enemyNumber == diceNumber) return;


        //If no enemy with side color -> Take random color from enemyList
        int newDiceNumber = _enemyList[Random.Range(0, _enemyList.Count)].enemyNumber;
        ActiveDiceSide = NumberToSide(newDiceNumber);
   }

   public void OnRelease()
   {
        
   }

   int SideToNumber(int side)
    {

        switch (side)
        {
            case 1:
                return 3;
            case 6:
                return 3;
            case 2:
                return 2;
            case 5:
                return 2;
            case 4:
                return 1; 
            case 3: return 0;

            default: return -1;
        }
    }

    int NumberToSide(int number)
    {
        switch(number)
        {
            case 0: return 3;
            case 1: return 4;
            case 2:
                if (Random.Range(0, 1) == 0) return 2;
                    return 5;
            case 3:
                if (Random.Range(0, 1) == 0) return 1;
                return 6;
            default: return -1;
        }
    }

}
