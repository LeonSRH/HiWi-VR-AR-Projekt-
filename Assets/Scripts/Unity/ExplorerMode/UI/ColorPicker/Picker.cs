using SmartHospital.Extensions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Picker : MonoBehaviour {
    public Color color = Color.black;
    Image image;

    int resolution = 32;
    int thickness = 5;

    public int Resolution {
        get { return resolution; }
        set {
            resolution = value;
            BuildPicker();
        }
    }

    public int Thickness {
        get { return thickness; }
        set {
            thickness = value;
            BuildPicker();
        }
    }

    void Start() {
        image = GetComponent<Image>();
        BuildPicker();
    }

    Texture2D GenerateTexture(int edgeLength) {
        var texture = new Texture2D(edgeLength, edgeLength);
        var center = edgeLength / 2;

        for (var i = 0; i < texture.width; i++) {
            for (var j = 0; j < texture.height; j++) {
                var distance = Mathematics.CalculateDistance(j, i, center, center);

                if (distance > center || distance < center - thickness) {
                    texture.SetPixel(j, i, Color.clear);
                }
                else if (distance <= center && distance >= center - thickness) {
                    texture.SetPixel(j, i, color);
                }
                else {
                    Debug.LogError($"Illegal distance in picker: {distance}");
                }
            }
        }

        texture.Apply();

        return texture;
    }

    public void BuildPicker() {
        image.sprite = Sprite.Create(GenerateTexture(resolution), new Rect(0f, 0f, resolution, resolution),
            new Vector2(0.5f, 0.5f));
    }
}