using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void MouseEnter()
    {
        this.transform.localScale = new Vector2(1.1f, 1.1f);
    }

    public void MouseExit()
    {
        this.transform.localScale = new Vector2(1f, 1f);
    }

    public void MouseDown()
    {
        this.transform.localScale = new Vector2(0.9f, 0.9f);
    }

    public void MouseUp()
    {
        this.transform.localScale = new Vector2(1f, 1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Play()
    {
        sound.instance.Jump();
        StartCoroutine(delayPlay());
    }

    IEnumerator delayPlay()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("condomman");
    }
}
