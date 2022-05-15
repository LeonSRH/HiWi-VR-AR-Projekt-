using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NUI_UserManagement : MonoBehaviour
{


    FillPanel fillPanel;
    DelegateContainer.DelegateCallInt _function; // Test Funktion
    DelegateContainer.DelegateCallInt _LoadScene_OpeningScene;




    // Start is called before the first frame update
    void Start()
    {
        _LoadScene_OpeningScene += LoadOpeningScene;
        _function += Function;




        fillPanel = GameObject.FindObjectOfType<FillPanel>();

        Liste(1);

        CreateHeader();

    }


    void CreateHeader()
    {
        // HEADER

        float yPos = 505;

        // fillPanel.CreateImageOnUI(BackgroundColor, transform, 2500, 80, 0, yPos); // Background
        fillPanel.CreateHeaderOnUI("Berechtigungen", transform, 300, 65, -700, yPos);
        fillPanel.CreateButtonOnUI("Zurück", transform, _LoadScene_OpeningScene, 100, 50, 700, -yPos);

        /*
        Transform Parent = fillPanel.CreatePanel(300, 80, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Vertical, true);  // Hier braucht es keine anordnung der KindObjekte


        fillPanel.CreateButtonOnUI("Return", Parent, _LoadScene_OpeningScene, 100, 50, 0, 0);
        fillPanel.CreateButtonOnUI("Return", Parent, _LoadScene_OpeningScene, 100, 50, 0, 0);
        fillPanel.CreateButtonOnUI("Return", Parent, _LoadScene_OpeningScene, 100, 50, 0, 0);
        fillPanel.CreateButtonOnUI("Return", Parent, _LoadScene_OpeningScene, 100, 50, 0, 0);
        */

        //fillPanel.CreateImageOnUI(BackgroundColor, transform, 470, 75, 650, yPos); // Background
        // mainView.UserNameText = fillPanel.CreateTextElementOnUIWithHandle("Max Mustermann", transform, 200, 50, 550, yPos); // Name

    }


    //=================================================================================================================================
    //=================================================================================================================================
    public void Liste(int i)
    {

        Transform Parent = fillPanel.CreateParentObject(transform); // Hier braucht es eine anordnung der KindObjekte

        Parent.transform.localPosition = new Vector3(0, 0, 0);

        Transform Workers = fillPanel.CreatePanel(1400, 900, Parent, UI_Panel.enumForLayoutGroup.Horizontal, true);  // Hier braucht es keine anordnung der KindObjekte
        Workers.transform.localPosition = new Vector3(0, 0, 0);


        Transform LocalElement = fillPanel.CreatePanel(300, 600, Workers, UI_Panel.enumForLayoutGroup.Vertical, true);  // Hier braucht es keine anordnung der KindObjekte

        //fillPanel.CreateImageOnUI(BackgroundColor_White, LocalElement);

        fillPanel.CreateTextElementOnUI("PERSONAL", LocalElement);
        fillPanel.CreateTextElementOnUI("Mayer, Ewald", LocalElement);
        fillPanel.CreateTextElementOnUI("Mayer, Ewald", LocalElement);
        fillPanel.CreateTextElementOnUI("Mayer, Ewald", LocalElement);
        fillPanel.CreateTextElementOnUI("Mayer, Ewald", LocalElement);
        fillPanel.CreateTextElementOnUI("Mayer, Ewald", LocalElement);
        fillPanel.CreateTextElementOnUI("Mayer, Ewald", LocalElement);



        string[] Zugang = { "Navigation", "Personal", "Raumplanung", "Admin", "Inventar", "Devices", "Tracking", "WMS" };

        for (int z = 0; z < 8; z++)
        {

            Transform LocalElementMitarbeiter = fillPanel.CreatePanel(300, 600, Workers, UI_Panel.enumForLayoutGroup.Vertical, true);  // Hier braucht es keine anordnung der KindObjekte
                                                                                                                                       //fillPanel.CreateButtonOnUI("Zeige Mitarbeiter", LocalElementMitarbeiter, _function);

            fillPanel.CreateTextElementOnUI(Zugang[z], LocalElementMitarbeiter);
            fillPanel.CreateTextElementOnUI("Y", LocalElementMitarbeiter);
            fillPanel.CreateTextElementOnUI("N", LocalElementMitarbeiter);
            fillPanel.CreateTextElementOnUI("Y", LocalElementMitarbeiter);
            fillPanel.CreateTextElementOnUI("Y", LocalElementMitarbeiter);
            fillPanel.CreateTextElementOnUI("Zeige Mitarbeiter", LocalElementMitarbeiter);
            fillPanel.CreateTextElementOnUI("Zeige Mitarbeiter", LocalElementMitarbeiter);
        }


        /*

        Transform LocalElement2 = fillPanel.CreatePanel(300, 600, Workers, UI_Panel.enumForLayoutGroup.Vertical, true);  // Hier braucht es keine anordnung der KindObjekte

        fillPanel.CreateButtonOnUI("Zeige Mitarbeiter", LocalElement2, _function);
        fillPanel.CreateButtonOnUI("Zeige Mitarbeiter", LocalElement2, _function);
        fillPanel.CreateButtonOnUI("Zeige Mitarbeiter", LocalElement2, _function);
        fillPanel.CreateButtonOnUI("Zeige Mitarbeiter", LocalElement2, _function);
        */

    }


    public void LoadOpeningScene(int i)
    {
        SceneManager.LoadScene("OpeningScene");

    }

    void Function(int i)
    {

    }

}
