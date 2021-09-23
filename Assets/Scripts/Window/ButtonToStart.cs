using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToStart : MonoBehaviour
{

    public void PushButton()
    {
        SceneManager.LoadScene("Heya");
    }
}
