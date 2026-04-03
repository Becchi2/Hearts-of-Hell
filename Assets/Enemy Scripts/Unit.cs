using UnityEngine;

public class Unit : MonoBehaviour
{
    
    public string unitName;

    public int maxHP;
    public int currentHP;

    public int damage;

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;
        else
            return false;

    }

}
