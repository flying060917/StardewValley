using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    private SlotData data;
    public Image icon;
    public Text countText;
    public int index=0;
    public void SetData(SlotData slotData)
    {
        this.data=slotData;
        data.AddListener(OnDataChange);
        UpdateUI();
    }
    public SlotData GetData()
    {
        return data;
    }
    private void OnDataChange()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        
        if(data.item==null)
        {
            icon.enabled=false;
            countText.enabled=false;
        }
        else
        {
            icon.enabled=true;
            countText.enabled=true;
            icon.sprite=data.item.icon;
            countText.text=data.count.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemMoveHandler.instance.OnClick(this);
    }
}
