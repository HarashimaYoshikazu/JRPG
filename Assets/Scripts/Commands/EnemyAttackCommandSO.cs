using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class EnemyAttackCommandSO : CommandSO
{
    [SerializeField] public int attackPoint;

    public override void Execute(Unit user, Unit target)
    {

        target.hp = PlayerPrefs.GetInt("playerHP") - attackPoint;
        PlayerPrefs.SetInt("playerHP",target.hp);
        Debug.Log($"{user.name}の攻撃:{target.name}に{attackPoint}のダメージ:残りHP{target.hp}");
    }

}
