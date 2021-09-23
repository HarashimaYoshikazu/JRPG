using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public UItext uitext;
    [SerializeField] AudioSource click;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FinalText");
    }

    // Update is called once per frame

    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        while (!uitext.IsSpace()) yield return 0;
    }
    IEnumerator FinalText()
    {
        uitext.DrawText($"このわたしが負けるとは・・・");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"しかし、まだ野望はきえていない");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"またよみがえるのだ");
        yield return StartCoroutine("Skip");
        click.Play();
        uitext.DrawText($"つぎこそは・・・");
        yield return StartCoroutine("Skip");
        click.Play();
        SceneManager.LoadScene("Title");
        
    }
}
