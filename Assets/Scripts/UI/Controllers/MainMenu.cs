using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;

    private void Awake() {
        GameManager.ClearInstance();
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
    }

    public void ExitCredits()
    {
        credits.SetActive(false);
    }

    public void GotoItch(){
        Application.OpenURL("https://fmag.itch.io/earth-defense-department");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
