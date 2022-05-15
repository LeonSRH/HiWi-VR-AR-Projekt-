using UnityEngine;
using UnityEngine.UI;

public sealed class ToggleButton : Button {
    bool _selected;

    public Color SelectedColor { get; set; } = Color.red;

    public Color SelectedFontColor { get; set; } = Color.white;

    public Color DeselectedColor { get; set; } = Color.white;

    public Color DeselectedFontColor { get; set; } = Color.black;

    public bool Selected {
        get { return _selected; }
        set {
            _selected = value;
            GetComponent<Image>().color = _selected ? SelectedColor : DeselectedColor;
            GetComponentInChildren<Text>().color = _selected ? SelectedFontColor : DeselectedFontColor;
        }
    }
}