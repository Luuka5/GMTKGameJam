using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class healthbarChanger : MonoBehaviour
{
    
    [SerializeField] PlayerData playerData;
    Slider slider;
    PlayerCore playerCore;
    // Update is called once per frame
    private void Start()
    {
        playerCore = playerData.playerCore;
    }
    void Update()
    {
        
    }
}
