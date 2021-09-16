using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    //playerとenemyのUnitクラスを持ってきてる
    [SerializeField] Unit player = default;
    [SerializeField] Unit enemy = default;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject spellPanel;
    [SerializeField] GameObject textPanel;
    [SerializeField]AttackCommandSO at;
    public bool left = true;
    public bool up = true;
    public bool spellleft = true;
    public bool spellup = true;
    public static int currentHP ; 
    bool mainpanelHantei = false;
    bool spellpanelHantei = false;
    [SerializeField] float animationTime = 3f;
    Animator m_anime;
    [SerializeField] GameObject m_attackEffect;
    [SerializeField] GameObject m_defenceEfect;
    public UItext uitext;
    bool textToPhase = false;
    //２２文字まで
    enum Phase
    {
        StartPhase,
        CommandPhase,
        SpellCommandPhase,
        ExcutePhase,
        Result,
        End,
    }
    Phase phase;

    enum Condition
    {
        Normal,
        Slept,
        Poison,
    }
    Condition condition;



    void Start()
    {
        phase = Phase.StartPhase;
        StartCoroutine(Battle());
        SelectableText st = new SelectableText();
        spellPanel.SetActive(false);
        player.hp = currentHP;
        m_anime = GetComponent<Animator>();
        StartCoroutine("FirstText");
    }
    public void StartLoad()
    {
        StartCoroutine("WaitTime");
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(animationTime);     
    }
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsSpace()) yield return 0;
    }


    IEnumerator Battle()
    {

        SelectableText st = new SelectableText();
        while (phase != Phase.End)
        {
            yield return null;
            Debug.Log(phase);
            switch (phase)
            {
                case Phase.StartPhase:
                    
                    //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    if (textToPhase ==true)
                    {
                        phase = Phase.CommandPhase;
                    }                    
                    break;
                case Phase.CommandPhase:
                    mainPanel.SetActive(true);
                    mainpanelHantei = true;
                    if (up == true && left == true && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[0];
                        enemy.serectCommand = enemy.commands[0];
                        phase = Phase.ExcutePhase;
                        enemy.target = player;
                        player.target = enemy;                       
                        up = true;
                        left = true;
                        Debug.Log("<color=yellow>こうげき！</color>");
                    }
                    else if (up == true && left == false && Input.GetKeyDown(KeyCode.Space))
                    {

                        phase = Phase.SpellCommandPhase;
                        up = true;
                        left = true;
                        Debug.Log("<color=red>呪文へ！</color>");
                    }
                    else if (up == false && left == false && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[2];
                        enemy.serectCommand = enemy.commands[0];
                        up = true;
                        left = true;
                        phase = Phase.ExcutePhase;
                        Debug.Log("<color=yellow>にげる！</color>");
                    }
                    else if(up == false && left == true && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[4];
                        enemy.serectCommand = enemy.commands[1];
                        enemy.target = player;
                        player.target = enemy;
                        phase = Phase.ExcutePhase;
                        up = true;
                        left = true;
                        Debug.Log("<color=green>ぼうぎょ！</color>");
                    }


                    break;
                case Phase.SpellCommandPhase:
                    mainpanelHantei = false;
                    mainPanel.SetActive(false);
                    spellpanelHantei = true;
                    spellPanel.SetActive(true);

                    if (spellup == true && spellleft == true && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[1];
                        enemy.serectCommand = enemy.commands[0];
                        phase = Phase.ExcutePhase;
                        enemy.target = player;
                        player.target = player;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellpanelHantei = false;
                        spellPanel.SetActive(false);
                        spellup = true;
                        spellleft = true;
                        Debug.Log("<color=green>ホロロン！</color>");
                    }
                    else if (spellup == true && spellleft == false && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[3];
                        enemy.serectCommand = enemy.commands[0];
                        enemy.target = player;
                        player.target = enemy;
                        phase = Phase.ExcutePhase;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellpanelHantei = false;
                        spellPanel.SetActive(false);
                        spellup = true;
                        spellleft = true;
                        Debug.Log("<color=red>ボボ</color>");
                    }
                    else if (spellup == false && spellleft == true && Input.GetKeyDown(KeyCode.Space))
                    {
                        phase = Phase.ExcutePhase;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellpanelHantei = false;
                        spellPanel.SetActive(false);
                        spellup = true;
                        spellleft = true;
                        Debug.Log("<color=blue>。。。</color>");
                    }
                    else if (spellup == false && spellleft == false && Input.GetKeyDown(KeyCode.Space))
                    {
                        //コマンドフェーズへ戻る
                        phase = Phase.CommandPhase;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellpanelHantei = false;
                        spellPanel.SetActive(false);
                        spellup = true;
                        spellleft = true;
                    }
                    break;
                case Phase.ExcutePhase:
                    if (player.serectCommand == player.commands[0])
                    {
                        //こうげき                        
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        StartCoroutine("AttackText");
                        Debug.Log("<color=red>こうげき！２</color>");
                    }
                    else if (player.serectCommand == player.commands[1])
                    {
                        //ホロロン
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        Debug.Log("<color=red>かいふく！２</color>");
                    }
                    else if (player.serectCommand == player.commands[2])
                    {
                        //にげる
                        SceneManager.LoadScene("field");
                    }
                    else if (player.serectCommand == player.commands[3])
                    {
                        //ゴヒロミ
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        
                    }
                    else if (player.serectCommand == player.commands[4])
                    {
                        //ぼうぎょ
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        StartCoroutine("DefenceText");
                    }



                    //敵かプレイヤーが死んだら
                    if (player.hp <= 0 || enemy.hp <= 0)
                    {
                        phase = Phase.Result;
                    }
                    else
                    {
                        phase = Phase.CommandPhase;
                    }

                    break;
                case Phase.Result:
                    currentHP = player.hp;
                    break;
                case Phase.End:
                    break;


            }
        }

        while (condition != Condition.Normal)
        {
            switch (condition)
            {
                case Condition.Slept:
                    break;
                case Condition.Poison:
                    break;
                case Condition.Normal:
                    break;
            }
        }
    }
    public void CursorMove()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            left = false;
            Debug.Log("→" + left + up);

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
            Debug.Log("←" + left + up);

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            up = false;
            Debug.Log("↓" + left + up);

        }
        else if (Input.GetKeyDown(KeyCode.W))
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

    void Update()
    {

        if (mainpanelHantei == true)
        {
            CursorMove();
        }
        else if(spellpanelHantei == true)
        {
            SpellCursorMove();
        }

    }
    IEnumerator FirstText()
    {
        uitext.DrawText($"{enemy.name}が現れた！");
        yield return StartCoroutine("Skip");
        uitext.DrawText($"{player.name}はどうする？");
        yield return StartCoroutine("Skip");
        textToPhase = true;
        textPanel.SetActive(false);
    }
    IEnumerator AttackText()
    {
        textPanel.SetActive(true);
        uitext.DrawText($"{player.name}のこうげき！");
        yield return StartCoroutine("Skip");
        Instantiate(m_attackEffect, this.gameObject.transform.position, Quaternion.identity);
        uitext.DrawText($"{enemy.name}は{at.attackPoint}のダメージを受けた");
        yield return StartCoroutine("Skip");
        textPanel.SetActive(false);
    }
    IEnumerator DefenceText()
    {
        textPanel.SetActive(true);
        uitext.DrawText($"{player.name}は身を守った！");
        yield return StartCoroutine("Skip");
        Instantiate(m_defenceEfect, m_defenceEfect.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(m_defenceEfect);
        textPanel.SetActive(false);
    }
}
