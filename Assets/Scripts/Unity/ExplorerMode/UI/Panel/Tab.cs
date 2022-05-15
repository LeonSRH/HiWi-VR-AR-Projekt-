using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Tab {
    public string Label;
    public CanvasRenderer Panel;
    public UnityEvent onChange;
}