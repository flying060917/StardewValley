using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private Dictionary<ItemType,ItemData> itemDataDict=new Dictionary<ItemType,ItemData>();
    [HideInInspector]
    public InventoryData backpack;
    [HideInInspector]
    public InventoryData toolbar;
    private void Awake()
    {
       instance=this;
       Init(); 
    }
    private void Init()
    {
        ItemData[] itemDataArray=Resources.LoadAll<ItemData>("Data");
        foreach(ItemData itemData in itemDataArray)
        {
            itemDataDict.Add(itemData.type,itemData);
        }
        backpack=Resources.Load<InventoryData>("Data/Backpack");
        toolbar=Resources.Load<InventoryData>("Data/Toolbar");
    }
    private ItemData GetItemData(ItemType type)
    {
        ItemData data;
        bool isSuccess=itemDataDict.TryGetValue(type,out data);
        if(isSuccess)
        {
            return data;
        }
        else
        {
            Debug.LogWarning("你传递的Type："+type+"不存在");
            return null;
        }
    }
    public void AddItemToBackpack(ItemType type)
    {
        ItemData data=GetItemData(type);
        foreach(SlotData slotData in backpack.slotList)
        {
            if(slotData.item==data&&slotData.CanAddItem())
            {
                slotData.Add();
                return;
            }
        }
        foreach(SlotData slotData in backpack.slotList)
        {
            if(slotData.count==0)
            {
                slotData.AddItem(data);
                return;
            }
        }
        Debug.LogWarning("你的背包已满");
    }
}
