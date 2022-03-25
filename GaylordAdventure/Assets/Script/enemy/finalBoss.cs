using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBoss : MonoBehaviour
{
    [SerializeField] public bool start;
    [SerializeField] public GameObject lockBlock;
    [SerializeField] public GameObject[] sperms;

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

    [SerializeField] private bool spin;
    [SerializeField] private float cooldownSpin;
    [SerializeField] private bool oneTime;
    [SerializeField] private float durationSpin;
    [SerializeField] private float cooldownSperm;
    [SerializeField] private GameObject spermPointT;
    [SerializeField] private GameObject spermPointR;
    [SerializeField] private GameObject spermPointL;
    [SerializeField] private GameObject spermPointTR;
    [SerializeField] private GameObject spermPointTL;
    [SerializeField] private GameObject spermT;
    [SerializeField] private GameObject spermR;
    [SerializeField] private GameObject spermL;
    [SerializeField] private GameObject spermTR;
    [SerializeField] private GameObject spermTL;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Animator anime;

    public static finalBoss instance;
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
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
            Movement();
            durationEnemy();
            Spin();
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
            if (durationAtkCheck && !die && !spin)
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
            foreach (GameObject sperm in sperms)
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

        if(cooldownSpin > 0)
        {
            cooldownSpin -= Time.deltaTime;
            if(cooldownSpin <= 0)
            {
                spin = true;
            }
        }

    }

    void Spin()
    {
        if (spin)
        {
            if (!oneTime)
            {
                sound.instance.Spin();
                anime.SetTrigger("pre");
                StartCoroutine(delaySpin());
                speed = 0;
                oneTime = true;
            }
            if(cooldownSperm > 0)
            {
                cooldownSperm -= Time.deltaTime;
                if(cooldownSperm <= 0)
                {
                    sound.instance.Splat();
                    Instantiate(spermT,new Vector2(spermPointT.transform.position.x,spermPointT.transform.position.y),Quaternion.identity);
                    Instantiate(spermR, new Vector2(spermPointR.transform.position.x, spermPointR.transform.position.y), Quaternion.identity);
                    Instantiate(spermL, new Vector2(spermPointL.transform.position.x, spermPointL.transform.position.y), Quaternion.identity);
                    Instantiate(spermTL, new Vector2(spermPointTL.transform.position.x, spermPointTL.transform.position.y), Quaternion.identity);
                    Instantiate(spermTR, new Vector2(spermPointTR.transform.position.x, spermPointTR.transform.position.y), Quaternion.identity);
                    cooldownSperm = 1.2f;
                }
            }

            if(durationSpin > 0)
            {
                durationSpin -= Time.deltaTime;
                if(durationSpin <= 0)
                {
                    spin = false;
                    oneTime = false;
                    durationSpin = 4f;
                    cooldownSpin = 10f;
                }
            }
            
        }
    }

    void Movement()
    {
        if (inRange && !die && !spin)
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

    IEnumerator delaySpin()
    {
        yield return new WaitForSeconds(0.25f);
        anime.SetTrigger("sperm");
        
    }
}
