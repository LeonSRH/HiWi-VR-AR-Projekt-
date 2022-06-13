using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerRegistration : MonoBehaviour
{
    [SerializeField] private BoxCollider Triggerarea;
    [SerializeField] private GameObject Prompt;
    public bool triggerenabled;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerenabled)
        {
            Triggerarea.enabled = true;
            Prompt.SetActive(true);
        }
        else
        {
            Triggerarea.enabled = false;
            Prompt.SetActive(false);
        }
    }

    public void SetBool(bool active)
    {
        triggerenabled = active;

    }
}
