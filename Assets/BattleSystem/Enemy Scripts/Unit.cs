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
