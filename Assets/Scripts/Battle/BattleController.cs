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
    public bool left = true;
    public bool up = true;
    public bool spellleft = true;
    public bool spellup = true;


    bool mainpanelHantei = true;

    //SelectableText st = new SelectableText();


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
                    phase = Phase.CommandPhase;
                    break;
                case Phase.CommandPhase:
                    //技を選んだら次のフェーズへ
                    // new WaitUntil(() => ここがtrueになるまで待機

                    //yield return new WaitUntil(() =>Input.GetKeyDown(KeyCode.Space));

                    if (up == true && left == true && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[0];
                        enemy.serectCommand = enemy.commands[0];
                        phase = Phase.ExcutePhase;
                        enemy.target = player;
                        player.target = enemy;
                        Debug.Log("<color=yellow>こうげき！</color>");
                    }
                    else if (up == true && left == false && Input.GetKeyDown(KeyCode.Space))
                    {

                        phase = Phase.SpellCommandPhase;
                        Debug.Log("<color=red>呪文へ！</color>");
                    }
                    else if (up == false && left == true && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[2];
                        enemy.serectCommand = enemy.commands[0];

                        phase = Phase.ExcutePhase;
                        Debug.Log("<color=yellow>にげる！</color>");
                    }


                    break;
                case Phase.SpellCommandPhase:
                    mainpanelHantei = false;
                    mainPanel.SetActive(false);
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
                        spellPanel.SetActive(false);
                        Debug.Log("<color=green>ホロロン！</color>");
                    }
                    else if (spellup == true && spellleft == false && Input.GetKeyDown(KeyCode.Space))
                    {
                        player.serectCommand = player.commands[4];
                        enemy.serectCommand = enemy.commands[0];
                        enemy.target = player;
                        player.target = enemy;
                        phase = Phase.ExcutePhase;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellPanel.SetActive(false);
                        Debug.Log("<color=red>ゴヒロミ</color>");
                    }
                    else if (spellup == false && spellleft == true && Input.GetKeyDown(KeyCode.Space))
                    {


                        phase = Phase.ExcutePhase;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellPanel.SetActive(false);
                        Debug.Log("<color=blue>カキゴリ</color>");
                    }
                    else if (spellup == false && spellleft == false && Input.GetKeyDown(KeyCode.Space))
                    {
                        //コマンドフェーズへ戻る
                        phase = Phase.CommandPhase;
                        mainpanelHantei = true;
                        mainPanel.SetActive(true);
                        spellPanel.SetActive(false);

                    }


                    break;
                case Phase.ExcutePhase:
                    if (player.serectCommand == player.commands[0])
                    {
                        //こうげき
                        player.serectCommand.Execute(player, player.target);
                        enemy.serectCommand.Execute(enemy, enemy.target);
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
                        player.serectCommand.Execute(player, player.target);
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
                    break;
                case Phase.End:
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
        else
        {
            SpellCursorMove();
        }

    }







}
