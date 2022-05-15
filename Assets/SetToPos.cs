using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToPos : MonoBehaviour
{

    public Vector3[] Layers;
    public Transform CameraPos;


    public Transform ParentObjectLayers;
    public GameObject ButtonForLayer;

    int currentLayer = 0;

    // Start is called before the first frame update
    void Start()
    {


        // We 
        Layers = new Vector3[50];

        for (int i = 0; i < 50; i++)
        {

            Layers[i] = new Vector3(0, -i * 1, 0);

        }

        CreateLayer();




 

    }

    /*
    public void setupBtn()
    {
        string param = "bar";
        btn.GetComponent<Button>().onClick.AddListener(delegate { btnClicked(param); });
    }

    public void btnClicked(string param)
    {
        Debug.Log("foo " + param);
    }
    */

    public void CreateLayer()
    {

        GameObject clone = Instantiate(ButtonForLayer, ParentObjectLayers);


        Debug.Log(currentLayer);
        int currentLayerCopy = currentLayer;

        Debug.Log(clone.name);
        clone.GetComponent<Button>().onClick.AddListener(delegate { SetToPosInWorldMatrix(currentLayerCopy); });

        currentLayer++;
    }

    public void SetToPosInWorldMatrix(int _LayerOfArray)
    {
        if (Layers[_LayerOfArray] != null)
            transform.position = Layers[_LayerOfArray];
        else
        {
            // LAYER ERROR
        }

    }
}
