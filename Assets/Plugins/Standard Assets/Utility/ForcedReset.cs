using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI; // Required when Using UI elements.

#pragma warning disable CS0618 // "GUITexture" ist veraltet: "This component is part of the legacy UI system and will be removed in a future release."
[RequireComponent(typeof (Image))]
#pragma warning restore CS0618 // "GUITexture" ist veraltet: "This component is part of the legacy UI system and will be removed in a future release."
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... reload the scene
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
