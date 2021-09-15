using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PoisonConditionSO : ConditionSO
{

    public int poisonDamage;
    public override void StatExecute(Unit user)
    {
        user.hp-=poisonDamage;
        Debug.Log($"{user.name}は毒状態:{poisonDamage}のダメージを受けた");
    }
}
