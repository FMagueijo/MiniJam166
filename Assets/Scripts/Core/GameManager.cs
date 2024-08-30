using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum FTutorials
{
    None = 0,
    Basic = 1 << 0,
    Temperature = 2 << 1,

    //TODO: Add as we create more tutorials
};

[Flags] public enum FEarthDisasters {
    None = 0,
    Temperature = 1 << 0,

    //TODO: Add as we create more disasters
};

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Gameplay Info")]
    [SerializeField] private int daysPassed = 0;
    [SerializeField, Min(0)] private float chaosMeter = 0;
    [SerializeField, Min(1)] private float difficulty = 1;


    [Header("Unlockables")]
    [SerializeField] private FEarthDisasters unlockedDisasters;
    [SerializeField] private FTutorials unlockedTutorials;


    [Header("Disasters Info")]
    [SerializeField] private bool bDisasterOn = false;
    
    public void StartDay()
    {
        bDisasterOn = true;
    }

    public void ExitDay()
    {
        bDisasterOn = false;
    }


    //Getters
    public int DaysPassed => daysPassed;
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
