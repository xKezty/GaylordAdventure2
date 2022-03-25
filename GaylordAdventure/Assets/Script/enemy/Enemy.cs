using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;

    [SerializeField] private float noDamage = 0;

    [SerializeField] private Animator aniEnemy;
    [SerializeField] private float durationDie;
    [SerializeField] private bool die;

    [SerializeField] private float speed;
    [SerializeField] private int direction = -1;

    [SerializeField] private bool sperm;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sp;
    void Start()
    {
        aniEnemy = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        durationEnemy();
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    void durationEnemy()
    {
        if (die)
        {
            durationDie -= Time.deltaTime;
            if(durationDie <= 0)
            {
                Destroy(gameObject);
            }
        }

        if(noDamage > 0)
        {
            noDamage -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !Player.instance.die && !die)
        {
            Player.instance.getDamage(transform.position.x);
        }
        if (!sperm)
        {
            if (collision.gameObject.tag == "flip" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "enemy")
            {
                flipSprite();
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sperm)
        {
            if (collision.gameObject.tag == "flip")
            {
                flipSprite();
            }
        }
    }

    public void takeDamage(int damage)
    {
        if (!die && noDamage <= 0)
        {
            hp -= damage;
            StartCoroutine(damageEffect());
            noDamage = 0.5f;
        }
        
        if(hp <= 0)
        {
            sound.instance.EnemyDie();
            aniEnemy.SetBool("die", true);
            die = true;
            GetComponent<Collider2D>().enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            speed = 0;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !sperm)
        {
            flipSprite();
        }
    }


    void flipSprite()
    {
        if (direction == -1)
        {
            direction = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction == 1)
        {
            direction = -1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    IEnumerator damageEffect()
    {
        yield return new WaitForSeconds(0.1f);
        sp.color = new Color(1f, 0.25f, 0.25f, 1f);
        yield return new WaitForSeconds(0.1f);
        sp.color = new Color(1f, 1f, 1f, 1f);
    }
}
