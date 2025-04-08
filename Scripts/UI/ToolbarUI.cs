using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public List<ToolbarSlotUI> slotUIList;
    private ToolbarSlotUI selectSlotUI;
    public static ToolbarUI instance;
    private void Awake()
    {
        instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    void Update()
    {
        ToolBarSelectControl();
    }
    public ToolbarSlotUI GetSelectSlotUI()
    {
        return selectSlotUI;
    }
    private void Init()
    {
        slotUIList=new List<ToolbarSlotUI>();
        ToolbarSlotUI[] slotUIArray=this.GetComponentsInChildren<ToolbarSlotUI>();
        foreach(ToolbarSlotUI slotUI in slotUIArray)
        {
            slotUIList.Add(slotUI);
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        List<SlotData> slotDataList=InventoryManager.instance.toolbar.slotList;
        for(int i=0;i<slotUIList.Count;i++)
        {
            slotUIList[i].SetData(slotDataList[i]);
        }
    }
    void ToolBarSelectControl()
    {
        for(int i=(int)KeyCode.Alpha1;i<=(int)KeyCode.Alpha9;i++)
        {
            if(Input.GetKeyDown((KeyCode)i))
            {
                if(selectSlotUI!=null)
                {
                    selectSlotUI.SetDark();
                }
                int index=i-(int)KeyCode.Alpha1;
                selectSlotUI=slotUIList[index];
                selectSlotUI.SetHighLight();
            }
        }
    }

}
