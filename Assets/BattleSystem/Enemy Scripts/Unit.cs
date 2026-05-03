using UnityEngine;

public class Unit : MonoBehaviour
{
    
    public string unitName;

    public int maxHP;
    public int currentHP;

    public int damage;
    public int magicDamage;

    public int maxMP;
    public int currentMP;
    public int mpCost;

    public int maxLimit;
    public int limit;

    public int attackBuff;

    public int attackBuffValue = 10;
    public int attackBuffTurns = 0;
    public int buffMPCost = 15;

    public int bleeding;
    public int reflect;
    void Awake()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    public bool checkHealth()
    {
        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void UseMP(int mpCost)
    {
        currentMP -= mpCost;
    }

    public bool IsMPAvailable(int mpCost)
    {
        return currentMP >= mpCost;
    }


}
