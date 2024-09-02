using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Queue<SOTutorial> sOTutorials= new Queue<SOTutorial>();
    public SOTutorial currentTutorial;

    public TMPro.TMP_Text title, body;
    public Image img;

    private void Awake() {
        gameObject.SetActive(false);
    }


    private void Start()
    {
        ShowTutorial();
    }

    public void ShowTutorial(){
        Debug.Log("Para tudo");
        Time.timeScale = 0;
        gameObject.SetActive(true);

        currentTutorial = sOTutorials.Dequeue();

        if (currentTutorial == null) return;

        title.text = currentTutorial.title;
        body.text = currentTutorial.body;
        img.sprite = currentTutorial.image;
        img.preserveAspect = true;

    }

    public void AddToQueue(SOTutorial tutorial){
        sOTutorials.Enqueue(tutorial);
        Debug.Log(sOTutorials.Count);
        gameObject.SetActive(true);
    }

    public void Understood() {
        if(sOTutorials.Count <= 0){
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
        else{
            ShowTutorial();
        }
    }
}
