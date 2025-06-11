using System;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    #region [Inspector]
    [SerializeField] private ItemSO[] inventory;
    
    public event Action<int> OnInventorySlotUpdate;
    public ItemSO[] Inventory { get => inventory; set => inventory = value; }

    #endregion
    
    
    #region [LifeCycle]
    protected override void Awake()
    {
        base.Awake();
        Inventory = inventory;
    }
    
    #endregion
    
    
    #region [method]
    public void UseItem(int index)
    {
        ItemSO inventoryItem = Inventory[index];

        if (inventoryItem == null)
        {
            return;
        }

        OnInventorySlotUpdate?.Invoke(index);
    }
    
    #endregion
    
    
    // 구현 실패
    #region [ing...]
    public void SwichItem(int from, int to)
    {
        (Inventory[from], Inventory[to]) = (Inventory[to], Inventory[from]);
        
        OnInventorySlotUpdate?.Invoke(from);
        OnInventorySlotUpdate?.Invoke(to);
    }
    
    #endregion
}
