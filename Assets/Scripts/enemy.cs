using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
   public int eHP = 10;
   public int eAT = 5;
   public int eDF = 5;
   public int eSP = 3;

    public void Death()
    {
        if (eHP == 0)
        {
            Destroy(this.gameObject);
        }
    }
        
    
    

}
