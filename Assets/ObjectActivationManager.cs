using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Author: SDA August 2019.
/// </summary>

public class ObjectActivationManager : MonoBehaviour
{
    public bool isActivated = false;
    public ObjectPlacementChecker objectPlacementChecker;
    public bool AllowPlacement = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectPlacementChecker != null)
        AllowPlacement = !objectPlacementChecker.CollisionWithOtherBuilding;        
    }


    public void SetObjectIntoWorld()
    {
        isActivated = true; // Start Behavior of the object. or better. Other script are waiting until this boolean is true.

        if (objectPlacementChecker != null)
            objectPlacementChecker.ResetToOldMaterial();

    }

}
