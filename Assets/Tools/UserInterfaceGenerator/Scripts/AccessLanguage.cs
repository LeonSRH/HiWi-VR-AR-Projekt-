using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AccessLanguage : MonoBehaviour
{

    public TextMeshProUGUI text;

    private Language language;
    string chooseLanguage = "English";
    int localIndex = 0;
    string path = "C:/Users/SDA/Documents/HoloLens/SmartHospital/Smart Hospital (2)/Assets/UserInterfaceGenerator/Sprachen.txt";

    // Start is called before the first frame update
    void Start()
    {



       
   


        language = new Language(path, chooseLanguage, false);



        text.text = language.getString("app_name");
        Debug.Log("Value is " + language.getString("app_name"));




    }


    public void ChangeLanguageOnClick()
    {

        localIndex += 1;

        if (localIndex == 0)
        {
            chooseLanguage = "English";
        }
        if (localIndex == 1)
        {
            chooseLanguage = "French";

        }
        if (localIndex == 2)
        {
            chooseLanguage = "German";

        }
        if (localIndex == 3)
        {
            chooseLanguage = "Japanese";

        }
        if (localIndex == 4)
        {
            chooseLanguage = "Chinese";

        }
        if (localIndex == 5)
        {
            chooseLanguage = "Hungarian";

        }
        if (localIndex == 6)
        {
            chooseLanguage = "Portuguese";

        }
        if (localIndex == 7)
        {
            chooseLanguage = "Russian";

        }
        if (localIndex == 8)
        {
            chooseLanguage = "Thai";
            localIndex = 0;
        }


        language.setLanguage(path, chooseLanguage);
        text.text = language.getString("app_name");
        Debug.Log("Value is " + language.getString("app_name"));

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
