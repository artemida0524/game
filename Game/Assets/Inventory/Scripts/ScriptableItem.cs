using UnityEngine;


public enum TypeObject
{
    Weapon,
    Food,
    Instrument,
    Resource,
    Build
}

public enum FurnaceMode
{
    None,
    Fuel,
    Resource,
    UpdateResource
}


[CreateAssetMenu(fileName = "Item")]
public class ScriptableItem : ScriptableObject
{
    public TypeObject typeObject;
    public FurnaceMode furnaceMode;

    public int countObjectForMerger;

    public int secondFuseIfResource;
    public int secondFuel;
    public string idResultIfResource;

    public int SetHpIfFood;
    public int SetFoodIfFood;
    public int SetWaterIfFood;

    public Sprite sprite;
    public GameObject gameObject;
    public int count;
    public string id;


    public string description;

}
