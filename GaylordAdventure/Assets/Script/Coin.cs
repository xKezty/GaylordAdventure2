using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
   
    [SerializeField] private bool Coins;
    [SerializeField] private bool HPBox;
    [SerializeField] GameObject pop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Coins)
            {
                sound.instance.Coin();
                GameUI.instant.coinp += 1;
                Destroy(gameObject);
                Instantiate(pop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }
            else if (HPBox)
            {
                Player.instance.addHp();
                sound.instance.Coin();
                Destroy(gameObject);
                Instantiate(pop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }
           
        }
    }
}
