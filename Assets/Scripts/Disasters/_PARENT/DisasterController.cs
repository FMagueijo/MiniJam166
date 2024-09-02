using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisasterController : MonoBehaviour
{
    private Disaster disaster;

    [Header("(Set Active) Requirements")]
    [SerializeField] private FEarthDisasters reqDisaster;

    [Header("Tutorial")]
    [SerializeField] private bool bTutorial = true;
    [SerializeField] private FTutorials tutorial;
    [SerializeField] private TutorialController ttcc;
    [SerializeField] private SOTutorial tutorialLoad;


    [Header("Events")]
    [SerializeField] private UnityEvent OnEnableEvent;
    [SerializeField] private UnityEvent OnDisableEvent;

    private void Awake() {
        disaster = GetComponent<Disaster>();
        
        gameObject.SetActive(CanEnable());

        if (CanEnable()) CallTutorial();
    }

    public bool CanEnable(){
        if (!CheckDisaster()) return false;

        return true;
    }

    private bool CheckDisaster(){
        return (GameManager.instance.UnlockedDisasters & reqDisaster) == reqDisaster;
    }

    private bool CheckTutorial()
    {
        return (GameManager.instance.UnlockedTutorials & tutorial) == tutorial;
    }

    private void OnEnable() {
        OnEnableEvent.Invoke();
    }

    private void OnDisable() {
        OnDisableEvent.Invoke();
    }

    public void CallTutorial(){
        if (!bTutorial) return;
        
        if (ttcc == null) return;

        if (!CheckTutorial())
        {
            ttcc.AddToQueue(tutorialLoad);
            GameManager.instance.AddTutorial(tutorial);
            ttcc.gameObject.SetActive(true);
        }
    }

}
