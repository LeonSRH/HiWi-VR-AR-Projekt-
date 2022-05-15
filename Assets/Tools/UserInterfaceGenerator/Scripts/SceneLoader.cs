using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{

    FillPanel fillPanel;
#pragma warning disable CS0414 // Dem Feld "SceneLoader.lastLoadedScene" wurde ein Wert zugewiesen, der aber nie verwendet wird.
    int lastLoadedScene = 0;
#pragma warning restore CS0414 // Dem Feld "SceneLoader.lastLoadedScene" wurde ein Wert zugewiesen, der aber nie verwendet wird.


    [Header("Only 1 Scene, Per Element")]
    public SceneToLoad[] sceneToLoadArray;


    // Load Delegates must be linked with real loading Method
    DelegateContainer.DelegateCallString    _load_3DModell, 
                                            _load_highscore, 
                                            _load_raumBuch, 
                                            _load_Rundgang;

#pragma warning disable CS0414 // Dem Feld "SceneLoader.SceneHasLoaded" wurde ein Wert zugewiesen, der aber nie verwendet wird.
    bool SceneHasLoaded;
#pragma warning restore CS0414 // Dem Feld "SceneLoader.SceneHasLoaded" wurde ein Wert zugewiesen, der aber nie verwendet wird.

    
    void Start()
    {

        SceneHasLoaded = false;


        sceneToLoadArray = new SceneToLoad[1];
        sceneToLoadArray[0] = Resources.Load<SceneToLoad>("SceneToLoad");
        
        fillPanel = GameObject.FindObjectOfType<FillPanel>();

        
        for (int i = 0; i < sceneToLoadArray.Length; i++)
        {
            sceneToLoadArray[i].isLoaded = false;               // Init the isloaded flag og the Scriptable object.
        }



        // Create horizontal Panel for all Buttons
        Transform panel = fillPanel.CreatePanel(800, 50, UI_OrientationOnScreen.enumForOrientations.Top, UI_Panel.enumForLayoutGroup.Horizontal,false);


        for (int i = 0; i < sceneToLoadArray.Length; i++)
        {
            sceneToLoadArray[i].loadMethod += LoadUnityScene;                                   // Link Method

            string sceneName = "scenenName";
            
            
            if (sceneToLoadArray[i].NameOfScene != null)
            sceneName = sceneToLoadArray[i].NameOfScene;
           
            if (sceneName.Length == 0)
            sceneName = "Bla";
            
            fillPanel.CreateLoadButtonOnUI(sceneName, panel, sceneToLoadArray[i].loadMethod, sceneName);     // Create Button
        }
    }



    SceneToLoad LinkLoadMethod(SceneToLoad _sceneToLoad)
    {

        SceneToLoad sceneToLoad = _sceneToLoad;

        
        return sceneToLoad;
        
    }
    

    void LoadUnityScene(string _levelName)
    {
            for( int i = 0; i < sceneToLoadArray.Length; i++)
            {
                if (sceneToLoadArray[i].NameOfScene == _levelName)
                {
                    if (sceneToLoadArray[i].isLoaded == false)
                    {
                    SceneManager.LoadScene(_levelName, LoadSceneMode.Additive);
                        sceneToLoadArray[i].isLoaded = true;

                        return;
                    }
                    else
                    {
                        UnloadLastLoadedScene(_levelName);
                        fillPanel.ClearUserInterface();             // Clea should be linked to Obkect itself 
                        sceneToLoadArray[i].isLoaded = false;

                        return;
                    }
                }

            }
    }


    void UnloadLastLoadedScene( string _levelName)
    {
        SceneManager.UnloadSceneAsync(_levelName);
    }
}


class SceneToLoadMethodLink
{
    public string NameOfScene;
    public DelegateContainer.DelegateCallInt LoadingSceneMethod;
}


[CreateAssetMenu(fileName = "SceneToLoad", menuName = "SceneToLoadLink", order = 1)]
public class SceneToLoad : ScriptableObject
{
    public string NameOfScene = "ComponentName";
    public DelegateContainer.DelegateCallString loadMethod;
    public bool isLoaded;
    
}

[CreateAssetMenu(fileName = "UserRules", menuName = "RuleSetForUser", order = 1)]
public class RuleSet : ScriptableObject
{
    
    public string [] SceneNames;
    public bool [] Check;

}