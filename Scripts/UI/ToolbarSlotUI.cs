using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarSlotUI : SlotUI
{
    public Sprite hightSprite;
    public Sprite darkSprite;
    private Image image;
    private void Awake()
    {
        image=this.GetComponent<Image>();
    }
    public void SetHighLight()
    {
        image.sprite=hightSprite;
    }
    public void SetDark()
    {
        image.sprite=darkSprite;
    }
}
