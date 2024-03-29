﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float fallSpeed = 1f;
    enum PowerUpType {
        SLOW, WIDE, STICKY, MULTI, LASER, LIFE, BONUS, BOMB, GHOST
    };
    const int TYPE_COUNT = 9;
    [SerializeField] Sprite[] typeSprites;
    const float effectTime = 10f;

    // State
    [SerializeField] PowerUpType m_type;    // TODO deserialize later

    // Cached references
    GameSession gameSession;
    Paddle paddle;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        paddle = FindObjectOfType<Paddle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // int index = Random.Range(0, TYPE_COUNT);
        // m_type = (PowerUpType) index;
        // GetComponent<SpriteRenderer>().sprite = typeSprites[index];
        m_type = PowerUpType.WIDE;
        GetComponent<SpriteRenderer>().sprite = typeSprites[1];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle"))
        {
            UsePower();
            Destroy(gameObject, 11f);
        }
    }

    private void UsePower()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        switch (m_type)
        {
            case PowerUpType.SLOW: StartCoroutine(SlowBall());
                                    break;
            case PowerUpType.WIDE: StartCoroutine(WidePaddle());
                                    break;
            case PowerUpType.STICKY: Debug.Log("Received Sticky Paddle");
            break;
            case PowerUpType.MULTI: Debug.Log("Received A New Ball");
            break;
            case PowerUpType.LASER: Debug.Log("Received Laser Ball");
            break;
            case PowerUpType.LIFE: Debug.Log("Received Extra Life");
            break;
            case PowerUpType.BONUS: Debug.Log("Received 10x Bonus, Smaller Paddle");
            break;
            case PowerUpType.GHOST: Debug.Log("Received Ghost Ball");
            break;
        }
    }

    IEnumerator SlowBall()
    {
        Debug.Log("Received Slower Ball");
        gameSession.HalveSpeed();
        yield return new WaitForSecondsRealtime(effectTime);
        gameSession.NormalSpeed();
    }

    IEnumerator WidePaddle()
    {
        Debug.Log("Received Wider Paddle");
        paddle.WidenPaddle();
        yield return new WaitForSecondsRealtime(effectTime);
        paddle.NormalPaddle();
    }
}
