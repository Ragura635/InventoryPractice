using UnityEngine;
using UnityEngine.UI;

public class DragManager : Singleton<DragManager>
{
    public InventorySlot DraggedInventoryItem { get; private set; }
    [SerializeField] private Image dragImage;
    
    public bool IsDragging { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        dragImage.gameObject.SetActive(false);
    }

    public void StartDrag(InventorySlot slot, Transform source)
    {
        if (slot.InventoryItem == null)
            return;
        DraggedInventoryItem = slot;
        dragImage.transform.SetParent(source);
        SetDragSlot(slot.InventoryItem.ItemSprite);
    }

    public void UpdateDrag(Vector2 position)
    {
        dragImage.transform.position = position;
    }

    public void EndDrag()
    {
        if (DraggedInventoryItem != null)
            DraggedInventoryItem = null;

        dragImage.gameObject.SetActive(false);
        IsDragging = false;
    }

    void SetDragSlot(Sprite img)
    {
        dragImage.gameObject.SetActive(true);
        dragImage.sprite = img;
        dragImage.transform.SetAsLastSibling();
        IsDragging = true;
    }
}
