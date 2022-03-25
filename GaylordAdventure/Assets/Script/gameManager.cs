using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossRelease;
    [SerializeField] private GameObject lockblock;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject resume;
    [SerializeField] public GameObject start;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private bool gay;
    [SerializeField] private GameObject gayrayTalk;

    [SerializeField] private GameObject died;

    [SerializeField] private bool Condomman;
    [SerializeField] private bool GreenMamba;
    [SerializeField] private bool finalboss;

    [SerializeField] private bool WinSound;

    [SerializeField] private Animator gayray;
    [SerializeField] private bool oneOnly;

    public static gameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        lockblock.SetActive(false);
        resume.SetActive(false);
        Time.timeScale = 1f;
        fadeOut.SetActive(false);
        if (finalboss)
        {
            gayrayTalk.SetActive(false);
        }
        died.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x >= bossRelease.transform.position.x)
        {
            if (Condomman)
            {
                CondomMan.instance.start = true;
            }
            else if (GreenMamba)
            {
                GreenMamBa.instance.start = true;
            }
            else if (finalboss)
            {
                finalBoss.instance.start = true;
            }
            lockblock.SetActive(true);
        }

        if(player.transform.position.x >= win.transform.position.x)
        {
            if (Condomman)
            {
                Player.instance.control = false;
                if (!WinSound)
                {
                    WinSound = true;
                    sound.instance.Win();
                }
                Player.instance.step = -1;
                fadeOut.SetActive(true);
                StartCoroutine(delayGreenMamba());
            }
            else if (GreenMamba)
            {
                Player.instance.control = false;
                if (!WinSound)
                {
                    WinSound = true;
                    sound.instance.Win();
                }
                Player.instance.step = -1;
                fadeOut.SetActive(true);
                StartCoroutine(delayFinalBoss());
            }
            else if (finalboss)
            {
                
                if (Input.GetKeyDown("e"))
                {
                    oneOnly = true;
                    StartCoroutine(delayEnd());
                }
                else if(!oneOnly)
                {
                    Player.instance.control = false;
                    Player.instance.anime.SetBool("isMove", false);
                    Player.instance.rb.velocity = new Vector2(0, Player.instance.rb.velocity.y);
                    Player.instance.step = -1;
                    gayrayTalk.SetActive(true);
                    gayray.SetBool("talk", true);
                }
                //StartCoroutine(delayFinalBoss());
            }
            
            
        }

        if (Player.instance.die)
        {
            StartCoroutine(delayRicardo());
        }

        if (Input.GetKeyUp(KeyCode.Escape) && !gay && !Player.instance.die)
        {
            showGay();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && !Player.instance.die)
        {
            hideGay();
        }
    }

    void showGay()
    {
        resume.SetActive(true);
        gay = true;
        sound.instance.Damage();
        Time.timeScale = 0f;
    }

    public void hideGay()
    {
        resume.SetActive(false);
        gay = false;
        Time.timeScale = 1f;
    }

    public void RicardoHelp()
    {
        sound.instance.Jump();
        StartCoroutine(delayGameover());
    }

    public void DenyRicardo()
    {
        StartCoroutine(delayTitle());
    }

    IEnumerator delayGreenMamba()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("greenmamba");
    }

    IEnumerator delayFinalBoss()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("finalboss");
    }

    IEnumerator delayGameover()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator delayTitle()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("title");
    }

    IEnumerator delayEnd()
    {
        gayrayTalk.SetActive(false);
        gayray.SetBool("talk", false);
        yield return new WaitForSeconds(1f);
        Player.instance.anime.SetBool("isGround",false);
        Player.instance.step = 2;
        yield return new WaitForSeconds(2f);
        if (!WinSound)
        {
            WinSound = true;
            sound.instance.Win();
        }
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("title");
    }
   
    IEnumerator delayRicardo()
    {
        yield return new WaitForSeconds(2f);
        music.instance.audios.Stop();
        died.SetActive(true);
    }
}
