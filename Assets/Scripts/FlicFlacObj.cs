using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlicFlacObj : MonoBehaviour {

    public float timer;
    bool isActive;

    public GameObject[] elements; 

	// Use this for initialization
	void Start () {


        Invoke("ChangeState", timer);
	}



    void ChangeState()
    {
        
        for(int i = 0; i< elements.Length; i++)
        {
            elements[i].SetActive(isActive);

        }

        isActive = !isActive;

        Invoke("ChangeState", timer);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
