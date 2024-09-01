using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Flags]
public enum FTutorials
{
    None = 0,
    Basic = 1 << 0,
    Temperature = 1 << 1,
    Tornado = 1 << 2,
    Moon = 1 << 3,
    Meteor = 1 << 4,

    //TODO: Add as we create more tutorials
};

[Flags] public enum FEarthDisasters {
    None = 0,
    Temperature = 1 << 0,
    Tornado = 1 << 1,
    Moon = 1 << 2,
    Meteor = 1 << 3,

    //TODO: Add as we create more disasters
};

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Gameplay Info")]
    [SerializeField] private bool bAlive = true;
    [SerializeField] private bool bDone = false;
    [SerializeField] private int daysPassed = 1;
    [SerializeField, Range(0,1)] private float chaosMeter = 0;
    [SerializeField, Range(0, 1)] private float chaosDec = .025f;
    [SerializeField, Range(0, 1)] private float chaosAdd = .1f;
    [SerializeField, Min(1)] private float difficulty = 1;

    [Header("Timers")]
    public float endTimer = 0;
    public float timeLeft = 90;

    public UnityEvent OnDayEnd = new UnityEvent();

    [Header("Visuals")]
    Texture2D cursor;


    [Header("Unlockables")]
    [SerializeField] private FEarthDisasters unlockedDisasters;
    [SerializeField] private FTutorials unlockedTutorials;


    [Header("Disasters Info")]
    [SerializeField] private bool bDayActive = false;
    [SerializeField] private bool bDisasterOn = false;
    [SerializeField] private List<Disaster> loadedDisasters;

    private void Awake() {
        
        AddRandomDisaster(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start() {
        StartDay();
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartDay();
    }

    public void StartDay()
    {
        bDisasterOn = true;
        bDayActive = true;
        bAlive = true;
        chaosMeter = 0;
        loadedDisasters = GameObject.FindObjectsOfType<Disaster>().ToList();

    }

    private void Update() {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) ClearInstance();

        if (bDayActive) TickDay();
        else{
            if (!bDone || endTimer > 3.5f) return;

            endTimer += Time.deltaTime;
            if(endTimer > 3.5f){
                
                ExitDay();
            }
        }
    }

    public void AddRandomDisaster(int x){

        FEarthDisasters[] allDisasters = Enum.GetValues(typeof(FEarthDisasters))
                                             .Cast<FEarthDisasters>()
                                             .Where(d => d != FEarthDisasters.None && !unlockedDisasters.HasFlag(d))
                                             .ToArray();

        System.Random random = new System.Random();
        allDisasters = allDisasters.OrderBy(x => random.Next()).ToArray();

        for (int i = 0; i < x && i < allDisasters.Length; i++)
        {
            unlockedDisasters |= allDisasters[i];
        }
    }

    public void TickDay(){
        
        if(chaosMeter >= 1){
            Die();
        }
        else{
            CalculateChaos();
        }

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                bDone = true;
                bDayActive = false;
                bDisasterOn = false;
            }
        }
    }
    
    public void NextDay()
    {
        AddRandomDisaster(1);

        
        daysPassed++;
        difficulty += .3f;
        timeLeft = 90;
        

        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        AddRandomDisaster(1);


        daysPassed++;
        difficulty += .3f;
        timeLeft = 90;


        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1f;
    }

    public void Die(){
        chaosMeter = 1;
        daysPassed = 1;

        foreach (Disaster d in loadedDisasters){
            if (d.bDontDestroyOnDie) continue;

            Destroy(d.gameObject);
        }

        bDone = true;
        bDayActive = false;
        bDisasterOn = false;
        bAlive = false;
    }

    public void CalculateChaos(){
        float totalChaos = 0;
        
        foreach(Disaster d in loadedDisasters){
            totalChaos += Mathf.Clamp01(d.GetChaos()) * chaosAdd;
        }

        chaosMeter += (totalChaos - (chaosDec + totalChaos * .3f)) * Time.deltaTime;
        chaosMeter = Mathf.Clamp01(chaosMeter);
    }


    public void ExitDay()
    {
        bDisasterOn = false;
        
        chaosMeter = 0;
        endTimer = 0;

        loadedDisasters.Clear();

        bDayActive = false;
        bDisasterOn = false;
        bDone = false;

        OnDayEnd.Invoke();


        OnDayEnd.RemoveAllListeners();
    }


    //Getters
    public int DaysPassed => daysPassed;
    public float Chaos => chaosMeter;
    public float Difficulty => difficulty;
    public bool IsAlive => bAlive;

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
