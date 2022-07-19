using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class healthBatChanger : MonoBehaviour
{

    [SerializeField] PlayerData playerData;
    [SerializeField]Slider slider;
    PlayerCore playerCore;
    // Update is called once per frame
    private void Start()
    {
        playerCore = playerData.playerCore;
    }
    void Update()
    {
        slider.value = playerCore._health;
    }
}
