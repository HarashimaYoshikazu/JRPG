using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireCommandSO : CommandSO
{
    [SerializeField] public int wisdom;

    public override void Execute(Unit user, Unit target)
    {
        target.hp -= wisdom;
        Debug.Log($"{user.name}のボボ！:{target.name}に{wisdom}のダメージ:残りHP{target.hp}");
    }
}
