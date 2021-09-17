using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] float destroyTime = 2f;
    void Start()
    {
        Destroy(this.gameObject,destroyTime);
    }
}
