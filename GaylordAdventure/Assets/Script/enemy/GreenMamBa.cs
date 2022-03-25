using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMamBa : MonoBehaviour
{
    [SerializeField] public bool start;
    [SerializeField] public GameObject lockBlock;
    [SerializeField] public GameObject[] cumSnake;
    [SerializeField] public GameObject[] cumPoint;
    [SerializeField] public GameObject[] sperms;

    [SerializeField] private float speed;
    [SerializeField] private int direction;
    [SerializeField] private int hp;
    [SerializeField] private bool atkCheck;
    [SerializeField] private float cooldownATK;

    [SerializeField] private bool die;
    [SerializeField] private float durationDie;

    [SerializeField] private float noDamage = 0;
    [SerializeField] private bool Sound;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] public Animator anime;

    public static GreenMamBa instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            sperms = GameObject.FindGameObjectsWithTag("sperm");
            anime.SetBool("run", true);
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
            cumsnake();
            durationEnemy();
        }
    }

    void durationEnemy()
    {
        if (die)
        {
            foreach(GameObject sperm in sperms)
            {
                Destroy(sperm);
            }
            durationDie -= Time.deltaTime;
            if (durationDie <= 0)
            {
                Destroy(gameObject);
                //sound.instance.EnemyDie();
                lockBlock.SetActive(false);
            }
        }

        if (noDamage > 0)
        {
            noDamage -= Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "flip")
        {
            if(direction == -1)
            {
                direction = 1;
                sp.flipX = true;
            }
            else if(direction == 1)
            {
                direction = -1;
                sp.flipX = false;
            }
        }
    }

    void cumsnake()
    {
        if (atkCheck)
        {
            cooldownATK -= Time.deltaTime;
            if(cooldownATK <= 0)
            {
                atkCheck = false;
            }
        }

        if(!atkCheck && start)
        {
            if(direction == -1)
            {
                Instantiate(cumSnake[0], new Vector2(cumPoint[0].transform.position.x, cumPoint[0].transform.position.y),Quaternion.identity);
                
            }
            else
            {
                Instantiate(cumSnake[1], new Vector2(cumPoint[1].transform.position.x, cumPoint[1].transform.position.y), Quaternion.identity);

            }
            sound.instance.Splat();
            atkCheck = true;
            cooldownATK = 4f;
        }
    }

    public void takeDamage(int damage)
    {
        if (!die && noDamage <= 0 && start)
        {
            hp -= damage;
            StartCoroutine(damageEffect());
            noDamage = 0.5f;
        }

        if (hp <= 0)
        {
            if (!Sound)
            {
                Sound = true;
                sound.instance.Another();
            }
            anime.SetTrigger("die");
            die = true;
            speed = 0;
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
