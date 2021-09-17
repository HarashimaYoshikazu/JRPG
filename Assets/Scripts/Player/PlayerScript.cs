using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Animator m_anime;
    [SerializeField] Animator m_panelanime;
    [SerializeField] float Speed = 1.0f;
    private Rigidbody2D m_rb;
    private Vector2 inputAxis;
    public static int check = 10;
    bool input = false;
    Unit unit = new Unit();
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anime = GetComponent<Animator>();
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {

        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");


        if (inputAxis.x != 0)
        {
            inputAxis.y = 0;
        }
        //斜め移動しない


        if (inputAxis.x == 1)
        {
            m_anime.SetBool("right", true);

        }
        else if (inputAxis.x == -1)
        {
            m_anime.SetBool("left", true);

        }


        if (inputAxis.x == 0)
        {
            m_anime.SetBool("left", false);
            m_anime.SetBool("right", false);

        }



        if (inputAxis.y == 1)
        {
            m_anime.SetBool("up", true);

        }
        else if (inputAxis.y == -1)
        {
            m_anime.SetBool("down", true);

        }

        if (inputAxis.y == 0)
        {
            m_anime.SetBool("up", false);
            m_anime.SetBool("down", false);
        }

    }

    public void StartLoad()
    {
        StartCoroutine("Wait_time");
    }
    IEnumerator Wait_time()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("battle", LoadSceneMode.Additive);
    }



    private void FixedUpdate()
    {
        m_rb.velocity = inputAxis.normalized * Speed;
    }
    //動きをどの画面に合わせられる
    private void OnTriggerStay2D(Collider2D collision)
    {
        //private void OnTriggerStay2D コライダーの中にいるとずっと


        if (collision.gameObject.tag == "encount")
        {
            Encount();

        }



    }
    void Encount()
    {
        var Speed = m_rb.velocity.magnitude;
        var RateEncount = Random.Range(0, 300);
        Debug.Log(RateEncount);
        Debug.Log(Speed);
        if (Speed > 0.5 && RateEncount == 50)
        {
            m_anime.Play("stop");
            m_panelanime.Play("tenmetu");
            StartLoad();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "check")
        {
            check -= 1;
            Debug.Log(unit.hp);

        }
        else if (collision.gameObject.tag == "exit")
        {
            SceneManager.LoadScene("test3");

        }
        else if (collision.gameObject.tag == "test")
        {
            Debug.Log("gold=" + check);
        }
    }

}
