using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    //playerとenemyのUnitクラスを持ってきてる
    [Header("ユニット")]
    [SerializeField]public Unit player = default;
    [SerializeField]public Unit enemy = default;
    [Header("アニメーター")]
    [SerializeField] Animator bat_anime = default;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject spellPanel;
    [SerializeField] GameObject textPanel;
    [SerializeField]AttackCommandSO at;
    [SerializeField] FireCommandSO fire;
    [SerializeField] FireCommandSO ice;
    [SerializeField] HealCommandSO heal;
    [SerializeField] EnemyAttackCommandSO batBaite;
    [SerializeField] EnemyAttackCommandSO batWing;
    [SerializeField] HealCommandSO BatHeal;
    public bool left = true;
    public bool up = true;
    public bool spellleft = true;
    public bool spellup = true;
    bool mainpanelHantei = false;
    bool spellpanelHantei = false;
    [SerializeField] float animationTime = 3f;
    Animator m_anime;
    [Header("エフェクト")]
    [SerializeField] GameObject m_attackEffect;
    [SerializeField] GameObject m_defenceEfect;
    [SerializeField] GameObject m_fireEffect;
    [SerializeField] GameObject m_biteEffect;
    [SerializeField] GameObject m_wingEffect;
    [SerializeField] GameObject m_healEffect;
    [SerializeField] GameObject m_iceEffect;
    public UItext uitext;
    bool textToPhase = false;
    bool isText = false;
    bool comm = false;
    bool isPlay= false;
    bool isWin = false;
    PlayerScript ps;
    AudioSource audioSource;
    [Header("サウンド")]
    [SerializeField] AudioSource kettei;
    [SerializeField] AudioSource koumoriSound;
    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource missSound;
    [SerializeField] AudioSource victorySound;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource fieldBgm;
    [SerializeField] GameObject monstar;
    [SerializeField] string sceneName = "Combat";

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




    void Start()
    {
        phase = Phase.StartPhase;
        StartCoroutine(Battle());
        SelectableText st = new SelectableText();
        spellPanel.SetActive(false);
        m_anime = GetComponent<Animator>();
        StartCoroutine("FirstText");
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        audioSource = GetComponent<AudioSource>();
        //kettei = GameObject.Find("Soundkettei").GetComponent<AudioSource>();
        player.hp = PlayerPrefs.GetInt("playerHP");
        player.mp = PlayerPrefs.GetInt("playerMP");
        fieldBgm = GameObject.Find("FieldBGM").GetComponent<AudioSource>();
        fieldBgm.Stop();
        
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
        while (phase != Phase.End)
        {
            yield return null;
            Debug.Log(phase);
            switch (phase)
            {
                case Phase.StartPhase:
                    
                    if (textToPhase ==true)
                    {
                        phase = Phase.CommandPhase;
                    }                    
                    break;
                case Phase.CommandPhase:
                    //                   
                    mainPanel.SetActive(true);
                    mainpanelHantei = true;


                    if (up == true && left == true && Input.GetKeyDown(KeyCode.Space)&&mainpanelHantei ==true )
                    {
                        kettei.Play();
                        player.serectCommand = player.commands[0];
                        //enemy.serectCommand = enemy.commands[0];
                        phase = Phase.ExcutePhase;
                        enemy.target = player;
                        player.target = enemy;                       
                        up = true;
                        left = true;
                        comm = true;
                        kettei.Play();
                        Debug.Log("<color=yellow>こうげき！</color>");
                    }
                    else if (up == true && left == false && Input.GetKeyDown(KeyCode.Space) && mainpanelHantei == true)
                    {
                        kettei.Play();
                        phase = Phase.SpellCommandPhase;
                        up = true;
                        left = true;
                        kettei.Play();
                        Debug.Log("<color=red>呪文へ！</color>");
                    }
                    else if (up == false && left == false && Input.GetKeyDown(KeyCode.Space) && mainpanelHantei == true)
                    {
                        kettei.Play();
                        player.serectCommand = player.commands[2];
                        //enemy.serectCommand = enemy.commands[0];
                        up = true;
                        left = true;
                        comm = true;
                        phase = Phase.ExcutePhase;
                        Debug.Log("<color=yellow>にげる！</color>");
                    }
                    else if(up == false && left == true && Input.GetKeyDown(KeyCode.Space) && mainpanelHantei == true)
                    {
                        kettei.Play();
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

                    
                     if (spellup == true && spellleft == true && Input.GetKeyDown(KeyCode.Space) && player.mp >= 2)
                    {
                        
                        
                            kettei.Play();
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
                    else if (spellup == true && spellleft == false && Input.GetKeyDown(KeyCode.Space) && player.mp >= 3)
                    {
                        
                            kettei.Play();
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
                    else if (spellup == false && spellleft == true && Input.GetKeyDown(KeyCode.Space)&&player.mp>=2)
                    {
                        
                        
                            player.serectCommand = player.commands[5];
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
                            Debug.Log("<color=blue>ポッキン</color>");
                        
                        
                    }
                    else if (spellup == false && spellleft == false && Input.GetKeyDown(KeyCode.Space))
                    {
                        kettei.Play();
                        //コマンドフェーズへ戻る
                        phase = Phase.CommandPhase;
                        spellpanelHantei = false;
                        spellPanel.SetActive(false);
                        spellup = true;
                        spellleft = true;
                    }
                    break;
                case Phase.ExcutePhase:
                    int randum = Random.Range(0, 3);
                    int exrandum = Random.Range(-2, 3);
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
                        
                            StartCoroutine("AttackText");
                                              
                        Debug.Log("<color=red>こうげき！２</color>");
                    }
                    else if (player.serectCommand == player.commands[1] && comm == true && isText == false)
                    {
                        //ホロロン
                        comm = false;
                        StartCoroutine("HealText");
                        player.serectCommand.Execute(player, player.target);
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
                        StartCoroutine("FireText");
                        player.serectCommand.Execute(player, player.target);
                        
                       
                    }
                    else if (player.serectCommand == player.commands[4] && comm == true && isText == false)
                    {
                        //ぼうぎょ
                        
                        comm = false;
                        StartCoroutine("DefenceText");
                        player.serectCommand.Execute(player, player.target);
                        
                       
                    }
                    else if (player.serectCommand == player.commands[5] && comm == true && isText == false)
                    {
                        //ポッキン

                        comm = false;
                        StartCoroutine("IceText");
                        player.serectCommand.Execute(player, player.target);


                    }

                    if (enemy.hp > 0)
                    {
                        if (isPlay == true && enemy.serectCommand == enemy.commands[3])
                        {
                            //miss
                            enemy.serectCommand.Execute(enemy, enemy.target);
                            StartCoroutine("EnemyMissText");
                        }
                        else if (isPlay == true && randum == 0)
                        {
                            //かみつき
                            enemy.serectCommand.Execute(enemy, player);
                            StartCoroutine("EnemyAttackText");
                        }
                        else if (isPlay == true && randum == 1)
                        {
                            //はばたき
                            enemy.serectCommand.Execute(enemy, player);
                            StartCoroutine("EnemyAttackText2");
                        }
                        else if (isPlay == true && randum == 2)
                        {
                            //羽休め
                            enemy.serectCommand.Execute(enemy, enemy);
                            StartCoroutine("EnemyAttackText3");
                        }
                    }
                    
                    
                    //敵かプレイヤーが死んだら
                    if (player.hp <= 0 || enemy.hp <= 0 )
                    {
                        
                        phase = Phase.Result;
                    }
                    else if(isText ==false)
                    {
                        phase = Phase.CommandPhase;
                    }

                    break;
                case Phase.Result:
                    //StartCoroutine("WinText");
                    break;
                case Phase.End:
                    break;


            }
        }

      
    }
    public void CursorMove()
    {

        if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            left = false;
            Debug.Log("→" + left + up);
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left = true;
            Debug.Log("←" + left + up);
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            up = false;
            Debug.Log("↓" + left + up);
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            up = true;
            Debug.Log("↑" + left + up);
            audioSource.Play();
        }

    }

    public void SpellCursorMove()
    {

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            spellleft = false;
            Debug.Log("→" + spellleft + spellup);
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spellleft = true;
            Debug.Log("←" + spellleft + spellup);
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            spellup = false;
            Debug.Log("↓" + spellleft + spellup);
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            spellup = true;
            Debug.Log("↑" + spellleft + spellup);
            audioSource.Play();
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
        audioSource.Play();
        uitext.DrawText($"{player.name}はどうする？");
        yield return StartCoroutine("Skip");
        audioSource.Play();
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
        audioSource.Play();
        Instantiate(m_attackEffect, this.gameObject.transform.position, Quaternion.identity);

        uitext.DrawText($"{enemy.name}は{at.attackPoint}のダメージを受けた");
        yield return StartCoroutine("Skip");
        audioSource.Play();
        if (enemy.hp <=0)
        {
            bgm.Stop();
            monstar.SetActive(false);
            victorySound.Play();
            uitext.DrawText($"{enemy.name}をたおした！");
            yield return StartCoroutine("Skip");
            fieldBgm.Play();
            audioSource.Play();
            ps.isMove = true;
            ps.speed = 3f;
            ps.m_panelanime.Play("paneldefault");
            ps.eventSystem.SetActive(true);
            ps.isCombat = false;
            PlayerPrefs.SetInt("playerHP", player.hp);
            PlayerPrefs.SetInt("playerMP", player.mp);
            SceneManager.UnloadSceneAsync(sceneName);
        }
        else
        {
            textPanel.SetActive(false);
            mainPanel.SetActive(true);
            mainpanelHantei = true;
            isPlay = true;
            isText = false;
        }           
    }
    IEnumerator DefenceText()
    {
        isText = true;
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        textPanel.SetActive(true);

        uitext.DrawText($"{player.name}のぼうぎょ！");
        yield return StartCoroutine("Skip");
        audioSource.Play();
        Instantiate(m_defenceEfect, m_defenceEfect.transform.position, Quaternion.identity);

        uitext.DrawText($"{player.name}は身を守っている");
        yield return StartCoroutine("Skip");
        audioSource.Play();
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

        if (enemy.hp <= 0)
        {
            bgm.Stop();
            monstar.SetActive(false);
            victorySound.Play();
            uitext.DrawText($"{enemy.name}をたおした！");
            yield return StartCoroutine("Skip");
            fieldBgm.Play();
            ps.isMove = true;
            ps.speed = 3f;
            ps.m_panelanime.Play("paneldefault");
            ps.eventSystem.SetActive(true);
            ps.isCombat = false;
            PlayerPrefs.SetInt("playerHP", player.hp);
            PlayerPrefs.SetInt("playerMP", player.mp);
            SceneManager.UnloadSceneAsync(sceneName);
        }
        else
        {
            textPanel.SetActive(false);
            mainPanel.SetActive(true);
            mainpanelHantei = true;
            isPlay = true;
            isText = false;
        }
        
    }

    IEnumerator IceText()
    {
        isText = true;
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        textPanel.SetActive(true);

        uitext.DrawText($"{player.name}はポッキンをくりだした！");
        yield return StartCoroutine("Skip");
        Instantiate(m_iceEffect, m_iceEffect.transform.position, Quaternion.identity);

        uitext.DrawText($"{enemy.name}は{ice.wisdom}のダメージ");
        yield return StartCoroutine("Skip");

        if (enemy.hp <= 0)
        {
            bgm.Stop();
            monstar.SetActive(false);
            victorySound.Play();
            uitext.DrawText($"{enemy.name}をたおした！");
            yield return StartCoroutine("Skip");
            fieldBgm.Play();
            ps.isMove = true;
            ps.speed = 3f;
            ps.m_panelanime.Play("paneldefault");
            ps.eventSystem.SetActive(true);
            ps.isCombat = false;
            PlayerPrefs.SetInt("playerHP", player.hp);
            PlayerPrefs.SetInt("playerMP", player.mp);
            SceneManager.UnloadSceneAsync(sceneName);
        }
        else
        {
            textPanel.SetActive(false);
            mainPanel.SetActive(true);
            mainpanelHantei = true;
            isPlay = true;
            isText = false;
        }

    }

    IEnumerator HealText()
    {
        isText = true;
        mainpanelHantei = false;
        mainPanel.SetActive(false);
        textPanel.SetActive(true);

        uitext.DrawText($"{player.name}のホロロン！");
        yield return StartCoroutine("Skip");
        Instantiate(m_healEffect, m_healEffect.transform.position, Quaternion.identity);
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

        fieldBgm.Play();
        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isText = false;
        //yield return new WaitForSeconds(2f);
        ps.isMove = true;
        ps.speed = 3f;
        ps.m_panelanime.Play("paneldefault");
        ps.eventSystem.SetActive(true);
        ps.isCombat = false;
        PlayerPrefs.SetInt("playerHP",player.hp);
        PlayerPrefs.SetInt("playerMP", player.mp);
        SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
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
        audioSource.Play();
        Instantiate(m_biteEffect, m_biteEffect.transform.position, Quaternion.identity);

        uitext.DrawText($"{player.name}は{batBaite.attackPoint}のダメージを受けた");
        yield return StartCoroutine("Skip");
        audioSource.Play();
        if (player.hp <=0)
        {
            gameOverSound.Play();
            uitext.DrawText($"{player.name}はちからつきた・・・");
            yield return StartCoroutine("Skip");
            audioSource.Play();
            ps.isMove = true;
            ps.speed = 3f;
            ps.m_panelanime.Play("paneldefault");
            ps.eventSystem.SetActive(true);
            ps.isCombat = false;
            PlayerPrefs.SetInt("playerHP", player.hp);
            PlayerPrefs.SetInt("playerMP", player.mp);
            SceneManager.LoadSceneAsync("Title");
        }
        else
        {
            textPanel.SetActive(false);
            mainPanel.SetActive(true);
            mainpanelHantei = true;
            isText = false;
        }       
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
        audioSource.Play();
        Instantiate(m_wingEffect, m_wingEffect.transform.position, Quaternion.identity);

        uitext.DrawText($"{player.name}は{batWing.attackPoint}のダメージを受けた");
        yield return StartCoroutine("Skip");
        audioSource.Play();

        if (player.hp <= 0)
        {
            gameOverSound.Play();
            uitext.DrawText($"{player.name}はちからつきた・・・");
            yield return StartCoroutine("Skip");
            audioSource.Play();
            ps.isMove = true;
            ps.speed = 3f;
            ps.m_panelanime.Play("paneldefault");
            ps.eventSystem.SetActive(true);
            ps.isCombat = false;
            PlayerPrefs.SetInt("playerHP", player.hp);
            PlayerPrefs.SetInt("playerMP", player.mp);
            SceneManager.LoadSceneAsync("Title");
        }
        else
        {
            textPanel.SetActive(false);
            mainPanel.SetActive(true);
            mainpanelHantei = true;
            isText = false;
        }
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
        audioSource.Play();

        koumoriSound.Play();
        bat_anime.Play("WingStopAnimation");
        uitext.DrawText($"{enemy.name}のHPが{BatHeal.healPoint}回復した！");
        yield return StartCoroutine("Skip");
        audioSource.Play();

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
        audioSource.Play();

        uitext.DrawText($"ミス！ダメージをあたえられない！");
        yield return StartCoroutine("Skip");
        audioSource.Play();
        missSound.Play();

        textPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainpanelHantei = true;
        isText = false;
    }
  

}
