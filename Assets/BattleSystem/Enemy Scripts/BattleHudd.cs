using UnityEngine;
using UnityEngine.UI;
public class BattleHudd : MonoBehaviour
{

    public Slider hpslider;// reference to the health slider UI element
    public Slider mpslider;// reference to the mana slider UI element

    public void SetHUD(Unit unit)// sets the health and mp sliders to the unit's current health and mp values
    {
        hpslider.maxValue = unit.maxHP;
        hpslider.value = unit.currentHP;

        mpslider.maxValue = unit.maxMP;
        mpslider.value = unit.currentMP;
    }

    public void SetHP(int hp)
    {
        hpslider.value = hp; // updates the health slider to the specified value
    }

    public void SetMP(int mp)
    {
        mpslider.value = mp; // updates the mana slider to the specified value
    }
}