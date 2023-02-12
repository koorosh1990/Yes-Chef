using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameObject Meat;
    public GameObject Lettuce;
    public GameObject Cheese;
    public GameObject Burger;
    public GameObject ChopedLettuce;

    public static bool GameIsStart;
    public static bool GameIsFinished;

    public GameObject Player;

    public static int Score;

    public bool IsGamePause;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    void Start()
    {
        IsGamePause = false;
        Score = 0;
        if (!PlayerPrefs.HasKey("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", 0);
        }

    }

    public void StartSetter()
    {
        GameIsStart = true;
        GameIsFinished = false;
        UIManager.Instance.StartMenuOff();
    }
    public void EndGameSetter()
    {
        if (Score > PlayerPrefs.GetInt("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", Score);
        }
        CancelInvoke();

    }
    public void PauseSwitch()
    {
        IsGamePause = (!IsGamePause);
        if (IsGamePause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        UIManager.Instance.PauseButtonUISet();
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
