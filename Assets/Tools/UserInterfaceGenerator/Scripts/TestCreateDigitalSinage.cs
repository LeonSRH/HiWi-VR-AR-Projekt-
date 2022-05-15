using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreateDigitalSinage : MonoBehaviour
{

    FillPanel fillPanel;

    //================================
    // 1) Create Panel(P1) with certain Size
    // 2) Create Child Object with certain Size and values und set P1 as a Parent-Object
    // 3) Set local Position (Inside Panel)

    // Start is called before the first frame update
    void Start()
    {

        fillPanel = GetComponent<FillPanel>();



        //fillPanel.CreatePanel()

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
