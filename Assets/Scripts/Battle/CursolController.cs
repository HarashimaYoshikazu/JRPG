using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursolController : MonoBehaviour
{
    public bool left;
    public bool up;
    public bool spellleft;
    public bool spellup;
    // Start is called before the first frame update
    public void CursorMove()
    {

        if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            left = false;
            Debug.Log("→" + left + up);
            
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left = true;
            Debug.Log("←" + left + up);
            
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            up = false;
            Debug.Log("↓" + left + up);
            
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            up = true;
            Debug.Log("↑" + left + up);
           
        }

    }

    public void SpellCursorMove()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            spellleft = false;
            Debug.Log("→" + spellleft + spellup);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            spellleft = true;
            Debug.Log("←" + spellleft + spellup);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            spellup = false;
            Debug.Log("↓" + spellleft + spellup);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            spellup = true;
            Debug.Log("↑" + spellleft + spellup);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
