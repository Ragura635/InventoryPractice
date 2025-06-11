using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : UIBase<UIStatus>, IUIBase
{
    #region [Inspector]
    [SerializeField] private Button ExitBtn;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI curExpTxt;
    [SerializeField] private TextMeshProUGUI maxExpTxt;
    [SerializeField] private Image expGuage;
    
    [SerializeField] private TextMeshProUGUI curHpTxt;
    [SerializeField] private TextMeshProUGUI maxHpTxt;
    [SerializeField] private Image hpGuage;
    
    [SerializeField] private TextMeshProUGUI curAtkTxt;
    [SerializeField] private TextMeshProUGUI deltaAtkTxt;
    [SerializeField] private TextMeshProUGUI curDefTxt;
    [SerializeField] private TextMeshProUGUI deltaDefTxt;
    [SerializeField] private TextMeshProUGUI curCrtTxt;
    [SerializeField] private TextMeshProUGUI deltaCrtTxt;
    [SerializeField] private TextMeshProUGUI curSpdTxt;
    [SerializeField] private TextMeshProUGUI deltaSpdTxt;
    
    #endregion
    
    
    #region [LifeCycle]
    protected override void Awake()
    {
        base.Awake();
        
        if (ExitBtn != null)
            ExitBtn.onClick.AddListener(UIMainMenu.Instance.OnClickStatusMenu);
    }
    
    public override void Open()
    {
        base.Open();
        Initialize();
    }

    public override void Close()
    {
        base.Close();
    }
    
    #endregion
    
    
    #region [Method]
    // 스탯 초기화 및 텍스트 갱신
    public void Initialize()
    {
        StatManager.Instance.InitializeStats();

        Dictionary<StatType, int> delta = new();

        foreach (var kvp in UIInventory.Instance.Equipped)
        {
            var item = InventoryManager.Instance.Inventory[kvp.Value];
            if (item == null) continue;

            foreach (var opt in item.Option)
            {
                if (!delta.ContainsKey(opt.stat))
                    delta[opt.stat] = 0;

                delta[opt.stat] += opt.value;
            }
        }

        StatManager.Instance.ApplyModifiedStats(delta);

        levelTxt.text = StatManager.Instance.Level.ToString("D2");
        int curExp = StatManager.Instance.CurExp;
        int maxExp = StatManager.Instance.MaxExp;
        curExpTxt.text = curExp.ToString();
        maxExpTxt.text = maxExp.ToString();
        expGuage.fillAmount = (float)curExp / maxExp;

        int curHp = StatManager.Instance.CurHp;
        int maxHp = StatManager.Instance.MaxHp;
        curHpTxt.text = curHp.ToString();
        maxHpTxt.text = maxHp.ToString();
        hpGuage.fillAmount = (float)curHp / maxHp;

        DisplayStat(StatManager.Instance.BaseStat(StatType.ATK), delta, StatType.ATK, curAtkTxt, deltaAtkTxt);
        DisplayStat(StatManager.Instance.BaseStat(StatType.DEF), delta, StatType.DEF, curDefTxt, deltaDefTxt);
        DisplayStat(StatManager.Instance.BaseStat(StatType.CRT), delta, StatType.CRT, curCrtTxt, deltaCrtTxt);
        DisplayStat(StatManager.Instance.BaseStat(StatType.SPD), delta, StatType.SPD, curSpdTxt, deltaSpdTxt);
    }
    
    private void DisplayStat(int baseVal, Dictionary<StatType, int> delta, StatType type, TextMeshProUGUI cur, TextMeshProUGUI deltaTxt)
    {
        int deltaVal = delta.ContainsKey(type) ? delta[type] : 0;
        int total = baseVal + deltaVal;

        cur.text = total.ToString();

        if (deltaVal == 0)
        {
            deltaTxt.text = "";
        }
        else
        {
            string color = deltaVal > 0 ? "#029600" : "#FF3940";
            string sign = deltaVal > 0 ? "+" : "";
            deltaTxt.text = $"<color={color}>({sign}{deltaVal})</color>";
        }
    }
    
    #endregion
    
}
