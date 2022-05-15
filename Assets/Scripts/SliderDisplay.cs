using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderDisplay : MonoBehaviour {
    public TMP_Text Display;

    void Start() {
        GetComponent<Slider>().onValueChanged.AddListener(newValue => Display.text = ((int) newValue).ToString());
    }
}