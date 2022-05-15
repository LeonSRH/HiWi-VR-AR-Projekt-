using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NavigationDummyView : MonoBehaviour {
    [SerializeField] TMP_Dropdown startDropdown;
    [SerializeField] TMP_Dropdown destDropdown;

    public event Action<string> OnStartChosen;
    public event Action<string> OnDestChosen;

    readonly string[] availableTargets =
        {"6420.01.566", "6420.01.522", "6420.01.371", "6420.01.622", "6420.01.036"};

    void Start() {
        SetupStartPoints();
        startDropdown.onValueChanged.AddListener(delegate(int index) {
            var target = startDropdown.options[index].text;
            destDropdown.options = FilterOptionData(startDropdown.options, target);
            destDropdown.value = 0;
            destDropdown.onValueChanged.Invoke(destDropdown.value);
            OnStartChosen?.Invoke(target);
        });
        destDropdown.onValueChanged.AddListener(delegate(int index) {
            OnDestChosen?.Invoke(destDropdown.options[index].text);
        });
    }

    void SetupStartPoints() {
        var options = availableTargets.Select(t => new TMP_Dropdown.OptionData(t)).ToList();
        startDropdown.options = options;
    }

    List<TMP_Dropdown.OptionData> FilterOptionData(IEnumerable<TMP_Dropdown.OptionData> list, string usedTarget) {
        var newList = new List<TMP_Dropdown.OptionData>(list);

        for (var i = 0; i < newList.Count; i++) {
            if (newList[i].text == usedTarget) {
                newList.Remove(newList[i]);
                break;
            }
        }

        return newList;
    }
}