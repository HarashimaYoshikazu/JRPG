using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SleptConditionSO : ConditionSO
{
    public override void StatExecute(Unit user)
    {
        
        Debug.Log($"{user.name}は眠り状態:");
    }
}
