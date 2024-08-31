using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Flags]
public enum FTutorials
{
    None = 0,
    Basic = 1 << 0,
    Temperature = 2 << 1,
    Asteroids = 2 << 1,
    Tornado = 3 << 2,

    //TODO: Add as we create more tutorials
};

[Flags] public enum FEarthDisasters {
    None = 0,
    Temperature = 1 << 0,
    Asteroids = 2 << 1,
    Tornado = 3 << 2,

    //TODO: Add as we create more disasters
};

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Gameplay Info")]
    [SerializeField] private bool bAlive = true;
    [SerializeField] private int daysPassed = 0;
    [SerializeField, Range(0,1)] private float chaosMeter = 0;
    [SerializeField, Min(1)] private float difficulty = 1;


    [Header("Visuals")]
    Texture2D cursor;


    [Header("Unlockables")]
    [SerializeField] private FEarthDisasters unlockedDisasters;
    [SerializeField] private FTutorials unlockedTutorials;


    [Header("Disasters Info")]
    [SerializeField] private bool bDayActive = false;
    [SerializeField] private bool bDisasterOn = false;
    [SerializeField] private List<Disaster> loadedDisasters; 

    private void Start() {
        //Cursor.visible = false;

        StartDay();
    }

    public void StartDay()
    {
        bDisasterOn = true;
        bDayActive = true;
        chaosMeter = 0;
        loadedDisasters = GameObject.FindObjectsOfType<Disaster>().ToList();
    }

    private void Update() {
        if (bDayActive) TickDay();
    }

    public void TickDay(){
        CalculateChaos();
        
        if(chaosMeter >= 1){
            Die();
        }
    }

    public void Die(){
        chaosMeter = 1;
        bDayActive = false;
        bAlive = false; 
    }

    public void CalculateChaos(){
        float totalChaos = 0;
        
        foreach(Disaster d in loadedDisasters){
            totalChaos += d.GetChaos();
        }

        totalChaos /= loadedDisasters.Count;
    }


    public void ExitDay()
    {
        bDisasterOn = false;
    }


    //Getters
    public int DaysPassed => daysPassed;
    public float Chaos => chaosMeter;
    public float Difficulty => difficulty;

    public FEarthDisasters UnlockedDisasters => unlockedDisasters;
    public FTutorials UnlockedTutorials => unlockedTutorials;

    public bool IsDisasterOn => bDisasterOn;


    //Setters

    public void AddDisaster(FEarthDisasters disaster){
        unlockedDisasters |= disaster;
    }

    public void AddTutorial(FTutorials tutorial)
    {
        unlockedTutorials |= tutorial;
    }
}
