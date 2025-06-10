using UnityEngine;

public class UIBase<T> : Singleton<T> where T : UIBase<T>, IUIBase
{
    [SerializeField] private GameObject uiContainer;
    
    public virtual void Open()
    {
        uiContainer.SetActive(true);
        UIManager.Instance.OpenPanel(this as IUIBase);
    }
    
    public virtual void Close()
    {
        uiContainer.SetActive(false);
        UIManager.Instance.ClosePanel(this as IUIBase);
    }
}
