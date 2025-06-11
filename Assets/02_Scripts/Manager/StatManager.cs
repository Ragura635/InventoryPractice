using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    ATK,
    DEF,
    CRT,
    SPD,
}

public class StatManager : Singleton<StatManager>
{
    #region [Inspector]
    [SerializeField] private int level = 1;
    private int curExp;
    private int maxExp;

    private int curHp;
    private int maxHp;

    private int curAtk;
    private int baseAtk;
    private int curDef;
    private int baseDef;
    private int curCrt;
    private int baseCrt;
    private int curSpd;
    private int baseSpd;
    
    public int Level => level;
    public int CurExp => curExp;
    public int MaxExp => maxExp;
    public int CurHp => curHp;
    public int MaxHp => maxHp;
    
    #endregion
    
    
    #region [Method]
    // 스탯별 스탯반환
    public int BaseStat(StatType type)
    {
        return type switch
        {
            StatType.ATK => baseAtk,
            StatType.DEF => baseDef,
            StatType.CRT => baseCrt,
            StatType.SPD => baseSpd,
            _ => 0
        };
    }
    
    // 레벨 기준 기초스탯 초기화 
    public void InitializeStats()
    {
        maxExp = 100 * level;
        maxHp = 50 + 50 * level;
        baseAtk = 10 + 5 * level;
        baseDef = 10 + 5 * level;
        baseCrt = 1 * level;
        baseSpd = 5 + 2 * level;

        curExp = 0;
        curHp = maxHp;

        curAtk = baseAtk;
        curDef = baseDef;
        curCrt = baseCrt;
        curSpd = baseSpd;
    }
    
    // 장착 아이템 조회뒤 스탯변화치 계산
    public void ApplyModifiedStats(Dictionary<StatType, int> delta)
    {
        curAtk = baseAtk + (delta.ContainsKey(StatType.ATK) ? delta[StatType.ATK] : 0);
        curDef = baseDef + (delta.ContainsKey(StatType.DEF) ? delta[StatType.DEF] : 0);
        curCrt = baseCrt + (delta.ContainsKey(StatType.CRT) ? delta[StatType.CRT] : 0);
        curSpd = baseSpd + (delta.ContainsKey(StatType.SPD) ? delta[StatType.SPD] : 0);
    }
    
    #endregion
    
}
