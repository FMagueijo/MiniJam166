using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Tornado : Disaster
{
    [Header("Tornado Gameplay")]
    [SerializeField] bool bSpawned;
    [SerializeField] private float minWait;
    [SerializeField] private float maxWait;
    [SerializeField] private float dragSpeed;
    [SerializeField] private float counterSpeed;
    [SerializeField] private float defaultDistance;
    [SerializeField] private float dragDistance;

    [Header("Tornado Stats")]
    [SerializeField] private float coreDistance;
    [SerializeField] private Vector3 lastMousePosition;

    [Header("Tornado Components")]
    [SerializeField] private Transform pivot;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        TargetTickIntervalTimer = 0;
    }

    public override void Enter()
    {
        RespawnTime();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();
        coreDistance = Vector2.Distance(pivot.position, transform.position);

        // Timed Spawn
        if (!bSpawned) Spawn();

        if (coreDistance > defaultDistance && bSpawned)
        {
            transform.position -= transform.up * counterSpeed * Time.deltaTime;
        }
    }

    private void Spawn()
    {
        bSpawned = true;
        TargetTickIntervalTimer = 0;
        animator.SetInteger("State", 1); // Startup Animation

        if (pivot == null) return;

        float randomAngle = Random.Range(0, 360);
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(pivot.position, axis, randomAngle);

        transform.position = pivot.position + transform.up * defaultDistance;
    }

    private void OnMouseDrag()
    {
        if (!bSpawned) return;

        Vector3 mouseDelta = Input.mousePosition - lastMousePosition; 
        Vector3 localDragDirection = transform.InverseTransformDirection(mouseDelta); 

        if (localDragDirection.y > 0)
        {
            transform.position += transform.up * dragSpeed * Time.deltaTime;
            coreDistance = Vector2.Distance(pivot.position, transform.position);

            if (coreDistance > dragDistance)
            {
                Die();
            }
        }
        

        lastMousePosition = Input.mousePosition;
    }

    private void Die()
    {
        bSpawned = false;
        animator.SetInteger("State", 0); // Death Animation
        RespawnTime();
    }

    

    private void RespawnTime()
    {
        TargetTickIntervalTimer = Random.Range(minWait, maxWait);
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override float GetChaos()
    {
        return base.GetChaos();
    }
}
