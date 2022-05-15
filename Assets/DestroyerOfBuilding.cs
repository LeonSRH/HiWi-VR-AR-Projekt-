using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOfBuilding : MonoBehaviour
{



    bool foundDestructableObj = false;
    GameObject GameObjectToDestroy; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {




        if (foundDestructableObj && Input.GetMouseButtonDown(0))
        {

            if (GameObjectToDestroy != null)
             Destroy(GameObjectToDestroy);


             Destroy(gameObject);


        }

        
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Building"))
        {

            foundDestructableObj = true;
            GameObjectToDestroy = other.gameObject;

            transform.localScale = new Vector3(0.9f, 2.0f, 0.9f);


        }
  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building"))
        {

            foundDestructableObj = false;
            GameObjectToDestroy = null;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        }

    }

}
