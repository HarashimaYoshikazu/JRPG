using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScene : MonoBehaviour
{
    [SerializeField] string scene = "Heya";
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "upStair")
        {
            SceneManager.LoadScene(scene);
        }
    }
}
