using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable
{
    bool GetDestroyedState();
    void Destroy();

    void Respawn();
}
