using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    public GameObject parentUI;
    public List<SlotUI> slotUIList;
    // Start is called before the first frame update
    void Start()
    {
        parentUI=this.transform.Find("parentUI").gameObject;
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
    }
    private void InitUI()
    {
        slotUIList=new List<SlotUI>(24);
        SlotUI[] slotUIArray=transform.GetComponentsInChildren<SlotUI>();
        foreach(SlotUI slotUI in slotUIArray)
        {
            slotUIList.Add(slotUI);
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        List<SlotData> slotDataList=InventoryManager.instance.backpack.slotList;
        for(int i=0;i<slotDataList.Count;i++)
        {
            slotUIList[i].SetData(slotDataList[i]);
        }
    }
    private void ToggleUI()
    {
        parentUI.SetActive(!parentUI.activeSelf);
    }
    public void OnCloseClick()
    {
        ToggleUI();
    }
}
