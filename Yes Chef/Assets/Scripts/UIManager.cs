using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    float timeLeft = 180;
    public TextMeshProUGUI TimerText;

    public GameObject StartMenu;
    public GameObject EndGameMenu;
    public Image PauseButton;
    public Sprite PlayIMG;
    public Sprite PauseIMG;
    public TextMeshProUGUI InGamemenuScoreText;

    public TextMeshProUGUI EndGameMenuScoreText;
    public TextMeshProUGUI EndGameMenuBestScoreText;

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

    void Update()
    {
        if (GameManager.GameIsStart && (!GameManager.GameIsFinished))
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft >= 0)
            {
                TimerText.text = System.Math.Round(timeLeft, 2).ToString();
            }
            else
            {
                GameManager.GameIsFinished = true;
                GameManager.GameIsStart = false;
                TimerText.text = "00:00";
                GameManager.Instance.EndGameSetter();
                ScoreSetter();
                EndGameMenuOn();

            }
        }
    }

    public void StartMenuOff()
    {
        StartMenu.SetActive(false);
    }
    public void EndGameMenuOn()
    {
        EndGameMenu.SetActive(true);
    }
    public void ScoreSetter()
    {
        InGamemenuScoreText.text = GameManager.Score.ToString();

        EndGameMenuScoreText.text = GameManager.Score.ToString();
        EndGameMenuBestScoreText.text = (PlayerPrefs.GetInt("bestScore")).ToString();
    }
    public void PauseButtonUISet()
    {
        if (GameManager.Instance.IsGamePause)
        {
            PauseButton.sprite = PlayIMG;
        }
        else
        {
            PauseButton.sprite = PauseIMG;
        }
    }
}
