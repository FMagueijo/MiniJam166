using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    public TMPro.TMP_Text timerShift;
    
    public GameObject pauseMenu;

    private void Update() {
        timerShift.text = (Mathf.Ceil(GameManager.instance.timeLeft)).ToString();
    }

    public void NextDay(){
        GameManager.instance.NextDay();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void Restart()
    {
        GameManager.instance.Die();
        SceneManager.LoadSceneAsync(1);
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void PauseGame(){
        if (!GameManager.instance.IsGameActive) return;

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

    }
}
