using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType
{
    NONE,
    RED,
    GREEN,
    BLUE,
}

[Serializable]
public class SlotData
{
    public SlotType type;
    public int level;
    public Color color;

    public SlotData(SlotType type, int level)
    {
        this.type = type;
        this.level = level;
        this.color = GetColorByType(type);
    }

    private Color GetColorByType(SlotType type)
    {
        switch (type)
        {
            case SlotType.NONE:
                return Color.gray;
            case SlotType.RED:
                return Color.red;
            case SlotType.GREEN:
                return Color.green;
            case SlotType.BLUE:
                return Color.blue;
            default:
                return Color.black;
        }
    }
}

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SlotData data;
    public Image icon;
    public TextMeshProUGUI levelText;

    private Color originIconColor;

    public void InitSlot()
    {
        data = new SlotData(SlotType.NONE, 0);
        icon.color = data.color;
        levelText.text = "";
    }

    public void SetSlot(SlotData data)
    {
        this.data = data;
        icon.color = data.color;
        levelText.text = data.level.ToString();
    }

    public bool CanMerge(Slot slot)
    {
        if (slot != null && this.data.type == slot.data.type && this.data.level == slot.data.level && this.data.type != SlotType.NONE && slot.data.type != SlotType.NONE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public SlotData UpgradeSlot()
    {
        if (this.data.level >= 3)
        {
            GameManager.Instance.AddScore();
            InitSlot();
            return null;
        }

        return new SlotData(data.type, data.level + 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (data.type == SlotType.NONE)
        {
            return;
        }

        originIconColor = icon.color;

        Color setAlpha = originIconColor;
        setAlpha.a = 0.8f;
        icon.color = setAlpha;

        GameManager.Instance.dragIcon.SetDragIcon(this);
        GameManager.Instance.dragIcon.OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameManager.Instance.dragIcon.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (data.type == SlotType.NONE)
        {
            return;
        }

        GameManager.Instance.dragIcon.OnEndDrag();

        if (eventData.pointerEnter == null)
        {
            icon.color = originIconColor;
            return;
        }

        Slot targetSlot = eventData.pointerEnter.GetComponent<Slot>();

        if (targetSlot != null && targetSlot != this && this.CanMerge(targetSlot))
        {
            SlotData upgrade = targetSlot.UpgradeSlot();

            if (upgrade != null)
            {
                targetSlot.SetSlot(upgrade);
            }

            this.InitSlot();
            return;
        }
        icon.color = originIconColor;
    }
}
