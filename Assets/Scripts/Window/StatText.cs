using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatText : MonoBehaviour
{
    [SerializeField] string m_messageTextName = "MessageText";
    [SerializeField] BattleController bc = new BattleController();
    [SerializeField] GameObject panel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
            ShowMessage();
        }
    }
    
    void ShowMessage()
    {
        GameObject go = GameObject.Find(m_messageTextName);
        Text text = go?.GetComponent<Text>();

        if (text)
        {
            text.text = $"{bc.player.hp}";
            Debug.Log(text.text);
        }
    }
}
