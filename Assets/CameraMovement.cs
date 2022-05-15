using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/// <summary>
/// 
/// Camera Movement
/// 
/// Author: Sebastian de Andrade
/// Date 5.8.2019
/// 
/// 
/// Movement script for camera in building editor.
/// 
/// </summary>







public class CameraMovement : MonoBehaviour
{

    public float speed;
    Vector3 MoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


      
        MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, transform.forward.z * Input.GetAxis("Vertical"));
        MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 1* Input.GetAxis("Vertical"));






        transform.position += MoveDirection *  speed * Time.deltaTime;



 


        
    }
}
