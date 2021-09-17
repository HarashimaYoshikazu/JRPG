using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class HealCommandSO : CommandSO
{
    [SerializeField] public int healPoint;

    //CommandSOの関数をオーバーライドしてきた
    public override void Execute(Unit user, Unit target)
    {
        target.hp += healPoint;
        Debug.Log($"{target.name}のホロロン！{healPoint}回復！:残りHP{target.hp}");
    }
}
