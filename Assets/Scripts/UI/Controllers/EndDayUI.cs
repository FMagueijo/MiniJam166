using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDayUI : MonoBehaviour
{
    public GameObject dead;
    public GameObject alive;

    private void Start() {
        GameManager gm = GameManager.instance;
        if (gm == null) return;

        gm.OnDayEnd.AddListener(EndDayProcess);
    }

    private void EndDayProcess(){
        GameManager gm = GameManager.instance;


        if (gm.IsAlive)
        {
            alive.SetActive(true);
        }
        else{
            dead.SetActive(true);
        }

        Time.timeScale = 0;

    }
}
