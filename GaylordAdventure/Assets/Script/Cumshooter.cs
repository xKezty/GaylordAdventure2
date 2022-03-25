using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumshooter : MonoBehaviour
{
    [SerializeField] private float speed = 15f, lifeTime = 10f;
    [SerializeField] private int direction;
    [SerializeField] private LayerMask target;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        Damage();
    }

    void Damage()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(transform.position, 0.2f, target);
        foreach(Collider2D enemy in hitEnemy)
        {
            if (enemy.name.Substring(0, 3) == "Con")
            {
                enemy.GetComponent<CondomMan>().takeDamage(2);
            }
            else if (enemy.name.Substring(0, 3) == "Gre")
            {
                enemy.GetComponent<GreenMamBa>().takeDamage(2);
            }
            else if (enemy.name.Substring(0, 3) == "Fin")
            {
                enemy.GetComponent<finalBoss>().takeDamage(2);
            }
            else
            {
                enemy.GetComponent<Enemy>().takeDamage(2);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "flip" || collision.gameObject.tag == "Ground")
        {
            Destroy (gameObject);
        }
    }

}
