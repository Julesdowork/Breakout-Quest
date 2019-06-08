using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Configuration parameters
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] bool autoplay;


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

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoplayEnabled()
    {
        return autoplay;
    }

    public void HalveSpeed()
    {
        gameSpeed = 0.5f;
    }

    public void NormalSpeed()
    {
        gameSpeed = 1f;
    }
}
