using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] AudioClip[] ballSounds;

    Vector2 paddleToBallVector;
    bool hasStarted = false;

    Rigidbody2D m_rigidbody;
    AudioSource m_audioSource;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockToPaddle();
            LaunchOnMouseClick();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0, randomFactor),
            Random.Range(0, randomFactor));
        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            m_audioSource.PlayOneShot(clip);
            m_rigidbody.velocity += velocityTweak;
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x,
            paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            m_rigidbody.velocity = new Vector2(xPush, yPush);
        }
    }
}
