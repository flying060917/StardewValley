using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
[Serializable]
public class SlotData
{
    public ItemData item;
    public int count=0;
    private Action OnChange;
    public void MoveSlot(SlotData slotData)
    {
        this.item=slotData.item;
        this.count=slotData.count;
        OnChange?.Invoke();
    }
    public bool IsEmpty()
    {
        return count==0;
    }
    public bool CanAddItem()
    {
        return count<item.maxCount;
    }
    public void Add(int num=1)
    {
        count=count+num;
        OnChange?.Invoke();
        return;
    }
    public void AddItem(ItemData item,int count=1)
    {
        this.item=item;
        this.count=count;
        OnChange?.Invoke();
    }
    public int GetFreeSpace()
    {
        return item.maxCount-this.count;
    }
    public void Reduce(int numToReduce=1)
    {
        count-=numToReduce;
        if(count==0) Clear();
        else OnChange?.Invoke();
    }
    public void Clear()
    {
        item=null;
        count=0;
        OnChange?.Invoke();
    }
    public void AddListener(Action action)
    {
        OnChange=action;
    }
}
