using UnityEngine;


public enum TypeObject
{
    Weapon,
    Food,
    Instrument
}


[CreateAssetMenu(fileName = "Item")]
public class ScriptableItem : ScriptableObject
{
    public TypeObject typeObject;
    public Sprite sprite;
    public GameObject gameObject;
    public int count;
    public string id;
}
