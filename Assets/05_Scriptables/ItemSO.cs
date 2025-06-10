using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
}

[System.Serializable]
public class StatOption
{
    public StatType stat;
    public int value;
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/Item", order = 0)]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public ItemType ItemType;
    public Sprite ItemSprite;
    public string ItemDescription;
    public List<StatOption> Option = new List<StatOption>();
    
    
}
