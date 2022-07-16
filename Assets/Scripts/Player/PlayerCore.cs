using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    PlayerData playerData;

    private void Awake()
    {
        playerData.playerCore = this;
    }
}
