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
        //実行内容
    }
}
