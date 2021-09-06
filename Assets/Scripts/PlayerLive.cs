using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLive : MonoBehaviour
{
    public int pHP = 20;
    public int pMP = 0;
    public int pAT = 5;
    public int pDF = 5;
    public int pMG = 0;
    public int pDEX = 3;
    public void Death()
    {
        if (pHP == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
