using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cumSnake : MonoBehaviour
{
    [SerializeField] private int direction;
    [SerializeField] private float speed;

    [SerializeField] private bool right;
    [SerializeField] private bool left;
    [SerializeField] private bool top;
    [SerializeField] private bool topCheck;


    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((left || right) && top)
        {
            rb.velocity = new Vector2(speed * direction, speed);
            if (!topCheck)
            {
                if (right)
                {
                    topCheck = true;
                    transform.Rotate(0, 0, 45, Space.World);
                }
                else if(left)
                {
                    topCheck = true;
                    transform.Rotate(0, 0, -45, Space.World);
                }

            }
        }
        else if(top)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (!topCheck)
            {
                topCheck = true;
                transform.Rotate(0, 0, 90, Space.World);
            }
            
        }
        else if ((left || right) && !top)
        {
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !Player.instance.die)
        {
            Player.instance.getDamage(transform.position.x);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "flip" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
