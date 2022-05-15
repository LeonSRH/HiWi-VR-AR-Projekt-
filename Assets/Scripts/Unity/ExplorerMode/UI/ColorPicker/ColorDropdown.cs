using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class ColorDropdown : MonoBehaviour {
    TMP_Dropdown dropdown;

    public delegate void ChangeColor(Color newColor);

    public delegate void EndColorChange(Color finalColor);

    public event ChangeColor OnColorChange;

    public event EndColorChange OnEndColorChange;

    public Color Color {
        get { return pickedColor; }
        set { SetColor(value); }
    }

    Color pickedColor;
    
    readonly Color[] colors = {
        Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white,
        Color.yellow
    };

    readonly string[] colorNames = {
        "Schwarz", "Blau", "Cyan", "Grau", "Gruen", "Magenta", "Rot", "Weiss", "Gelb"
    };

    void Awake() {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    void Start() {
        
        for (var i = 0; i < colors.Length; i++) {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, colors[i]);
            texture.Apply();

            dropdown.options.Add(new TMP_Dropdown.OptionData(colorNames[i], Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0, 0))));
        }

        dropdown.onValueChanged.AddListener(index => {
            pickedColor = colors[index];
            OnColorChange?.Invoke(pickedColor);
            OnEndColorChange?.Invoke(pickedColor);
        });
    }

    void SetColor(Color color) {
        if (colors.Contains(color)) {
            dropdown.value = colors.ToList().IndexOf(color);
        }
        else {
            Debug.LogError($"Unavailable Color {color}");
        }
    }
}