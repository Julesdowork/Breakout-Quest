using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Configuration parameters
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlock = 10;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplay;

    // State
    [SerializeField] int currentScore = 0;

    void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayScore();
    }

    // Update is called once per frame
    void Update()
    {
        // Time.timeScale = gameSpeed;
    }

    private void DisplayScore()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlock;
        DisplayScore();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoplayEnabled()
    {
        return autoplay;
    }
}
