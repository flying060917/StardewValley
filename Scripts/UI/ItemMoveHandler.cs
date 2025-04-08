using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMoveHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static ItemMoveHandler instance;
    private Image icon;
    private SlotData selectSlotData;
    public PlayerMove player;
    private bool isCtrlDown=false;
    public void Awake()
    {
        instance=this;
        icon=GetComponentInChildren<Image>();
        HideIcon();
    }
    private void Update()
    {
        if(icon.enabled)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
            Input.mousePosition,null,
            out pos);
            icon.GetComponent<RectTransform>().anchoredPosition=pos;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject()==false)
            {
                Throw();
            }
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isCtrlDown=true;
        }
        else
        {
            isCtrlDown=false;
        }
        if(Input.GetMouseButtonDown(1))
        {
            ClearHand();
        }
        
    }
    public void OnClick(SlotUI slotUI)
    {
        //判断手上是否为空
        if(selectSlotData!=null)
        {
            if(slotUI.GetData().IsEmpty())
            {
                //当前点击了一个空格子
                MoveToEmptySlot(selectSlotData,slotUI.GetData());
            }
            else
            {
                //当前点击了一个非空格子
                //点击了自身
                if(slotUI.GetData()==selectSlotData) 
                {
                    Debug.Log("ClickSelf");
                    return;
                }
                //点击了非自身
                else
                {
                    //点击了同类
                    if(slotUI.GetData().item==selectSlotData.item)
                    {
                        Debug.Log("ClickSame");
                        MoveToNotEmptySlot(selectSlotData,slotUI.GetData());
                    }
                    //点击了非同类
                    else
                    {
                        Debug.Log("ClickDiff");
                        SwitchItem(selectSlotData,slotUI.GetData());
                    }
                }  
            }
        }
        else
        {
            //手上为空
            if(slotUI.GetData().IsEmpty()) return;
            selectSlotData=slotUI.GetData();
            ShowIcon(selectSlotData.item.icon);
            Debug.Log("HandEmpty");
        }

    }
    private void HideIcon()
    {
        icon.enabled=false;
    }
    private void ShowIcon(Sprite sprite)
    {
        icon.sprite=sprite;
        icon.enabled=true;
    }
    private void ClearHand()
    {
        HideIcon();
        selectSlotData=null;
    }
    private void Throw()
    {
        if(selectSlotData!=null)
        {
            GameObject prefab=selectSlotData.item.prefab;
            int count=selectSlotData.count;
            if(isCtrlDown)
            {
                player.ThrowItem(prefab,1);
                selectSlotData.Reduce(); 
                Debug.Log("CtrlThrow");
            }
            else
            {
                player.ThrowItem(prefab,count);
                selectSlotData.Clear();
                Debug.Log("AllThrow");
            }
            if(selectSlotData.IsEmpty())
            {
                ClearHand();
            } 
        }  
    }
    private void MoveToEmptySlot(SlotData fromData,SlotData toData)
    {
        if(isCtrlDown)
        {
            toData.AddItem(fromData.item);
            fromData.Reduce();
        }
        else
        {
            toData.MoveSlot(fromData);
            fromData.Clear();
            ClearHand();
        } 

    }
    private void MoveToNotEmptySlot(SlotData fromData,SlotData toData)
    {
        if(isCtrlDown)
        {
            if(toData.CanAddItem())
            {
                toData.Add();
                fromData.Reduce();
            }  
        }
        else
        {
            if(fromData.count>toData.GetFreeSpace())
            {
                int freeSpace=toData.GetFreeSpace();
                toData.Add(freeSpace);
                Debug.Log(fromData.count);
                fromData.Reduce(freeSpace);
                Debug.Log(fromData.count);
            }
            else
            {
                toData.Add(fromData.count);
                fromData.Clear();
            }
        }
        if(selectSlotData.IsEmpty())
        {
            ClearHand();
        } 
    }
    private void SwitchItem(SlotData data1,SlotData data2)
    {
        ItemData tempItem=data1.item;
        int tempCount=data1.count;
        data1.MoveSlot(data2);
        data2.AddItem(tempItem,tempCount);
        ClearHand();
    }
}
