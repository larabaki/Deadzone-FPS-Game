using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public Image healthBarFill;

    [Header("Paused Menu")]
    public GameObject pauseMenu;

    [Header("End Game Screen")]
    public GameObject endGameScreen;
    public TextMeshProUGUI endGameHeaderText;
    public TextMeshProUGUI endGameScoreText;

    public static UIManager instance;
    //allows us to get access this script from anywhere else in the project instantly 

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealthBar(int currentHP,int maxHP)
    {
        healthBarFill.fillAmount = (float)currentHP / (float)maxHP;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score:" + score;
    }

    public void UpdateAmmoText(int currentAmmo,int maxAmmo)
    {
        ammoText.text = "Ammo:" + currentAmmo + " / " + maxAmmo;  // 30/100 gibi
    }

    public void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

    public void SetEndGameScreen(bool won,int score)
    {
        endGameScreen.SetActive(true);
        endGameHeaderText.text = won == true ? "You Won :) " : "You Lose :(";
        endGameHeaderText.color = won == true ? Color.green : Color.red;
        endGameScoreText.text = "<b> Score:  </b>\n " +  score;
    }

    public void OnResumeButton()
    {
        GameManager.instance.TogglePausedGame();
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("Hospital");
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
