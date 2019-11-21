using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Text runTimerText;

    public float halfScreenWidth = 0f;
    public float halfScreenHeight = 0f;

    public bool isGameOver = false;
    public int currentLevel = 1;
    public bool hasStartedCurrentLevel = false;
    public float runTimer = 0f;

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

    void Update()
    {
        // Update run timer
        UpdateRunTimer();
    }

    public void LoadCurrentLevelScene()
    {
        SceneManager.LoadSceneAsync("Level " + currentLevel, LoadSceneMode.Single);
    }

    public void LevelComplete()
    {
        currentLevel++;

        hasStartedCurrentLevel = false;

        LoadCurrentLevelScene();
    }

    private void UpdateRunTimer()
    {
        if (hasStartedCurrentLevel)
        {
            runTimer += Time.deltaTime;

            runTimerText.text = runTimer.ToString("F2");
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
