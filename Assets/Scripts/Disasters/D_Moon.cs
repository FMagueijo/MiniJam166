using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Moon : Disaster
{
    [Header("Moon Gameplay")]

    [SerializeField] private float minStart;
    [SerializeField] private float maxStart;
    [SerializeField] private float dragForce;
    [SerializeField] private float counterForce;



    [Header("Tornado Stats")]
    [SerializeField] private float coreDistance;
    [SerializeField] private Vector3 lastMousePosition;

    [Header("Moon Components")]
    [SerializeField] private Transform pivot;

    private void Awake() {
        TargetStartTimer = Random.Range(minStart, maxStart);      
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override float GetChaos()
    {
        return base.GetChaos();
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<Earth>()){
            GameManager.instance.Die();
        }
    }
}
