using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class D_Temperature : Disaster
{
    [Header("Temperature Stats")]
    [SerializeField] private float currentTemperature;
    [SerializeField] private float normalTemperature;
    [SerializeField, Min(0)] private float marginTemperature;
    [SerializeField, Min(0)] private float badTemperatureDifference;


    [Header("Temperature Gameplay")]
    [SerializeField, Min(0)] private float incrementTemperature;
    [SerializeField, Min(0)] private float regulateTemperature;
    [SerializeField] private float minTemperature;
    [SerializeField] private float maxTemperature;


    [Header("Temperature Visuals")]
    [SerializeField] private TMPro.TMP_Text txtTemp;
    [SerializeField] private Color colorLowTemp;
    [SerializeField] private Color colorHighTemp;

    private void Awake() {

        currentTemperature = (int)Random.Range(normalTemperature - marginTemperature, normalTemperature + marginTemperature);
        incrementTemperature *= GameManager.instance.Difficulty;

        UpdateThermo();
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {

        base.Exit();
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override void Tick()
    {
        base.Tick();
        
        currentTemperature += (currentTemperature > normalTemperature) ? incrementTemperature : -incrementTemperature;
        currentTemperature = Mathf.Clamp(currentTemperature, minTemperature, maxTemperature);

        UpdateThermo();
    }

    protected void UpdateThermo(){
        
        float normalizedValue = Mathf.InverseLerp(minTemperature, maxTemperature, currentTemperature);
        normalizedValue = Mathf.Clamp01(normalizedValue);

        txtTemp.text = ((int)currentTemperature).ToString() + "C"; 
        txtTemp.color = Color.Lerp(colorLowTemp, colorHighTemp, normalizedValue);
    }

    public override float GetChaos()
    {
        float _chaos = Mathf.Abs(normalTemperature - currentTemperature) - marginTemperature;
        return _chaos <= 0 ? 0 : _chaos / badTemperatureDifference;
    }

    public void AddTemp(){
        if (!bCanTick || bForceDisable) return;

        currentTemperature += regulateTemperature;
    }

    public void SubTemp()
    {
        if (!bCanTick || bForceDisable) return;

        currentTemperature -= regulateTemperature;
    }
}
