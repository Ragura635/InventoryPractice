using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    
    public void OnOpenStatus(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIManager.Instance.CheckOpenPopup(UIStatus.Instance);
        }
    }
    
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIManager.Instance.CheckOpenPopup(UIInventory.Instance);
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
