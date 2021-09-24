using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoss : MonoBehaviour
{
    public UItext uitext;
    [SerializeField] PlayerScript ps;
    [SerializeField] Animator psAnime;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject ev;
    [SerializeField] AudioSource click;
 
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsSpace()) yield return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            PlayerPrefs.SetInt("playerHP", 70);
            PlayerPrefs.SetInt("playerMP", 30);
            panel.SetActive(true);
            ps.speed = 0;
            psAnime.Play("stopplayer");
            StartCoroutine("BossText");
        }
    }
    IEnumerator BossText()
    {
        uitext.DrawText($"よくぞここまできた。");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"わたしのもくてきは破壊と混乱なのだ。");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"じゃまをするものはすべてたおしてきた。");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"おまえにもじごくをみせてやろう。");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"ゆうしゃはけついがみなぎった");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"HPとMPが全回復した！");
        yield return StartCoroutine("Skip");
        click.Play();
        ev.SetActive(false);
        SceneManager.LoadScene("Combat4",LoadSceneMode.Additive);
    }
}
