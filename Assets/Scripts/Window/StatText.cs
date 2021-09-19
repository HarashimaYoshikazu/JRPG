using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatText : MonoBehaviour
{
    [SerializeField] string m_messageTextName = "MessageText";
    [SerializeField] BattleController bc = new BattleController();
    [SerializeField] GameObject panel;
    int hp;
    bool isPanel;
    PlayerScript ps = new PlayerScript();
    private void Start()
    {
        hp = 20;
        
    }
    private void Update()
    {
        if (isPanel ==false &&Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
            ShowMessage();
            isPanel = true;
        }
        if (isPanel == true && Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(false);
            isPanel = false;
        }
        if (ps.isCombat ==true)
        {
            hp = PlayerPrefs.GetInt("playerHP");
            Debug.Log("hp書き換え");
        }
    }
    
    void ShowMessage()
    {
        GameObject go = GameObject.Find(m_messageTextName);
        Text text = go?.GetComponent<Text>();

        
            text.text = $"{hp}";
            Debug.Log(text.text);
        
    }
}
