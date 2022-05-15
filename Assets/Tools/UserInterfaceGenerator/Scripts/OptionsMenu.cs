using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{





    FillPanel fillPanel;


    // Start is called before the first frame update
    void Start()
    {
        fillPanel = GameObject.FindObjectOfType<FillPanel>();




        //====================================================================================== Create Panel on left hand side
        Transform newPanel = fillPanel.CreatePanel(800, 50, UI_OrientationOnScreen.enumForOrientations.TopSmall, UI_Panel.enumForLayoutGroup.Horizontal, false);



        //====================================================================================== Create Header
        fillPanel.CreateHeaderOnUI("Smart Hospital", newPanel);

        //====================================================================================== Create Button
        //fillPanel.CreateButtonOnUI("OusideAction", newPanel, _function);

        //====================================================================================== Create Text
        fillPanel.CreateTextElementOnUI("Heidelberg", newPanel);
        fillPanel.CreateTextElementOnUI("3.20.19", newPanel);
        fillPanel.CreateTextElementOnUI("Chirurgie", newPanel);
        fillPanel.CreateTextElementOnUI("Admin", newPanel);
        fillPanel.CreateTextElementOnUI("Admin", newPanel);

        fillPanel.CreateTextElementOnUI("Admin", newPanel);

        fillPanel.CreateTextElementOnUI("Admin", newPanel);

        fillPanel.CreateTextElementOnUI("Admin", newPanel);


        //====================================================================================== Create DropDown
        //string[] dropDownElements = { "First", "Second", "Third" };
        //fillPanel.CreateDropDownOnUI("OtherDown", newPanel, dropDownElements, _function);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
