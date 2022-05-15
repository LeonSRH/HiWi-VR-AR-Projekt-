using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRelationSetterController : MonoBehaviour
{





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RayOnObject();
    }



    public  Transform ReturnClickedTarget()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {


            Debug.Log(hitInfo.transform.gameObject.name);


            return hitInfo.transform;

            //currentPlaceableObject.transform.position = hitInfo.point;

            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

        }

        return transform;

    }


    public bool CheckCursorOnGameObject(string _name)
    {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {


            if (hitInfo.transform.name == _name)
               return true;
         

        }


        return false;




    }


    private void RayOnObject()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {


            Debug.Log(hitInfo.transform.gameObject.name);




            //currentPlaceableObject.transform.position = hitInfo.point;

            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

        }

    }


}
