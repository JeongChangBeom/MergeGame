using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragIcon : MonoBehaviour
{
    public Slot slot;
    public GameObject icon;
    public Image iconImage;
    public TextMeshProUGUI iconText;

    private void Start()
    {
        icon.SetActive(false);
    }

    public void SetDragIcon(Slot slot)
    {
        this.slot = slot;
    }

    public void OnBeginDrag()
    {
        icon.SetActive(true);
        iconImage.color = slot.data.color;
        iconText.text = slot.data.level.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (icon != null)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag()
    {
        icon.SetActive(false);
    }
}
