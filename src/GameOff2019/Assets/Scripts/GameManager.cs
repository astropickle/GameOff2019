using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float halfScreenWidth = 0f;
    public float halfScreenHeight = 0f;

    public bool isGameOver = false;
    public int currentLevel = 1;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        halfScreenHeight = Camera.main.orthographicSize;
        halfScreenWidth = Camera.main.aspect * halfScreenHeight;

        LoadCurrentLevelScene();
    }

    public void LoadCurrentLevelScene()
    {
        SceneManager.LoadSceneAsync("Level " + currentLevel, LoadSceneMode.Single);
    }

    public void LevelComplete()
    {
        currentLevel++;

        LoadCurrentLevelScene();
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
