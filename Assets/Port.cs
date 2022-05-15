using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Port : MonoBehaviour
{


    public GameObject Package;

    Vector3 StartPoint;


    public enum StatesOfPort { CheckForTarget, Online, Activated, Deactivated, Error}
    public StatesOfPort states;


    public Transform EndPoint;


    public int currentPackages;
    public TextMeshPro Text;

    private Port EndPort;


    public ObjectRelationSetterController objRelSetCon;



    // Start is called before the first frame update
    void Start()
    {
        objRelSetCon = GameObject.FindObjectOfType<ObjectRelationSetterController>();

        states = StatesOfPort.Activated;

        gameObject.name = gameObject.name + Time.time.ToString();


    }

    // Update is called once per frame
    void Update()
    {









        Text.text = currentPackages.ToString();


        if (Input.GetKeyDown(KeyCode.Q))
        {

            StartPackageSending();
        }




        switch (states)
        {
            case StatesOfPort.Activated:

                if (Input.GetMouseButtonDown(1) && objRelSetCon.CheckCursorOnGameObject(transform.name))
                {
                    states = StatesOfPort.CheckForTarget;
                }

                break;

            case StatesOfPort.CheckForTarget:

                if (Input.GetMouseButtonDown(0))
                {
                    EndPoint = objRelSetCon.ReturnClickedTarget();

                    states = StatesOfPort.Activated;
                }

                break;
            case StatesOfPort.Deactivated:

                break;
            case StatesOfPort.Error:

                break;
            case StatesOfPort.Online:

                break;
            default:

                break;
        }





    }


    public void AddPackageToPort(int _amount)
    {
        currentPackages += _amount;
    }

    public void StartPackageSending()
    {

        currentPackages -= 1;

        GameObject obj = Instantiate(Package, transform.position, transform.rotation);

        obj.GetComponent<Package>().SetNextDestination(true, EndPoint.position,EndPoint.GetComponent<Port>());

    }

}
