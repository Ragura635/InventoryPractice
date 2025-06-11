using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region [Inspector]
    private List<IUIBase> openedUI = new List<IUIBase>();
    
    public bool IsOpenUI { get; private set; }
    
    #endregion
    
    
    #region [Method]
    public void CheckOpenPopup(IUIBase panel)
    {
        if (openedUI.Contains(panel))
        {
            panel.Close();
        }
        else
        {
            panel.Open();
        }
    }
    
    #endregion
    
    
    // 구현 실패
    #region [ing...]
    // TODO: ESC키 입력을 통한 통합형 ClosePanel 구현 예정
    // UI패널들을 열린 순서대로 스택에 push
    // 이미 열린 패널과 상호작용시 스택의 peek로 이동
    // ESC 입력시 하나씩 pop해가며 ClosePanel
    
    public void OpenPanel(IUIBase panel)
    {
        openedUI.Add(panel);
        IsOpenUI = true;
    }

    public void ClosePanel(IUIBase panel)
    {
        openedUI.Remove(panel);
        IsOpenUI = false;
    }
    
    #endregion
}
