using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase<UIInventory>, IUIBase
{
    #region [Inspector]
    [SerializeField] private Button ExitBtn;
    [SerializeField] private InventorySlot[] inventorySlots;

    [Header("ItemInfo")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemOption;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI InteractionTxt;
    [SerializeField] private Image InteractionImg;

    private string optionTxt;

    public InventorySlot SelectedItem { get; private set; }
    public Dictionary<ItemType, int> Equipped  { get; private set; }
    
    #endregion


    #region [Lifecycle]
    protected override void Awake()
    {
        base.Awake();
        Equipped = new Dictionary<ItemType, int>();
        
        if (ExitBtn != null)
            ExitBtn.onClick.AddListener(UIMainMenu.Instance.OnClickInventoryMenu);
    }
    
    private void OnEnable()
    {
        SelectedItem?.DeSelectedSlot();
        InteractionImg.enabled = false;
        InitializeSlots();
        InventoryManager.Instance.OnInventorySlotUpdate += UpdateInventorySlot;
    }
    
    public override void Open()
    {
        InitializeSlots();
        base.Open();
    }

    public override void Close()
    {
        SelectedItem?.DeSelectedSlot();
        ResetItemInfo();
        base.Close();
    }
    
    #endregion
    
    
    #region [method]
    // 보유 아이템 기반 인벤토리 슬롯 초기화
    private void InitializeSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SetItem(i, InventoryManager.Instance.Inventory[i]);
        }
    }
    
    private void UpdateInventorySlot(int index)
    {
        if (index < 0 || index >= inventorySlots.Length)
            return;

        ItemSO itemData = InventoryManager.Instance.Inventory[index];

        inventorySlots[index].SetItem(index, itemData);
    }
    
    // 아이템 선택 및 설명 텍스트 변경
    public void SelectItem(InventorySlot item)
    {
        if (SelectedItem != null && SelectedItem != item)
            SelectedItem.DeSelectedSlot();

        InteractionImg.enabled = true;
        SelectedItem = item;
        ShowItemInfo();
        UpdateInteractionText();
    }
    
    public void DeselectItem()
    {
        InteractionImg.enabled = false;
        SelectedItem = null;
        ResetItemInfo();
        UpdateInteractionText();
    }
    
    private void ShowItemInfo()
    {
        if (SelectedItem == null)
            return;
        itemName.text = SelectedItem.InventoryItem.ItemName;
        itemType.text = $"({SelectedItem.InventoryItem.ItemType.ToString()})";
        optionTxt = "";
        foreach (var opt in SelectedItem.InventoryItem.Option)
        {
            optionTxt += $"{opt.stat.ToString()} {opt.value}  ";
        }
        itemOption.text = optionTxt;
        itemDescription.text = SelectedItem.InventoryItem.ItemDescription;
    }

    private void ResetItemInfo()
    {
        SelectedItem = null;
        itemName.text = "";
        itemType.text = "";
        itemOption.text = "";
        itemDescription.text = "";
        InteractionTxt.text = "";
    }
    
    private void UpdateInteractionText()
    {
        if (SelectedItem == null || SelectedItem.IsEmpty)
        {
            InteractionTxt.text = "";
            return;
        }

        ItemType type = SelectedItem.InventoryItem.ItemType;
        int selectedIndex = SelectedItem.Index;

        if (Equipped.ContainsKey(type) && Equipped[type] == selectedIndex)
        {
            InteractionTxt.text = "Unequip";
        }
        else
        {
            InteractionTxt.text = "Equip";
        }
    }
    
    // 아이템 장착 & 해제
    public void EquipSelectedItem()
    {
        if (SelectedItem == null || SelectedItem.IsEmpty)
            return;

        ItemSO item = SelectedItem.InventoryItem;
        ItemType type = item.ItemType;
        int currentIndex = SelectedItem.Index;

        if (Equipped.ContainsKey(type))
        {
            int prevIndex = Equipped[type];
            if (prevIndex == currentIndex)
            {
                inventorySlots[prevIndex].DisableOutline();
                Equipped.Remove(type);
                UpdateInteractionText();
                UIStatus.Instance.Initialize();
                return;
            }

            inventorySlots[prevIndex].DisableOutline();
        }

        inventorySlots[currentIndex].EnableOutline();
        Equipped[type] = currentIndex;
        SelectedItem.EquipItem();
        UpdateInteractionText();
        UIStatus.Instance.Initialize();
    }
    
    #endregion
}
