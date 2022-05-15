using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptFromOutside : MonoBehaviour
{
    FillPanel fillPanel;
    public UnityAction localFunction;

    public DelegateContainer.DelegateCallInt _function;



    // Start is called before the first frame update
    void Start()
    {


     //   localFunction += TextFunction;

        fillPanel = GameObject.FindObjectOfType<FillPanel>();



        _function += LoadUnityScene;



        /// Create User Interface
        /// Everything works like this:
        /// 1) Every Outside Scene is loaded by "Sceneloader". It is always loaded in addition to the real "starting scene ( which contains the main Canvas with the main Menu)
        /// 2) After loading - start funktion is called and creates a new UI for player. (ADD HERE: Security - just display the ui elements that fit to the users permissions);
        /// 3) Via delegate funktion every funktion is linked correctly to the execution script ( kind of Dependency injection)
        /// 4) The "On Destroy" event clears the canvas of all not important UI Elements.






        //====================================================================================== Create Panel on left hand side
        Transform newPanel = fillPanel.CreatePanel(300,200, UI_OrientationOnScreen.enumForOrientations.Left, UI_Panel.enumForLayoutGroup.Vertical,true);



        //====================================================================================== Create Header
        fillPanel.CreateHeaderOnUI("Smart Hospital",newPanel);

        //====================================================================================== Create Button
        fillPanel.CreateButtonOnUI("OusideAction", newPanel, _function);

        //====================================================================================== Create Text
        fillPanel.CreateTextElementOnUI("OtherText", newPanel);

        //====================================================================================== Create DropDown
        string[] dropDownElements = { "First", "Second", "Third" };
        fillPanel.CreateDropDownOnUI("OtherDown", newPanel, dropDownElements, _function);


        /* 

         fillPanel.CreateTextElementOnUI("oben", newPanel2);
         fillPanel.CreateTextElementOnUI("boben", newPanel2);

         fillPanel.CreateTextElementOnUI("droben", newPanel2);

         string[] dropDownElements = { "First", "Second", "Third" };
         fillPanel.CreateDropDownOnUI("Load", newPanel2, dropDownElements, _function);
         */

        //Wenn S





    }


    

    private void OnDestroy()
    {
        /// Every Loadable component needs an "OnDestroy" method for clearing the
        /// Canvas of the viewer. Otherwise we would get a mess full of not linked buttons etc.


        Debug.Log("OnDestroy");
        //fillPanel.ClearUserInterface();


    }

    private void OnDisable()
    {

        Debug.Log("OnDisable");
        //fillPanel.ClearUserInterface();

    }


    private void OnRectTransformRemoved()
    {
        Debug.Log("OnRectTransfomredRemoved");
    }


    void LoadUnityScene(int _nr)
    {

        Debug.Log("Grööhl From Outside: " + "" + _nr);


    }
}
