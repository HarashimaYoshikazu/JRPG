using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTest : MonoBehaviour
{
     static int m_PlayerHP;
    public int PlayerHP {
        get
        {
            return m_PlayerHP;
        }
        set
        {
            m_PlayerHP = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(m_PlayerHP);
        }
    }
}
