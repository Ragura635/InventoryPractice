using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    #region [Inspector]
    [SerializeField] private Image icon;
    [SerializeField] private Image outline;
    
    private bool isSelected;

    public bool IsEmpty => InventoryItem == null;
    public int  Index   { get; private set; }
    public ItemSO InventoryItem { get; private set; }
    
    #endregion
    
    
    #region [LifeCycle]
    
    #endregion
    
    
    #region [method]
    // 아이템 초기화
    public void SetItem(int index, ItemSO item)
    {
        Index = index;
        if (item == null)
        {
            EmptySlot();
            return;
        }

        icon.enabled = true;
        InventoryItem = item;
        icon.sprite = item.ItemSprite;
    }

    public void EmptySlot()
    {
        icon.enabled = false;
        InventoryItem = null;
    }

    // 아이템 선택 및 선택해제
    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsEmpty || !EventSystem.current.IsPointerOverGameObject())
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSelected)
                DeSelectedSlot();
            else
                SelectedSlot();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            EquipItem();
        }
    }

    public void SelectedSlot()
    {
        UIInventory.Instance.SelectItem(this);
        isSelected = true;
    }

    public void DeSelectedSlot()
    {
        UIInventory.Instance.DeselectItem();
        isSelected = false;
    }

    
    public void EquipItem()
    {
        ItemSO itemSo = InventoryItem;
        InventoryManager.Instance.UseItem(Index);
    }
    
    // 장착시 아웃라인 활성화, 해제시 비활성화
    public void EnableOutline()
    {
        if (outline != null)
            outline.gameObject.SetActive(true);
    }

    public void DisableOutline()
    {
        if (outline != null)
            outline.gameObject.SetActive(false);
    }
    
    #endregion
    
    
    // 구현 실패
    #region [ing...]
    // TODO: 드래그 앤 드랍을 통한 인벤토리 내부 슬롯 전환 구현 예정
    private void SwitchInvenSlot(InventorySlot swich)
    {
        InventoryManager.Instance.SwichItem(swich.Index, Index);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!DragManager.Instance.IsDragging)
            return;

        if (DragManager.Instance.DraggedInventoryItem != null)
        {
            SwitchInvenSlot(DragManager.Instance.DraggedInventoryItem);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            DragManager.Instance.StartDrag(this, UIInventory.Instance.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragManager.Instance.UpdateDrag(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.Instance.EndDrag();
    }
    
    #endregion
    
}
