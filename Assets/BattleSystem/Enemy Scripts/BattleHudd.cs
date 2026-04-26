using UnityEngine;
using UnityEngine.UI;
public class BattleHudd : MonoBehaviour
{

    public Slider hpslider;
    public Slider mpslider;

    public void SetHUD(Unit unit)
    {
        hpslider.maxValue = unit.maxHP;
        hpslider.value = unit.currentHP;

        mpslider.maxValue = unit.maxMP;
        mpslider.value = unit.currentMP;
    }

    public void SetHP(int hp)
    {
        hpslider.value = hp;
    }

    public void SetMP(int mp)
    {
        mpslider.value = mp;
    }
}