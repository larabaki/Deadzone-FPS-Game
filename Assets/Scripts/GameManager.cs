using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreToWin;
    public int currentScore;

    public bool gamePaused;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePausedGame();

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!gamePaused)
            {
                Time.timeScale = 0;
                gamePaused = true;
            }
            else
            {
                Time.timeScale = 1;
                gamePaused = false;
            }
        }
    }
    public void TogglePausedGame()
    {
        gamePaused = !gamePaused;

        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;


        Cursor.lockState = gamePaused == true ? CursorLockMode.None : CursorLockMode.Locked;
        //toggle the pause menu

        UIManager.instance.TogglePauseMenu(gamePaused);
    }

    public void AddScore(int score)
    {
        currentScore += score;

        UIManager.instance.UpdateScoreText(currentScore);

        if (currentScore >= scoreToWin)
            WinGame();
                }

    void WinGame()
    {
        //set the end game screen
        UIManager.instance.SetEndGameScreen(true, currentScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseGame()
    {
        UIManager.instance.SetEndGameScreen(false, currentScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
