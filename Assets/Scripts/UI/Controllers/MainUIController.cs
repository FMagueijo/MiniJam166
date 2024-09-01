using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    public TMPro.TMP_Text timerShift;

    private void Update() {
        timerShift.text = ((int)GameManager.instance.timeLeft).ToString();
    }

    public void NextDay(){
        GameManager.instance.NextDay();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Restart()
    {
        GameManager.instance.Die();
        SceneManager.LoadSceneAsync(1);
    }
}
