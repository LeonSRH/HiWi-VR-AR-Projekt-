using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_Element : MonoBehaviour
{


    public int Layer;
    public bool isDraged;
    public bool isDragable;


    Vector3 Pos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    float distance = 10;



    private void OnMouseDrag()

    {
        if (isDragable)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objectPos;
        }

    }


}
