using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateBuilding : MonoBehaviour
{


    GroundPlacementControler groundPlacer;
    SaveAndLoadFromFile saveAndLoadFromFile;



    // Start is called before the first frame update
    void Start()
    {


        saveAndLoadFromFile = GameObject.FindObjectOfType<SaveAndLoadFromFile>();
        groundPlacer = GameObject.FindObjectOfType<GroundPlacementControler>();

        string loadedData = saveAndLoadFromFile.LoadEntitiesFromTXT(@"D:\MyTest.txt");

        loadedData = ClearAllGaps(loadedData);

        Debug.Log("loadedDate: " + loadedData);

        BuildBasedOnString(loadedData);
        //BuildBasedOnString("1%0%0%0");
        //BuildBasedOnString("2%0%0%0");
        //BuildBasedOnString("3%0%0%0");
        //BuildBasedOnString("4%0%0%0");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            // groundPlacer.CreateObjectExternal(0, new Vector3(Random.Range(-10.0f,10.0f), 0, 0));
            /*groundPlacer.CreateObjectExternal(0 , new Vector3(1, 0, 0));
            groundPlacer.CreateObjectExternal(1, new Vector3(2, 0, 0));
            groundPlacer.CreateObjectExternal(0, new Vector3(3, 0, 0));
            groundPlacer.CreateObjectExternal(0, new Vector3(4, 0, 0));
            groundPlacer.CreateObjectExternal(0, new Vector3(5, 0, 0));*/
            string loadedData = saveAndLoadFromFile.LoadEntitiesFromTXT(@"D:\MyTest.txt");
            BuildBasedOnString(loadedData);

            /*BuildBasedOnString("1%2%3%0");
            BuildBasedOnString("1%0%0%0");
            BuildBasedOnString("2%0%0%0");
            BuildBasedOnString("3%0%0%0");
            BuildBasedOnString("4%0%0%0");*/
        }
        
    }



    void BuildBasedOnString(string _command)
    {

        _command = ClearAllGaps(_command);

        string [] splitted = _command.Split(new string[] { "\r\n", "\n", "%","?" },StringSplitOptions.RemoveEmptyEntries);

        Debug.Log("Okay: " + splitted[0] + splitted[1] + splitted[2]);

        float _pos_x = float.Parse(@splitted[0]);
        float _pos_y = float.Parse(@splitted[1]);
        float _pos_z = float.Parse(@splitted[2]);



        int _type = 0;//int.Parse(@splitted[3]);

        Debug.Log("NichOkay: " + splitted[0] + splitted[1] + splitted[2]);

        groundPlacer.CreateObjectExternal(_type, new Vector3(_pos_x , _pos_y, _pos_z));

        /*
        for (int i = 0; i < splitted.Length; i++)
        {

            Debug.Log(i.ToString() + " Split: " + splitted[i]);

        }
        */



    }



    char[] StringToCharArray(string _text)
    {
        char[] charArr = _text.ToCharArray();
        return charArr;
    }





    string ClearAllGaps(string _text)
    {

        string cleanedText = "";
        char[] charText = new char[_text.Length];
        char[] cleanedCharText = new char[_text.Length];

        char CheckFor = ' ';
        charText = StringToCharArray(_text);
        
        foreach (char ch in charText)
        {

            if (ch.CompareTo(CheckFor) == 0)    // Wenn ein Space gefunden wurde, tue nichts. Wenn ein Buchstabe gefunden, dass füge ihn zu cleanedText hinzu.
            {

            }
            else
            {

                cleanedText += ch.ToString();

            }
        }


        return cleanedText;

    }



}
