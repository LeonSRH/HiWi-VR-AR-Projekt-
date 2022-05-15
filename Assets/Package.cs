using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{

    bool MoveObj;
    Vector3 Target;

    Port targetPort;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetNextDestination(bool _moveObj, Vector3 _target, Port _port)
    {
        Target = _target;
        MoveObj = _moveObj;

        targetPort = _port;

    }


    // Update is called once per frame
    void Update()
    {

        if (MoveObj)
        {

            transform.position = Vector3.MoveTowards(transform.position, Target, 0.5f);

            if (Vector3.Distance(transform.position, Target) < 0.5f)
            {



                Debug.Log("Ziel erreicht");
                MoveObj = false;

                targetPort.AddPackageToPort(1);

                Destroy(gameObject);

            }


        }

        
    }
}
