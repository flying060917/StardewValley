using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    None,
    Seed_Carrot,
    Seed_Potato,
    Seed_Tomato,
    Hoe
}
[CreateAssetMenu()]
public class ItemData:ScriptableObject
{
    public ItemType type=ItemType.None;
    public Sprite icon;
    public GameObject prefab;
    public int maxCount=1;
}

