﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlockCommandSO : CommandSO
{
    public override void Execute(Unit user, Unit target)
    {
        
        Debug.Log($"{user.name}は身を守った！");
    }
}
