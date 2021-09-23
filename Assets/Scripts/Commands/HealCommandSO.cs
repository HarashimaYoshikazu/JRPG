using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class HealCommandSO : CommandSO
{
    [SerializeField] public int healPoint;
    [SerializeField] public int useMP;

    //CommandSOの関数をオーバーライドしてきた
    public override void Execute(Unit user, Unit target)
    {
        target.hp += healPoint;
        user.mp -= useMP;
        Debug.Log($"{target.name}の回復行動！{healPoint}回復！:残りHP{target.hp}");
    }
}
