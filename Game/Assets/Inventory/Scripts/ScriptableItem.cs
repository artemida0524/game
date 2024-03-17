using UnityEngine;


public enum TypeObject
{
    Weapon,
    Food,
    Instrument,
    Resource
}


[CreateAssetMenu(fileName = "Item")]
public class ScriptableItem : ScriptableObject
{
    public TypeObject typeObject;

    public int SetHpIfFood;
    public int SetFoodIfFood;
    public int SetWaterIfFood;

    public Sprite sprite;
    public GameObject gameObject;
    public int count;
    public string id;
}
