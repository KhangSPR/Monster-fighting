using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/MoreAbility" , fileName = "MoreAbility")]
public class More_Ability : Ability
{
    [Header("public variable")]
    public int moreMoney_Plus;
    public int moreHp_Plus;
    public int moreMoneyGrowth_Plus;
    public int moreSlot_Plus;
    public override void Active(GameObject parent)
    {
        int startMoney = GameObject.FindFirstObjectByType<GameManager>().Currency;
        Debug.Log("startMoney = " + startMoney);
        GameObject.FindFirstObjectByType<GameManager>().Currency = startMoney + money_PlusDefault + moreMoney_Plus;
        Debug.Log("You are using More Start Money");
    }
}
