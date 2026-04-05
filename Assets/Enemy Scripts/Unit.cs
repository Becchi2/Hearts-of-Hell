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

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;
        else
            return false;

    }

    public bool TakeMagicDamage(int magicDamage)
    {
        currentHP -= magicDamage;
        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
