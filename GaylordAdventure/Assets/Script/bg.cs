using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg : MonoBehaviour
{

    [SerializeField] private float min;
    [SerializeField] private float positions;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > min)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(positions,transform.position.y);
        }
    }
}
