using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform bg0;
    [SerializeField] private float factor0 = 0.8f;

    [SerializeField] private Transform player;

    [SerializeField] private float min;
    [SerializeField] private float max;

    private float defaultCamera;
    private float nextCamera;

    // Update is called once per frame
    void Update()
    {
        defaultCamera = transform.position.x;
        
        if(player.transform.position.x > min && player.transform.position.x < max)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void LateUpdate()
    {
        nextCamera = transform.position.x;
        bg0.position = new Vector3(bg0.position.x + (nextCamera - defaultCamera) *
        factor0, bg0.position.y, bg0.position.z);
    }
}
