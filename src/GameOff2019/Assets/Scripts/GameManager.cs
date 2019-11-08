using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float halfScreenWidth = 0f;
    public float halfScreenHeight = 0f;

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

        halfScreenHeight = Camera.main.orthographicSize;
        halfScreenWidth = Camera.main.aspect * halfScreenHeight;
    }
}
