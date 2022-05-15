using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

//=======================================================================================================================================
//=======================================================================================================================================
// AUTHOR: Sebastian de Andrade ( Herr der Chiffren)
// DATE: 29.3.2019

// The Console Executes Orders based on an string input

//=======================================================================================================================================
//=======================================================================================================================================


public class ConsoleUserInterfaceGenerator : MonoBehaviour
{

    public Sprite TestSprite;           // Just a Sprite for testing purpose - later on write it directly to the image you want to have displayed
    public FillPanel fillPanel;         // For creating commands
    public TMP_InputField inputField;   // The Input field of the console


    // How to create something
#pragma warning disable CS0414 // Dem Feld "ConsoleUserInterfaceGenerator.HelpText" wurde ein Wert zugewiesen, der aber nie verwendet wird.
    string HelpText = "CREATE_PANEL_LEFT_GRID:TEXT_NAME1:TEXT_NAME2:IMAGE";
#pragma warning restore CS0414 // Dem Feld "ConsoleUserInterfaceGenerator.HelpText" wurde ein Wert zugewiesen, der aber nie verwendet wird.

    //==================================================
    // 1) CREATE _ PANEL _ LEFT _ GRID:         
    // 2) :TEXT_NAME2
    // 3) :TEXT_NAME3
    // 4) :Image
    //==================================================



    // Start is called before the first frame update
    void Start()
    {
        fillPanel = GameObject.FindObjectOfType<FillPanel>();
    }



    public void ConsoleInput_Execute()
    {
        // Execute console Input based on string
        string command = inputField.text;

        //TODO:
        // CHECK if value is valid, if not, show the invalid command
        // suggest the right command

        // Execute the Build command
        CreateUIBasedOnString(command);

        Debug.Log("Create:" + "" + command);
    }

    public void ConsoleInput_Clear()
    {
        // Clears the Input field ( call it via button)

        inputField.text = "";
    }





    UI_OrientationOnScreen.enumForOrientations GetOrientation(string _command)
    {
        UI_OrientationOnScreen.enumForOrientations orientation;


        if (_command == "LEFT")
        {
            orientation = UI_OrientationOnScreen.enumForOrientations.Left;
        }
        else if (_command == "RIGHT")
        {
            orientation = UI_OrientationOnScreen.enumForOrientations.Right;

        }
        else if (_command == "TOP")
        {
            orientation = UI_OrientationOnScreen.enumForOrientations.Top;


        }
        else if (_command == "BOTTOM")
        {
            orientation = UI_OrientationOnScreen.enumForOrientations.Bottom;

        }
        else
        {

            Debug.Log(" No Valid Orientation");
            orientation = UI_OrientationOnScreen.enumForOrientations.Left;

        }


        return orientation;
    }

    UI_Panel.enumForLayoutGroup GetLayoutGroup(string _command)
    {
        UI_Panel.enumForLayoutGroup layoutGroup;


        if (_command == "GRID")
        {

            layoutGroup = UI_Panel.enumForLayoutGroup.Grid;
        }
        else if (_command == "HORIZONTAL")
        {
            layoutGroup = UI_Panel.enumForLayoutGroup.Horizontal;
        }
        else if (_command == "VERTICAL")
        {
            layoutGroup = UI_Panel.enumForLayoutGroup.Vertical;
        }
        else
        {

            Debug.Log(" Invalid LayoutGroup");
            layoutGroup = UI_Panel.enumForLayoutGroup.Grid;


        }


        return layoutGroup;


    }

    void CreateAddition(string _command, Transform _parent)
    {
        string CheckFor = "_";

        string[] singleCommands = Regex.Split(_command, CheckFor);

        for (int i = 0; i < singleCommands.Length; i++)         // To Upper
        {
            singleCommands[i] = singleCommands[i].ToUpper();
        }

        if (singleCommands[0] == "TEXT")
        {
            string textName = "Name";

            if (singleCommands[1] != null)
                textName = singleCommands[1];

            fillPanel.CreateTextElementOnUI(textName, _parent);
        }

        if (singleCommands[0] == "IMAGE")
        {
            fillPanel.CreateImageOnUI(TestSprite, _parent);


        }
    }

    void CreateUIBasedOnString(string _command)
    {

        string command = _command;
        string CheckFor = "_";
        string SplitCreatiionFromAddition = ":";



        // FROM == "create _ panel_ LEft _ grid    : Text_blupp"
        // TO   == "create_panel_LEft_grid:Text_blupp"
        command = Regex.Replace(command, @"\s", "");    // Clear empty gaps

        //=======================================================================================================
        string[] PanelAndAdditions = Regex.Split(command, SplitCreatiionFromAddition);
        command = PanelAndAdditions[0]; // Set First Elemnt as Father Element ( others are Childs)
                                        //=======================================================================================================

        // TO   == command => "create_panel_LEft_grid"
        //      == PannelAndAddition => "Text_blupp"

        string[] singleCommands = Regex.Split(command, CheckFor);

        //Split first input into each single command: => Split at "_" Symbol

        // FROM == "create_panel_LEft_grid"
        // TO:  singelCommands[0] => "create"
        //      singelCommands[1] => "panel"
        //      singelCommands[2] => "LEft"
        //      singelCommands[3] => "grid"

        // Now Input could be upper or lower case: Lets force each character to Upper Case

        for (int i = 0; i < singleCommands.Length; i++)
        {
            singleCommands[i] = singleCommands[i].ToUpper();
        }

        // Then we get the following change:

        // FROM:singelCommands[0] => "create"
        //      singelCommands[1] => "panel"
        //      singelCommands[2] => "LEft"
        //      singelCommands[3] => "grid"

        // TO:  singelCommands[0] => "CREATE"
        //      singelCommands[1] => "PANEL"
        //      singelCommands[2] => "LEFT"
        //      singelCommands[3] => "GRID"


        //========================================================================================================================
        //PARSE ACTIONS AN EXECUTE COMMANDS
        //========================================================================================================================
        // DESTROY
        //** Add HERE A STRING ARRAY WITH ALL POSSIBLE COMMANDS
        if (singleCommands[0] == "DESTROY" || singleCommands[0] == "DELETE")
        {
            UI_OrientationOnScreen.enumForOrientations orientaion = GetOrientation(singleCommands[1]);
            fillPanel.ClearScreenGeography(orientaion);
        }

        //========================================================================================================================
        // CREATE
        if (singleCommands[0] == "CREATE")
        {
            Debug.Log("CR YES");

            if (singleCommands[1] == "PANEL")    // Create Panel
            {
                UI_OrientationOnScreen.enumForOrientations orientation = GetOrientation(singleCommands[2]); // Get the orientation
                UI_Panel.enumForLayoutGroup layoutgroup = GetLayoutGroup(singleCommands[2]);    // Get the Layoutgroup

                // Create The Panel and save it into a transform ( handle). We need this for adding the children 
                Transform panelClone = fillPanel.CreatePanel(100, 100, orientation, UI_Panel.enumForLayoutGroup.Grid, true);

                //==== ADD Child When needed
                if (PanelAndAdditions[1] != null)
                {
                    // For each additive Element call CREATEADDITION()
                    for (int i = 1; i < PanelAndAdditions.Length; i++)
                    {
                        Debug.Log(PanelAndAdditions[i]);
                        CreateAddition(PanelAndAdditions[i], panelClone);
                    }
                }
            }

            if (singleCommands[1] == "B")    // Button
            {


            }

        }
    }



}
