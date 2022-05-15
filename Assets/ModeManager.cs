using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{

    enum GameModes { Build, Walk};
    GameModes currentMode = GameModes.Build;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        switch (currentMode)
        {

            case GameModes.Build:

                break;

            case GameModes.Walk:
                break;

            default:

                break;
        }


        


        
    }
}
