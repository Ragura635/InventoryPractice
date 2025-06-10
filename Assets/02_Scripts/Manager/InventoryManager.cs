using System;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private ItemSO[] inventory;
    
    public ItemSO[] Inventory { get => inventory; set => inventory = value; }

    public event Action<int> OnInventorySlotUpdate;

    protected override void Awake()
    {
        base.Awake();
        Inventory = inventory;
    }
    
    public void UseItem(int index, int amount = 1)
    {
        ItemSO inventoryItem = Inventory[index];

        if (inventoryItem == null)
        {
            return;
        }

        bool canUse = true;
        if (!canUse)
            return;

        OnInventorySlotUpdate?.Invoke(index);
    }

    public void SwichItem(int from, int to)
    {
        (Inventory[from], Inventory[to]) = (Inventory[to], Inventory[from]);


        OnInventorySlotUpdate?.Invoke(from);
        OnInventorySlotUpdate?.Invoke(to);
    }
}
