using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tottekuru : MonoBehaviour
{
    [SerializeField] BattleController bc = new BattleController();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(bc.player.hp);
    }
}
