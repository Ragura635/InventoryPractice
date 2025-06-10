using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    
    public void OnOpenStatus(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIMainMenu.Instance.OnClickStatusMenu();
        }
    }
    
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIMainMenu.Instance.OnClickInventoryMenu();
        }
    }
    
    public void OnEquipItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIInventory.Instance.EquipSelectedItem();
        }
    }
}
