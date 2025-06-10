using UnityEngine;
using UnityEngine.UI;

public class UIStatus : UIBase<UIStatus>, IUIBase
{
    [SerializeField] private Button ExitBtn;
    
    protected override void Awake()
    {
        base.Awake();
        
        if (ExitBtn != null)
            ExitBtn.onClick.AddListener(UIMainMenu.Instance.OnClickStatusMenu);
    }
}
