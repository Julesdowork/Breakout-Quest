using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 16f;
    float currentMinX;
    float currentMaxX;
    float minXWhenDoubled = 2f;
    float maxXWhenDoubled = 14f;

    // Cached references
    GameSession gameSession;
    Ball ball;
    Animator m_animator;
    SpriteRenderer m_spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        currentMinX = minX;
        currentMaxX = maxX;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetPosX(), currentMinX, currentMaxX);
        transform.position = paddlePos;
        
        Bump();
    }

    private float GetPosX()
    {
        if (gameSession.IsAutoplayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

    private void Bump()
    {
        if (ball.HasStarted() && Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Bump");
        }
    }

    public void WidenPaddle()
    {
        m_spriteRenderer.size = new Vector2(m_spriteRenderer.size.x * 2, m_spriteRenderer.size.y);
        currentMinX = minXWhenDoubled;
        currentMaxX = maxXWhenDoubled;
    }

    public void NormalPaddle()
    {
        m_spriteRenderer.size = new Vector2(m_spriteRenderer.size.x / 2, m_spriteRenderer.size.y);
        currentMinX = minX;
        currentMaxX = maxX;
    }
}
