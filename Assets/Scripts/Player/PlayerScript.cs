﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public bool isCombat ;
    [SerializeField] string m_messageTextName = "MessageText";
    [SerializeField] GameObject panel;
    int hp ;
    [SerializeField] string sceneName = "Combat4";
    GameObject house;
    [SerializeField] AudioSource encountsound;
    [SerializeField] AudioSource bgm;
    [SerializeField] GameObject minimap;

    bool isPanel;
    bool isFirstCombat = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anime = GetComponent<Animator>();
        Debug.Log("start");
         house = GameObject.FindGameObjectWithTag("House");
        PlayerPrefs.SetInt("playerHP", 30);
        PlayerPrefs.SetInt("playerMP", 10);
    }
    void ShowMessage()
    {
        GameObject go = GameObject.Find(m_messageTextName);
        GameObject gomp = GameObject.Find("MpText");
        Text text = go?.GetComponent<Text>();
        Text mptext = gomp?.GetComponent<Text>();

        if (isFirstCombat ==false)
        {
            text.text = $"HP : {PlayerPrefs.GetInt("playerHP")}";
            mptext.text = $"MP : {PlayerPrefs.GetInt("playerMP")}";
            Debug.Log(text.text);
        }
        
        if (isFirstCombat == true)
        {
            text.text = $"HP :{PlayerPrefs.GetInt("playerHP")}";
            mptext.text = $"MP : {PlayerPrefs.GetInt("playerMP")}";
            Debug.Log(text.text);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (isPanel == false && Input.GetKeyDown(KeyCode.Tab))
        {
            panel.SetActive(true);
            ShowMessage();
            isPanel = true;
        }

        if (isPanel == true && Input.GetKeyUp(KeyCode.Tab))
        {
            panel.SetActive(false);
            
            isPanel = false;
        }

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
        //if (isCombat == false)
        //{
        //    minimap.SetActive(true);
        //}
        //else if(isCombat == true)
        //{
        //    minimap.SetActive(false);
        //}
        //Debug.Log(currentHP);

    }

    public void StartLoad()
    {
        StartCoroutine("Wait_time");
    }
    public void StartLoad2()
    {
        StartCoroutine("Wait_time2");
    }
    IEnumerator Wait_time()
    {
        bgm.Stop();
        int randum = Random.Range(0, 3);
        
        encountsound.Play();
        yield return new WaitForSeconds(2f);
        if (randum ==0)
        {
            SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
        }
        if (randum == 1)
        {
            SceneManager.LoadScene("Combat2", LoadSceneMode.Additive);
        }
        if (randum == 2)
        {
            SceneManager.LoadScene("Combat3", LoadSceneMode.Additive);
        }
    }
    IEnumerator Wait_time2()
    {
        bgm.Stop();
        int randum = Random.Range(0, 3);
        
        encountsound.Play();
        yield return new WaitForSeconds(2f);
        if (randum == 0)
        {
            SceneManager.LoadScene("Combat5", LoadSceneMode.Additive);
        }
        if (randum == 1)
        {
            SceneManager.LoadScene("Combat6", LoadSceneMode.Additive);
        }
        if (randum == 2)
        {
            SceneManager.LoadScene("Combat7", LoadSceneMode.Additive);
        }
    }
    IEnumerator UpStair()
    {
        isMove = false;
        speed = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Heya");
    }

    IEnumerator DownStair()
    {
        isMove = false;
        speed = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("FirstMura");
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
        else if (collision.gameObject.tag == "reEncount")
        {
            Encount2();
        }



    }
    void Encount()
    {
        var Speed = m_rb.velocity.magnitude;
        var RateEncount = Random.Range(0, 450);
        Debug.Log(RateEncount);
        Debug.Log(Speed);
        if (Speed > 0.5 && RateEncount == 50 )
        {
            //m_anime.Play("stop");
            isCombat = false;
            isFirstCombat = false;
            isMove = false;
            speed = 0;
            
            eventSystem.SetActive(false);
            m_panelanime.Play("tenmetu");
            Debug.Log("バトルへ");
            StartLoad();
        }
    }
    void Encount2()
    {
        var Speed = m_rb.velocity.magnitude;
        var RateEncount = Random.Range(0, 450);
        Debug.Log(RateEncount);
        Debug.Log(Speed);
        if (Speed > 0.5 && RateEncount == 50)
        {
            //m_anime.Play("stop");
            isCombat = false;
            isFirstCombat = false;
            isMove = false;
            speed = 0;

            eventSystem.SetActive(false);
            m_panelanime.Play("tenmetu");
            Debug.Log("バトルへ2");
            StartLoad2();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "upStair")
        {
            StartCoroutine("UpStair");
           
        }
        else if (collision.gameObject.tag == "downStair")
        {
            StartCoroutine("DownStair");
        }
        else if (collision.gameObject.tag == "Enter")
        {
            house.SetActive(false);
        }
        else if (collision.gameObject.tag == "Exit")
        {
            house.SetActive(true);
        }
        else if (collision.gameObject.tag == "Tomap")
        {   
            SceneManager.LoadScene("map");
        }
        else if (collision.gameObject.tag == "mura")
        {
            SceneManager.LoadScene("FirstMura");
        }
        else if (collision.gameObject.tag == "cave")
        {
            SceneManager.LoadScene("cave");
        }
        else if (collision.gameObject.tag == "bat")
        {
            SceneManager.LoadScene("Combat",LoadSceneMode.Additive);
        }
        else if (collision.gameObject.tag == "slime")
        {
            SceneManager.LoadScene("Combat3", LoadSceneMode.Additive);
        }
        else if (collision.gameObject.tag == "skelton")
        {
            SceneManager.LoadScene("Combat2", LoadSceneMode.Additive);
        }
        else if (collision.gameObject.tag == "yellowbat")
        {
            SceneManager.LoadScene("Combat6", LoadSceneMode.Additive);
        }
        else if (collision.gameObject.tag == "redskelton")
        {
            SceneManager.LoadScene("Combat5", LoadSceneMode.Additive);
        }
        else if (collision.gameObject.tag == "greenslime")
        {
            SceneManager.LoadScene("Combat7", LoadSceneMode.Additive);
        }
        else if (collision.gameObject.tag == "maou")
        {
            SceneManager.LoadScene("Combat4", LoadSceneMode.Additive);
        }
    }

}
