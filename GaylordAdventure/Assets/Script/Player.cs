using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int step;

    public bool control;
    public float speed = 5f;
    public float jump = 5f;
    public int hp = 3;
    public int atk;
    private int check = 2;
    public int direction = 1;

    [SerializeField] private LayerMask target;
    [SerializeField] private Transform positionAttack;
    [SerializeField] private float rangeAttack;
    

    [SerializeField]private GameObject[] Health;

    [SerializeField] public bool isFlash = false;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator anime;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform KUY;

    [SerializeField] private bool isGround;
    [SerializeField] private bool isMove;
    [SerializeField] private bool Attack = false;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private GameObject Cumshooter_right;
    
    [SerializeField] private GameObject[] Cum;

    [SerializeField] public bool die;
    
    public static Player instance;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        control = false;
        step = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (control)
        {
            hpoint();
            if (!die)
            {
                Run();
                if (Input.GetKeyDown("j") && !Attack && !die)
                {
                    anime.SetTrigger("Attack");
                    Attack = true;
                    AttackEnemy();
                    StartCoroutine(Delay_Attack());

                }
            }

            FlipSprite();
            CheckGround();
            Jump();
            AnimePlayer();


            if (Input.GetKeyDown("k") && !Attack && !die)
            {
                anime.SetTrigger("Cumshoot");
                Attack = true;

                StartCoroutine(Delay_Cum());


            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                direction = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                direction = -1;
            }
        }
        else
        {
            if(step == 1)
            {
                if(transform.position.x < gameManager.instance.start.transform.position.x)
                {
                    rb.velocity = new Vector2(speed,rb.velocity.y);
                }
                else
                {
                    step = 0;
                }
            }
            else if(step == 2)
            {
                rb.velocity = new Vector2(rb.velocity.x,speed);
            }
            else if(step == 0)
            {
                control = true;
            }
        }
        
    }
    void Run()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        isMove = (Input.GetAxisRaw("Horizontal") != 0);
    }
    void FlipSprite()
    {
        bool flip = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (flip)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x) * 1f, 1f);
            
        }

    }
    void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(KUY.transform.position, 0.2f, groundLayer);
    }

    void Jump()
    {
        if (isGround)
        {
            check = 2;
        }
        if (Input.GetButtonDown("Jump") && check != 0 && !isFlash)
        {
            check -= 1;
            if (!isGround)
            {
                check -= 1;
            }
            rb.velocity = new Vector2(rb.velocity.x, jump);
            sound.instance.Jump();
        }
    }
    void AnimePlayer()
    {
        anime.SetBool("isMove", isMove);
        anime.SetBool("isGround", isGround);
    }

    void AttackEnemy()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(positionAttack.position, rangeAttack, target);

        foreach(Collider2D enemies in hitEnemy)
        {
            sound.instance.Attack();
            if(enemies.name.Substring(0,3) == "Con")
            {
                enemies.GetComponent<CondomMan>().takeDamage(atk);
            }
            else if (enemies.name.Substring(0, 3) == "Gre")
            {
                enemies.GetComponent<GreenMamBa>().takeDamage(atk);
            }
            else if (enemies.name.Substring(0, 3) == "Fin")
            {
                enemies.GetComponent<finalBoss>().takeDamage(atk);
            }
            else
            {
                enemies.GetComponent<Enemy>().takeDamage(atk);
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (positionAttack == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(positionAttack.position, rangeAttack);
    }

    IEnumerator Delay_Attack()
    {
        yield return new WaitForSeconds(0.5f);
            Attack = false;
    }
    IEnumerator Delay_Cum()
    {
        yield return new WaitForSeconds(0.5f);
        if (direction == 1)
        {
            sound.instance.Cum();
            Instantiate(Cum[0], new Vector2(Cumshooter_right.transform.position.x, Cumshooter_right.transform.position.y), Quaternion.identity);
        }
        else if (direction == -1)
        {
            sound.instance.Cum();
            Instantiate(Cum[1], new Vector2(Cumshooter_right.transform.position.x, Cumshooter_right.transform.position.y), Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        Attack = false;
    }


    public void hpoint()
    {
        if (hp == 3)
        {
            Health[0].gameObject.SetActive(true);
            Health[1].gameObject.SetActive(true);
            Health[2].gameObject.SetActive(true);
        }
        else if (hp == 2)
        {
            Health[0].gameObject.SetActive(true);
            Health[1].gameObject.SetActive(true);
            Health[2].gameObject.SetActive(false);
        }
        else if (hp == 1)
        {
            Health[0].gameObject.SetActive(true);
            Health[1].gameObject.SetActive(false);
            Health[2].gameObject.SetActive(false);
        }
        else
        {
            Health[0].gameObject.SetActive(false);
            Health[1].gameObject.SetActive(false);
            Health[2].gameObject.SetActive(false);
        }
    }

    public void addHp()
    {
        hp++;
        if (hp > 3)
            hp = 3;
    }

    public void getDamage(float position)
    {
        hp--;
        sound.instance.Damage();
        isFlash = true;
        if(transform.position.x > position)
        {
            rb.AddForce(new Vector2(4000, 20));
        }
        else
        {
            rb.AddForce(new Vector2(-4000, 20));
        }
        StartCoroutine(Flash());
        if (hp <= 0)
        {
            Die();
        }
    }

    public void DieOnly()
    {
        hp -= 3;
        sound.instance.Damage();
        Die();
    }

    void Die()
    {
        anime.SetTrigger("Die");
        die = true;
        GetComponent<Collider2D>().enabled = false;
        KUY.GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    IEnumerator Flash()
    {
        for (int n = 0; n < 6; n++)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
        if (!die)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Collider2D>().isTrigger = false;
            isFlash = false;
        }
    }



    /* void getDamage()
     {
         hp--;
         Audio_Manager.instance.playEnemyHit();
         isFlash = true;
         rb.bodyType = RigidbodyType2D.Static;
         GetComponent<Collider2D>().enabled = false;
         StartCoroutine(Flash());
         if (hp <= 0)
         {
             gameObject.SetActive(false);
         }
     }

     public void addHp()
     {
         hp++;
         if (hp > GamePlay.instance.maxHp)
             hp = GamePlay.instance.maxHp;
     }

     IEnumerator Flash()
     {
         for (int n = 0; n < 6; n++)
         {
             sr.color = new Color(1f, 1f, 1f, 0.5f);
             yield return new WaitForSeconds(0.1f);
             sr.color = new Color(1f, 1f, 1f, 1f);
             yield return new WaitForSeconds(0.1f);
         }
         rb.bodyType = RigidbodyType2D.Dynamic;
         GetComponent<Collider2D>().enabled = true;
         isFlash = false;
     }
     private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "Enemy" && !isFlash)
         {
             getDamage();
         }
     }
     private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.CompareTag("Heart"))
         {
             HeartCollect.instance.heart += 1;
         }
     }*/
}
