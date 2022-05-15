using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LMS Object", menuName = "LMSObj")]
public class LMSObject : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public List<LMSURLObject> objects;

}
