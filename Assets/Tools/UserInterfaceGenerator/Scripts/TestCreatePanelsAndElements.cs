using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;



//=============================================================================================================================
//=============================================================================================================================
// AUTHOR: Sebastian de Andrade ( Herr der Chiffren)
// DATE:    22.3.19

// DESCRP: This is a Test class for testing and loging the menu functionality
// TODO:   LOG of Test

    // HISTORY:
//=============================================================================================================================
//=============================================================================================================================

public class TestCreatePanelsAndElements : MonoBehaviour
{
    public Sprite TestSprite;

    FillPanel fillPanel;
    DelegateContainer.DelegateCallInt _function;


    void Function(int i)
    {

    }

    // Start is called before the first frame update
    void Start()
    {

        _function += Function;

        fillPanel = GameObject.FindObjectOfType<FillPanel>();


        Transform rightPanel = fillPanel.CreatePanel(150.0f, 500.0f, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Grid, true);

        fillPanel.SetSettingsForLayoutGrid(rightPanel,100.0f,50.0f);

        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateImageOnUI(TestSprite, rightPanel);
        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateImageOnUI(TestSprite, rightPanel);
        fillPanel.CreateImageOnUI(TestSprite, rightPanel);
       
        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateTextElementOnUI("bla", rightPanel);
        fillPanel.CreateImageOnUI(TestSprite, rightPanel);



        Transform leftPanel = fillPanel.CreatePanel(150.0f, 500.0f, UI_OrientationOnScreen.enumForOrientations.Left, UI_Panel.enumForLayoutGroup.Grid, true);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);
        fillPanel.CreateTextElementOnUI("bla", leftPanel);


        /*
        //====================================================================================== Create Panel on left hand side
        Transform newPanel = fillPanel.CreatePanel(800, 50, UI_OrientationOnScreen.enumForOrientations.TopSmall, UI_Panel.enumForLayoutGroup.Horizontal, false);

        //====================================================================================== Create Header
        fillPanel.CreateHeaderOnUI("Smart Hospital", newPanel);

        //====================================================================================== Create Button
        //fillPanel.CreateButtonOnUI("OusideAction", newPanel, _function);

        //====================================================================================== Create Text
        fillPanel.CreateTextElementOnUI("Heidelberg", newPanel);
        */

        //StartCoroutine(TestPhase());      // START TEST HERE AND CALL REKURSIV START METHOD


    }




    //====================================================================================
    //====================================================================================
    // CREATES A RANDOM UI ELEMENT

    void CreateRandomElement(string _name, Transform _parent)
    {
        

        int rnd = Random.Range(0, 5 );

        if (rnd == 0)
            fillPanel.CreateTextElementOnUI(_name.ToString(), _parent);
        else if (rnd == 1)
            fillPanel.CreateInputFieldOnUI(_name, _parent);
        else if (rnd == 2)
            fillPanel.CreateHeaderOnUI(_name, _parent);
        else if (rnd == 3)
            fillPanel.CreateButtonOnUI(_name, _parent, _function);
        else
            fillPanel.CreateImageOnUI(TestSprite, _parent);


    }
    
    //====================================================================================
    //====================================================================================
    // TESTPHASE NUMBER 1

    IEnumerator TestPhase()
    {


        yield return new WaitForSeconds(0.25f);

        int rnd_X = Random.Range(400, 1920);
        int rnd_Y = Random.Range(400, 1920);

        Transform newPanel = fillPanel.CreatePanel(rnd_X,rnd_Y, GetRandomOrientation(), GetRandomLayOutGroup(), true);

        yield return new WaitForSeconds(0.25f);

        Transform newPanel2 = fillPanel.CreatePanel(rnd_X/2, rnd_Y/2, GetRandomOrientation(), GetRandomLayOutGroup(), true);



        int rnd_1 = Random.Range(1, 10);
        int rnd_2 = Random.Range(1, 10);


        for (int i = 0; i < rnd_2; i++)
        {
            CreateRandomElement("A" + i.ToString(), newPanel2);

            //fillPanel.CreateTextElementOnUI("Admin " + i.ToString(), newPanel2);
            yield return new WaitForSeconds(0.1f);

        }



        for (int i = 0; i < rnd_1; i++)
        {

            CreateRandomElement("B" + i.ToString(), newPanel);

            yield return new WaitForSeconds(0.1f);

        }


        yield return new WaitForSeconds(1.0f);

        fillPanel.ClearUserInterface();
        Start();

        //StartCoroutine(TestPhase());

    }

    //====================================================================================
    //====================================================================================
    // RANDOM LAYOUTGROUP

    UI_Panel.enumForLayoutGroup GetRandomLayOutGroup()
    {
        // Returns random LayoutGroup

        UI_Panel.enumForLayoutGroup rndLayout;

        int rnd = Random.Range(0, 3);

        if (rnd == 0)
            rndLayout = UI_Panel.enumForLayoutGroup.Vertical;
        else if (rnd == 1)
            rndLayout = UI_Panel.enumForLayoutGroup.Horizontal;
        else if (rnd == 2)
            rndLayout = UI_Panel.enumForLayoutGroup.Vertical;
        else
            rndLayout = UI_Panel.enumForLayoutGroup.Horizontal;

        return rndLayout;

    }

    //====================================================================================
    //====================================================================================
    // RANDOM ORIENTATION

    UI_OrientationOnScreen.enumForOrientations GetRandomOrientation()
    {
        UI_OrientationOnScreen.enumForOrientations rndOrientation;

        int rnd = Random.Range(0, 3);

        if (rnd == 0)
            rndOrientation = UI_OrientationOnScreen.enumForOrientations.Bottom;
        else if (rnd == 1)
            rndOrientation = UI_OrientationOnScreen.enumForOrientations.Left;
        else if (rnd == 2)
            rndOrientation = UI_OrientationOnScreen.enumForOrientations.Right;
        else
            rndOrientation = UI_OrientationOnScreen.enumForOrientations.Left;

        return rndOrientation;

    }
}
