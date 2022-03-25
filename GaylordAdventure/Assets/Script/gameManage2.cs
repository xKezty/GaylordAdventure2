using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManage2 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossRelease;
    [SerializeField] private GameObject lockblock;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject resume;
    [SerializeField] private bool gay;

    [SerializeField] private bool WinSound;
    // Start is called before the first frame update
    void Start()
    {
        lockblock.SetActive(false);
        resume.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= bossRelease.transform.position.x)
        {
            CondomMan.instance.start = true;
            lockblock.SetActive(true);
        }

        if (player.transform.position.x >= win.transform.position.x)
        {
            Player.instance.control = false;
            if (!WinSound)
            {
                WinSound = true;
                sound.instance.Win();
            }

            StartCoroutine(delayGreenMamba());
        }

        if (Player.instance.die)
        {
            StartCoroutine(delayGameover());
        }

        if (Input.GetKeyUp(KeyCode.Escape) && !gay)
        {
            showGay();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
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

    IEnumerator delayGreenMamba()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("greenmamba");
    }

    IEnumerator delayGameover()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
