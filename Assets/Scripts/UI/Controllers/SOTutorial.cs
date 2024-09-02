using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScripatbleObjs/Tutorial", menuName = "SOTutorail")]
public class SOTutorial : ScriptableObject {
    public string title;
    [TextArea]public string body;
    public Sprite image;
}