using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Author: SDA August 2019
/// </summary>
public class Messanger : MonoBehaviour
{


    /// <summary>
    /// Messanger centralises the Text Output in Screen.
    /// </summary>

    public TextMeshProUGUI textField;


    float DisplayTime;
    bool displayingMessage;


    // Start is called before the first frame update
    void Start()
    {

        DisplayMessage("Hallo Welt 10 Sek", 10.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (displayingMessage)
        {
            if (Time.time > DisplayTime)
            {

                textField.text = "";
                displayingMessage = false;

            }
        }
    }


    public void DisplayMessage(string _text, float _time)
    {
        textField.text = _text;
        DisplayTime = Time.time + _time;
        displayingMessage = true;

    }


}
