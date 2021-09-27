using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    StaticTest staticTest;
    // Start is called before the first frame update
    void Start()
    {
        staticTest = GetComponent<StaticTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            staticTest.PlayerHP = 10;
        }
    }
}
