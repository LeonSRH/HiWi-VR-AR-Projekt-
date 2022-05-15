using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBetweenPoints : MonoBehaviour
{
    public Transform Pos1;
    public Transform Pos2;

    
    // Start is called before the first frame update
    void Start()
    {

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, Pos1.position);
        lineRenderer.SetPosition(1, Pos2.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
