using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceCore : MonoBehaviour
{
   public int ActiveDiceSide = 1;
    private GravityGun _gravityGun;
   public  PlayerData playerData;
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
        ActiveDiceSide = GetCurrentDiceSide();
   }

   public void OnRelease()
   {
        
   }

}
