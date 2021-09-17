using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    //playerとenemyのUnitクラスを持ってきてる
    [SerializeField]public Unit player = default;
    [SerializeField]public Unit enemy = default;
    [SerializeField] Animator bat_anime = default;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject spellPanel;
    [SerializeField] GameObject textPanel;
    [SerializeField]AttackCommandSO at;
    [SerializeField] FireCommandSO fire;
    [SerializeField] HealCommandSO heal;
    [SerializeField] AttackCommandSO enemyAt;
    [SerializeField] HealCommandSO enemyheal;
    public bool left = true;
    public bool up = true;
    public bool spellleft = true;
    public bool spellup = true;
    bool mainpanelHantei = false;
    bool spellpanelHantei = false;
    [SerializeField] float animationTime = 3f;
    Animator m_anime;
    [SerializeField] GameObject m_attackEffect;
    [SerializeField] GameObject m_defenceEfect;
    [SerializeField] GameObject m_fireEffect;
    [SerializeField] GameObject m_biteEffect;
    [SerializeField] GameObject m_wingEffect;
    public UItext uitext;
    bool textToPhase = false;
    bool isText = false;
    bool comm = false;
    bool isPlay= false;
    
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
                    //                   
                        mainPanel.SetActive(true);
                        mainpanelHantei = true;
                    

                    if (up == true && left == true && Input.GetKeyDown(KeyCode.Space) )
                    {
                        player.serectCommand = player.commands[0];
                        //enemy.serectCommand = enemy.commands[0];
                        phase = Phase.ExcutePhase;
                        enemy.target = player;
                        player.target = enemy;                       
                        up = true;
                        left = true;
                        comm = true;
                        
                        Debug.Log("<color=yellow>こうげき！</color>");
                    }
                    else if (up == true && left == false && Input.GetKeyDown(KeyCode.Space) )
                    {

                        phase = Phase.SpellCommandPhase;
                        up = true;
                        left = true;
                        
                        Debug.Log("<color=red>呪文へ！</color>");
                    }
                    else if (up == false && left == false && Input.GetKeyDown(KeyCode.Space) )
                    {
                        player.serectCommand = player.commands[2];
                        //enemy.serectCommand = enemy.commands[0];
                        up = true;
                        left = true;
                        comm = true;
                        phase = Phase.ExcutePhase;
                        Debug.Log("<color=yellow>にげる！</color>");
                    }
                    else if(up == false && left == true && Input.GetKeyDown(KeyCode.Space) )
                    {
                        player.serectCommand = player.commands[4];
                        enemy.serectCommand = enemy.commands[3];
                        enemy.target = player;
                        player.target = enemy;
                        phase = Phase.ExcutePhase;
                        up = true;
                        left = true;
                        comm = true;
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
                        //enemy.serectCommand = enemy.commands[0];
                        phase = Phase.ExcutePhase;
                        enemy.target = player;
                        player.target = player;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellpanelHantei = false;
                        spellPanel.SetActive(false);
                        spellup = true;
                        spellleft = true;
                        comm = true;
                        Debug.Log("<color=green>ホロロン！</color>");
                    }
                    else if (spellup == true && spellleft == false && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[3];
                        //enemy.serectCommand = enemy.commands[0];
                        enemy.target = player;
                        player.target = enemy;
                        phase = Phase.ExcutePhase;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellpanelHantei = false;
                        spellPanel.SetActive(false);
                        spellup = true;
                        spellleft = true;
                        comm = true;
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
                        comm = true;
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
                    int randum = Random.Range(0, 3);
                    if (!enemy.serectCommand == enemy.commands[3])
                    {
                        enemy.serectCommand = enemy.commands[randum];
                    }
                    

                    if (isText ==false)
                    {
                        mainPanel.SetActive(true);
                        mainpanelHantei = true;
                    }
                    if (player.serectCommand == player.commands[0] && isText ==false&&comm ==true)
                    {
                        //こうげき
                        comm = false;
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        
                        StartCoroutine("AttackText");
                       
                        


                        Debug.Log("<color=red>こうげき！２</color>");
                    }
                    else if (player.serectCommand == player.commands[1] && comm == true && isText == false)
                    {
                        //ホロロン
                        comm = false;
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        StartCoroutine("HealText");
                        Debug.Log("<color=red>かいふく！２</color>");
                    }
                    else if (player.serectCommand == player.commands[2] && comm == true && isText == false)
                    {
                        //にげる
                        comm = false;
                        StartCoroutine("RunText");
                        
                    }
                    else if (player.serectCommand == player.commands[3] && comm == true && isText == false)
                    {
                        //ボボ
                        comm = false;
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        StartCoroutine("FireText");
                    }
                    else if (player.serectCommand == player.commands[4] && comm == true && isText == false)
                    {
                        //ぼうぎょ
                        
                        comm = false;
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
                        StartCoroutine("DefenceText");
                    }

                    if (isPlay == true && enemy.serectCommand == enemy.commands[3])
                    {
                        //miss
                        StartCoroutine("EnemyMissText");
                    }
                    else if (isPlay == true && randum == 0)
                    {
                        //かみつき
                        StartCoroutine("EnemyAttackText");
                    }
                     else if (isPlay == true && randum == 1)
                    {
                        //はばたき
                        StartCoroutine("EnemyAttackText2");
                    }
                    else if(isPlay ==true && randum == 2)
                    {
                        //羽休め
                        StartCoroutine("EnemyAttackText3");
                    }
                    

                    //敵かプレイヤーが死んだら
                    if (player.hp <= 0 || enemy.hp <= 0)
                    {
                        phase = Phase.Result;
                    }
                    else if(isText ==false)
                    {
                        phase = Phase.CommandPhase;
                    }

                    break;
                case Phase.Result:

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

        if (mainpanelHantei == true && spellpanelHantei==false)
        {
            CursorMove();
        }
        else if(spellpanelHantei == true && mainpanelHantei==false)
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
        isText = true;
        textPanel.SetActive(true);
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        
        uitext.DrawText($"{player.name}のこうげき！");
        yield return StartCoroutine("Skip");
        Instantiate(m_attackEffect, this.gameObject.transform.position, Quaternion.identity);

        uitext.DrawText($"{enemy.name}は{at.attackPoint}のダメージを受けた");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isPlay = true;
        isText = false;
    }
    IEnumerator DefenceText()
    {
        isText = true;
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        textPanel.SetActive(true);

        uitext.DrawText($"{player.name}のぼうぎょ！");
        yield return StartCoroutine("Skip");
        Instantiate(m_defenceEfect, m_defenceEfect.transform.position, Quaternion.identity);

        uitext.DrawText($"{player.name}は身を守っている");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isPlay = true;
        isText = false;
    }
    IEnumerator FireText()
    {
        isText = true;
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        textPanel.SetActive(true);

        uitext.DrawText($"{player.name}はボボをくりだした！");
        yield return StartCoroutine("Skip");
        Instantiate(m_fireEffect, m_fireEffect.transform.position, Quaternion.identity);

        uitext.DrawText($"{enemy.name}は{fire.wisdom}のダメージ");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isPlay = true;
        isText = false;
    }
    IEnumerator HealText()
    {
        isText = true;
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        textPanel.SetActive(true);

        uitext.DrawText($"{player.name}のホロロン！");
        yield return StartCoroutine("Skip");
        Instantiate(m_fireEffect, m_fireEffect.transform.position, Quaternion.identity);
        uitext.DrawText($"{player.name}はHPが{heal.healPoint}回復した！");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isPlay = true;
        isText = false;
    }

    IEnumerator RunText()
    {
        isText = true;
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        textPanel.SetActive(true);

        uitext.DrawText($"{player.name}はにげだした！");
        yield return StartCoroutine("Skip");
        
        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isText = false;
        SceneManager.LoadScene("field");
    }
    IEnumerator EnemyAttackText()
    {
        isPlay = false;
        isText = true;
        textPanel.SetActive(true);
        mainpanelHantei = false;
        mainPanel.SetActive(false);

        uitext.DrawText($"{enemy.name}のかみつき！");
        yield return StartCoroutine("Skip");
        Instantiate(m_biteEffect, m_biteEffect.transform.position, Quaternion.identity);

        uitext.DrawText($"{player.name}は{enemyAt.attackPoint}のダメージを受けた");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isText = false;
        
    }
    IEnumerator EnemyAttackText2()
    {
        isPlay = false;
        isText = true;
        textPanel.SetActive(true);
        mainpanelHantei = false;
        mainPanel.SetActive(false);

        uitext.DrawText($"{enemy.name}のはばたきこうげき！");
        yield return StartCoroutine("Skip");
        Instantiate(m_wingEffect, m_wingEffect.transform.position, Quaternion.identity);

        uitext.DrawText($"{player.name}は{enemyAt.attackPoint}のダメージを受けた");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isText = false;
        Debug.Log("enemy");
    }
    IEnumerator EnemyAttackText3()
    {
        isPlay = false;
        isText = true;
        textPanel.SetActive(true);
        mainpanelHantei = false;
        mainPanel.SetActive(false);

        uitext.DrawText($"{enemy.name}は羽をやすめている...");
        yield return StartCoroutine("Skip");
        
        bat_anime.Play("WingStopAnimation");
        uitext.DrawText($"{enemy.name}のHPが{enemyheal.healPoint}回復した！");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isText = false;
        Debug.Log("enemy");
    }
    IEnumerator EnemyMissText()
    {
        isPlay = false;
        isText = true;
        textPanel.SetActive(true);
        mainpanelHantei = false;
        mainPanel.SetActive(false);

        uitext.DrawText($"{enemy.name}のこうげき！");
        yield return StartCoroutine("Skip");

        uitext.DrawText($"ミス！ダメージをあたえられない！");
        yield return StartCoroutine("Skip");

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isText = false;
    }
}
