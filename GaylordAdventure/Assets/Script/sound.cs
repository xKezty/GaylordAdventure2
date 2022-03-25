using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    [SerializeField] private AudioSource audios;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip coin;
    [SerializeField] private AudioClip enemyDie;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip cum;
    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip another;
    [SerializeField] private AudioClip spin;
    [SerializeField] private AudioClip splat;

    public static sound instance;
    private void Start()
    {
        instance = this;
        audios = GetComponent<AudioSource>();
    }

    void play(AudioClip clip)
    {
        audios.PlayOneShot(clip);
    }

    public void Jump()
    {
        play(jump);
    }

    public void Coin()
    {
        play(coin);
    }
    public void EnemyDie()
    {
        play(enemyDie);
    }
    public void Attack()
    {
        play(attack);
    }
    public void Cum()
    {
        play(cum);
    }
    public void Damage()
    {
        play(damage);
    }

    public void Win()
    {
        play(win);
    }

    public void Another()
    {
        play(another);
    }


    public void Spin()
    {
        play(spin);
    }

    public void Splat()
    {
        play(splat);
    }
}
