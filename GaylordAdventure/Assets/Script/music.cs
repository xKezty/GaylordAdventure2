using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    [SerializeField] public AudioSource audios;

    public static music instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audios = GetComponent<AudioSource>();
    }

}
