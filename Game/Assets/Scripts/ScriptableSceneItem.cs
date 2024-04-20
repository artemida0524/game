using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Scriptable Item", menuName ="Scriptable Item/Scene")]
public class ScriptableSceneItem : ScriptableObject
{
    public string idScene;
    public Vector3 scenePlace; 
}
