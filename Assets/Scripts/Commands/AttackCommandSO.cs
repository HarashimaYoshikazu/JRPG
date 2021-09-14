using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackCommandSO : CommandSO
{
    [SerializeField] int attackPoint;

    public override void Execute(Unit user, Unit target)
    {
        target.hp -= attackPoint;
        Debug.Log($"{user.name}の攻撃:{target.name}に{attackPoint}のダメージ:残りHP{target.hp}");
    }

}
