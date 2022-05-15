using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PieGraph : MonoBehaviour
{

    private float[] values;
    public Color[] wedgeColors;
    public Image wedgePrefab;

    private void Start()
    {
        float[] v = new float[3] { 10f, 100f, 50f };
        //SetValues(v);
        //MakeGraph();
    }

    public string Name { get; set; }

    /// <summary>
    /// Refresh the graph values
    /// </summary>
    public void RefreshGraph()
    {
        if (values != null)
        {
            MakeGraph();
        }

    }

    /// <summary>
    /// Sets the values of the graph
    /// </summary>
    /// <param name="val"></param>
    public void SetValues(float[] val)
    {
        values = val;
    }

    public void Deactivate()
    {

    }

    /// <summary>
    /// Makes a graph based on the values {values}
    /// </summary>
    void MakeGraph()
    {
        float total = 0f;
        float zRotation = 0f;

        for (int i = 0; i < values.Length; i++)
        {
            total += values[i];
        }

        for (int i = 0; i < values.Length; i++)
        {
            Image newWedge = Instantiate(wedgePrefab) as Image;
            newWedge.transform.SetParent(transform, false);
            newWedge.color = wedgeColors[i];
            newWedge.fillAmount = values[i] / total;
            newWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            newWedge.gameObject.GetComponentInChildren<TMP_Text>().SetText(values[i].ToString());

            //Rotate the wedge to the fillamount of the wedge set before
            zRotation -= newWedge.fillAmount * 360f;

        }
    }
}

