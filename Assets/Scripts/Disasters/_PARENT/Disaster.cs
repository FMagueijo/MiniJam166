using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disaster : MonoBehaviour
{
    public enum FInitState { UnInit, Init }
    public enum FEnterOption { Never, TimedStart, UnityStart, GMStart }
    public enum FExitOption { Never, TimedExit, GMStart }
    public enum FTickStartOption { UnityStart, DisasterDependent, TimedTick }


    [Header("Disaster Booleans")]
    [SerializeField] public bool bDisasterInit = false;
    [SerializeField] public bool bCanTick = false;
    [SerializeField] public bool bForceDisable = false;
    [SerializeField] public bool bDisableTimers = false;
    [SerializeField] public bool bDontDestroyOnDie = false;


    [Header("Events Options")]
    [SerializeField] private FEnterOption startOption;
    public FEnterOption StartOption => startOption;
    [SerializeField] private FExitOption exitOption;
    public FExitOption ExitOption => exitOption;
    [SerializeField] private FTickStartOption tickStartOption;
    public FTickStartOption TickStartOption => tickStartOption;


    [Header("Events Timers")]
    [SerializeField, Min(0)] private float StartTimer;
    [SerializeField, Min(0)] protected float TargetStartTimer;

    [SerializeField, Min(0)] private float ExitTimer;
    [SerializeField, Min(0)] protected float TargetExitTimer;

    [SerializeField, Min(0)] private float TickStartTimer;
    [SerializeField, Min(0)] protected float TargetTickStartTimer;


    [Header("Tick Intervals")]
    [SerializeField, Min(0)] private float TickIntervalTimer;
    [SerializeField, Min(0)] protected float TargetTickIntervalTimer;

    [SerializeField, Min(0)] private float PhysicsIntervalTimer;
    [SerializeField, Min(0)] protected float TargetPhysicsIntervalTimer;

    [Header("Disaster Stats")]
    [SerializeField, Min(0)] protected float chaos;

    private void Start()
    {
        if(startOption == FEnterOption.UnityStart) Enter();
    }

    private void Update()
    {
        chaos = GetChaos();

        if (!bDisableTimers) UpdateTimers();

        if (!GameManager.instance.IsDisasterOn || bForceDisable) return;

        if (tickStartOption == FTickStartOption.DisasterDependent && !bDisasterInit) return;
        
        if (tickStartOption == FTickStartOption.TimedTick && !bCanTick) return;

        if (TickIntervalTimer >= TargetTickIntervalTimer){
            Tick();
            TickIntervalTimer = 0;
        }

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsDisasterOn || bForceDisable) return;

        if (tickStartOption == FTickStartOption.DisasterDependent && !bDisasterInit) return;

        if (PhysicsIntervalTimer >= TargetPhysicsIntervalTimer && bCanTick)
        {
            PhysicsTick();
            PhysicsIntervalTimer = 0;
        }
    }

    private void UpdateTimers(){

        {
            if(startOption == FEnterOption.TimedStart){
                if (StartTimer < TargetStartTimer){
                    StartTimer += Time.deltaTime;

                    if (StartTimer > TargetStartTimer) Enter();
                }
                
            }
        }

        {
            if(exitOption == FExitOption.TimedExit){
                if (ExitTimer < TargetExitTimer){
                    ExitTimer += Time.deltaTime;

                    if (ExitTimer > TargetExitTimer) Exit();
                }
            }
        }

        {
            if (tickStartOption == FTickStartOption.TimedTick)
            {
                if (TickStartTimer < TargetTickStartTimer)
                {
                    TickStartTimer += Time.deltaTime;

                    if (TickStartTimer > TargetTickStartTimer) bCanTick = true;
                }
            }
        }

        {
            if (TargetTickIntervalTimer > 0) TickIntervalTimer += Time.deltaTime;
            if (TargetPhysicsIntervalTimer > 0) PhysicsIntervalTimer += Time.deltaTime;
        }


    }

    

    public virtual void Enter() { bDisasterInit = true; }
    public virtual void Tick() { }
    public virtual void PhysicsTick() { }
    public virtual void Exit() { bForceDisable = true; }
    public virtual float GetChaos() { return 0; }

}
