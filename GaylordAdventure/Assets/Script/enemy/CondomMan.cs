using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondomMan : MonoBehaviour
{
    [SerializeField] public bool start;
    [SerializeField] public GameObject lockBlock;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private int direction = -1;
    [SerializeField] private int hp;
    [SerializeField] private bool atkCheck;
    [SerializeField] private float cooldownATK;
    [SerializeField] private float distancetoStop;
    [SerializeField] private float maxFollowDistance = 10f;
    [SerializeField] private bool die;
    [SerializeField] private float durationDie;

    [SerializeField] private float noDamage = 0;
    [SerializeField] private float durationAtk;
    [SerializeField] private bool durationAtkCheck;
    
    [SerializeField] private GameObject player;
    [SerializeField] private bool inRange;
    [SerializeField] private bool Sound;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Animator anime;

    public static CondomMan instance;
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
            rb.velocity = new Vector2(speed * direction,rb.velocity.y);
            Movement();
            durationEnemy();
            if (player.transform.position.x > transform.position.x - maxFollowDistance && player.transform.position.x < transform.position.x + maxFollowDistance)
            {
                inRange = true;

            }
            else
            {
                inRange = false;
                speed = 0f;
            }

            if (atkCheck)
            {
                cooldownATK -= Time.deltaTime;
                if (cooldownATK <= 0)
                {
                    atkCheck = false;
                }
            }

            if (player.transform.position.x >= transform.position.x - distancetoStop && player.transform.position.x <= transform.position.x + distancetoStop && !Player.instance.die)
            {
                durationAtkCheck = true;
            }
            else
            {
                durationAtkCheck = false;
            }
            if (durationAtkCheck && !die)
            {
                durationAtk -= Time.deltaTime;
                if (durationAtk <= 0 && player.transform.position.y <= transform.position.y + 1 && player.transform.position.y >= transform.position.y - 1 && !Player.instance.die)
                {
                    Player.instance.getDamage(transform.position.x);
                    durationAtk = 5f;
                }
            }
            else
            {
                durationAtk = 0.5f;
            }

        }
        
    }

    void durationEnemy()
    {
        if (die)
        {
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

    void Movement()
    {
        if (inRange && !die)
        {
            if (player.transform.position.x < transform.position.x - distancetoStop)
            {
                anime.SetBool("run", true);
                speed = maxSpeed;
                if (direction == 1)
                {

                    direction = -1;
                    sp.flipX = false;
                }
            }
            else if (player.transform.position.x > transform.position.x + distancetoStop)
            {
                anime.SetBool("run", true);
                speed = maxSpeed;
                if (direction == -1)
                {

                    direction = 1;
                    sp.flipX = true;
                }
            }
            else
            {
                if (!atkCheck && cooldownATK <= 0 && player.transform.position.y <= transform.position.y + 1 && player.transform.position.y >= transform.position.y - 1 && !Player.instance.isFlash)
                {
                    anime.SetTrigger("attack");
                    atkCheck = true;
                    cooldownATK = 1f;
                }
                speed = 0;
            }
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
