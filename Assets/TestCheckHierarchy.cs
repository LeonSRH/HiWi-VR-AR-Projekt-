using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TestCheckHierarchy : MonoBehaviour
{


    public Scene[] scenesToCheck;





    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(CheckScenes());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator CheckScenes()
    {

        scenesToCheck = SceneManager.GetAllScenes();
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < scenesToCheck.Length; i++)
        {

            //SceneManager.LoadScene(scenesToCheck[i].name);

            //SceneManager.

            GameObject[] rootElements = scenesToCheck[i].GetRootGameObjects();
            yield return new WaitForSeconds(1.0f);

            for (int e = 0; e < rootElements.Length; e++)
            {

                Debug.Log(e.ToString() +" Hierarchy = " + rootElements[e].ToString());

                yield return new WaitForSeconds(0.25f);


            }



        }



        yield return new WaitForSeconds(1.0f);

    }


}
