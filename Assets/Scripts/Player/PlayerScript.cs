using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Animator m_anime;
    [SerializeField] public Animator m_panelanime;
    [SerializeField] public float speed = 1.0f;
    private Rigidbody2D m_rb;
    private Vector2 inputAxis;
    public static int check = 10;
    [SerializeField] public GameObject eventSystem;
    public bool isMove = true;
    int currentHP;
    public bool isCombat = false;
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

        if (isMove == true)
        {
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
        if (isCombat == true)
        {
            currentHP = PlayerPrefs.GetInt("playerHP");
        }
        Debug.Log(currentHP);

    }

    public void StartLoad()
    {
        StartCoroutine("Wait_time");
    }
    IEnumerator Wait_time()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
    }



    private void FixedUpdate()
    {
        m_rb.velocity = inputAxis.normalized * speed;
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
        var RateEncount = Random.Range(0, 100);
        Debug.Log(RateEncount);
        Debug.Log(Speed);
        if (Speed > 0.5 && RateEncount == 50 )
        {
            //m_anime.Play("stop");
            isCombat = true;
            isMove = false;
            speed = 0;
            eventSystem.SetActive(false);
            m_panelanime.Play("tenmetu");
            Debug.Log("バトルへ");
            StartLoad();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "check")
        {
            check -= 1;
            

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
