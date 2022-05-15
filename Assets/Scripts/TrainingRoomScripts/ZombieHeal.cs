using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeal : MonoBehaviour
{
    private bool healed = false;


    public bool getHealed()
    {
        return healed;
    }

    public void setHealed(bool status)
    {
        healed = status;
    }
}
