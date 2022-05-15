using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public partial class RoomSearch : MonoBehaviour
{

    PieGraph searchResultPie;

    
    private void Awake()
    {
        searchResultPie = GameObject.FindObjectOfType<PieGraph>();
    }
}
