using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text CoinPoint;
    [SerializeField] public int coinp = 0;
    public static GameUI instant;
    // Start is called before the first frame update
    void Start()
    {
        instant = this;
    }

    // Update is called once per frame
    void Update()
    {
        CoinPoint.text = coinp + "";
    }
}
