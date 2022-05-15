using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTriggerer : MonoBehaviour
{

    public bool active;
    private bool pointsAdded;
#pragma warning disable CS0108 // "ItemTriggerer.name" blendet den vererbten Member "Object.name" aus. Verwenden Sie das new-Schlüsselwort, wenn das Ausblenden vorgesehen war.
    public string name;
#pragma warning restore CS0108 // "ItemTriggerer.name" blendet den vererbten Member "Object.name" aus. Verwenden Sie das new-Schlüsselwort, wenn das Ausblenden vorgesehen war.

    private void Start()
    {
        active = true;
        pointsAdded = false;
    }

    public string getName()
    {
        return name;
    }

    public bool getActive()
    {
        return active;
    }
    public bool getpointsAdded()
    {
        return pointsAdded;
    }

    public void setActive(bool status)
    {
        active = status;
    }

    public void setpointsAdded(bool status)
    {
        pointsAdded = status;
    }


}
