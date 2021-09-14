using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandSO : ScriptableObject
{
    public new string name;


    //virtualすることでオーバーライド可能に
    //abstruct（強制）
    public virtual void Execute(Unit user, Unit target)
    {
        //target.hp -= at;
        //$マークでまとめて変数かける
        // Debug.Log($"{user.name}の攻撃:{target.name}に{3}のダメージ:残りHP{target.hp}");
    }
}
