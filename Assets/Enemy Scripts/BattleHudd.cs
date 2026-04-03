using UnityEngine;
using UnityEngine.UI;   
public class BattleHudd : MonoBehaviour
{

    public Slider hpslider;

    public void SetHUD(Unit unit)
    {
        hpslider.maxValue = unit.maxHP;
        hpslider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpslider.value = hp;
    }
}
