using UnityEngine;

public enum StatType
{
    ATK,
    DEF,
    CRT,
    SPD,
}

public class StatManager : MonoBehaviour
{
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
}
