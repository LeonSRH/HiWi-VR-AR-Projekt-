using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEditor_ControllerForCreation : MonoBehaviour
{


    GroundPlacementControler placer;

    // Start is called before the first frame update
    void Start()
    {

        placer = FindObjectOfType<GroundPlacementControler>();




        //placer.CreateBuilding(1);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
