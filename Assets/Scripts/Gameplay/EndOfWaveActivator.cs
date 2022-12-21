using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfWaveActivator : Wave
{
    [Header("Sorry A Lot Of Unnesesarry things here. Just a gamejam project, tho")]
    [SerializeReference] public GameObject[] _thingsToActivate;

     public override void Activate()
    {
        foreach (GameObject _thing in _thingsToActivate)
        {
            IActivatable _thingToActivate = _thing.GetComponent<IActivatable>();
            if (_thingToActivate!=null)
            { _thingToActivate.Activate();
               // Debug.Log("Trying to Activate " + _thing);
            }
            else
            {
                Debug.Log("Can't activate "+_thing+". I has no IActivatable.");
            }
        }
    }
}
