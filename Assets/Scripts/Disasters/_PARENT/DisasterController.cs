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
    [SerializeField] private GameObject tutorialGameObject;

    [Header("Events")]
    [SerializeField] private UnityEvent OnEnableEvent;
    [SerializeField] private UnityEvent OnDisableEvent;

    private void Awake() {
        disaster = GetComponent<Disaster>();
        
        gameObject.SetActive(CanEnable());

        if (CanEnable()) CheckTutorial();
    }

    public bool CanEnable(){
        if (!CheckDisaster()) return false;

        return true;
    }

    private bool CheckDisaster(){
        foreach (FEarthDisasters dis in Enum.GetValues(typeof(FEarthDisasters)))
        {
            if (reqDisaster != FEarthDisasters.None && (GameManager.instance.UnlockedDisasters & reqDisaster) == reqDisaster) return true;
        }
        
        return false;
    }

    private void OnEnable() {
        OnEnableEvent.Invoke();
    }

    private void OnDisable() {
        OnDisableEvent.Invoke();
    }

    public void CheckTutorial(){
        if (!bTutorial) return;

        foreach (FTutorials tutorial in Enum.GetValues(typeof(FTutorials)))
        {
            if (tutorial != FTutorials.None && (GameManager.instance.UnlockedTutorials & tutorial) == tutorial)
            {
                return;
            }
        }

        GameManager.instance.AddTutorial(tutorial);
        ShowTutorial();
    }

    private void ShowTutorial(){
        
    }
}
