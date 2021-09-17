using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyMissAttackCommandSO : CommandSO
{
    
    public override void Execute(Unit user, Unit target)
    {
        Debug.Log($"{user.name}の攻撃は失敗した:残りHP{target.hp}");
    }
}
