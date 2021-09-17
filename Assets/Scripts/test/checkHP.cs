using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkHP : MonoBehaviour
{
    [SerializeField] Unit p = default;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(p.hp);
    }
}
