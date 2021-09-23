using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireCommandSO : CommandSO
{
    [SerializeField] public int wisdom;
    [SerializeField] public int useMp;

    public override void Execute(Unit user, Unit target)
    {
        target.hp -= wisdom;
        user.mp -= useMp;
        Debug.Log($"{user.name}のボボ！:{target.name}に{wisdom}のダメージ:残りHP{target.hp}");
    }
}
