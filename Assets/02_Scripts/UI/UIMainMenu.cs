using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : Singleton<UIMainMenu>
{
    [SerializeField] private Button statusBtn;
    [SerializeField] private Button inventoryBtn;
    
    protected override void Awake()
    {
        base.Awake();
        
        if (statusBtn != null)
            statusBtn.onClick.AddListener(UIMainMenu.Instance.OnClickStatusMenu);
        
        if (inventoryBtn != null)
            inventoryBtn.onClick.AddListener(UIMainMenu.Instance.OnClickInventoryMenu);
    }

    public void OnClickStatusMenu()
    {
        statusBtn.gameObject.SetActive(!statusBtn.gameObject.activeSelf);
        UIManager.Instance.CheckOpenPopup(UIStatus.Instance);
    }
    
    public void OnClickInventoryMenu()
    {
        inventoryBtn.gameObject.SetActive(!inventoryBtn.gameObject.activeSelf);
        UIManager.Instance.CheckOpenPopup(UIInventory.Instance);
    }
}
