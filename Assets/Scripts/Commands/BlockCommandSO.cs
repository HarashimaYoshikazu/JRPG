using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlockCommandSO : CommandSO
{
    [SerializeField] int def;

    public override void Execute(Unit user, Unit target)
    {
        //target.hp -= wisdom;
        Debug.Log($"{user.name}は身を守った！");
    }
}
