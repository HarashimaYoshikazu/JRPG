using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat : MonoBehaviour
{
    PlayerLive pl = new PlayerLive();
    enemy en = new enemy();
    bool Turn;
    [SerializeField] GameObject FirstMS;
    [SerializeField] GameObject SecondMS;


    void Start()
    {
        Turn = true;
    }

    void Update()
    {
        if (Turn == false)
        {
            EnemyTurn();
        }
    }
    
    public void PushAttackButton()
    {
        
        FirstMS.SetActive(false);
        SecondMS.SetActive(true);
        PlayerTurn();
        
    }   
    public void PlayerTurn()
    {
        
        
        int damage = pl.pAT / 2 - en.eDF / 4;
        
        if (damage > 0)
        {
            en.eHP -= damage;
        }
        else
        {
            en.eHP -= 1;
        }
        Debug.Log("敵のHPは"+en.eHP);
        Turn = false;
    }
    public void EnemyTurn()
    {
        
        
        int damage = en.eAT / 2 - pl.pDF;
        if (damage>0)
        {
            pl.pHP -= damage;
        }
        else
        {
            pl.pHP -= 1;
        }
        Debug.Log("プレイヤーのHPは"+pl.pHP);
        Turn = true;


    }
    
}
