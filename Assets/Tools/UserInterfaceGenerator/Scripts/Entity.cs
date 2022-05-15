using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{


    public string Name;
    public Relation [] Relations;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





}



public class Relation
{
    GameObject relatedObject;
    string name;
    int value;
}
