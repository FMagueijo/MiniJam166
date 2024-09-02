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
    [SerializeField] private float defaultDistance;


    [Header("Moon Stats")]
    [SerializeField] private float coreDistance;
    [SerializeField] private Vector3 lastMousePosition;

    [Header("Moon Components")]
    [SerializeField] private Transform pivot;

    private void Awake() {
        TargetStartTimer = Random.Range(minStart, maxStart);

        if (pivot == null) return;

        float randomAngle = Random.Range(0, 360);
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(pivot.position, axis, randomAngle);

        transform.position = pivot.position + transform.up * defaultDistance;

        counterForce *= GameManager.instance.Difficulty;
        counterForce = Mathf.Clamp(counterForce, 0, dragForce - 1);
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
        coreDistance = Vector2.Distance(pivot.position, transform.position);
        
        transform.RotateAround(pivot.position, new Vector3(0,0,1), 15f * Time.deltaTime);

        if (coreDistance > 0)
        {
            Vector3 direction = pivot.position - transform.position;

            transform.position += direction.normalized * counterForce * Time.deltaTime;
            
        }
    }

    private void OnMouseDrag()
    {
        if (coreDistance >= defaultDistance) return;

        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        Vector3 localDragDirection = transform.InverseTransformDirection(mouseDelta);

        if (localDragDirection.y > 0)
        {
            Vector3 direction = pivot.position - transform.position;

            transform.position += transform.up * dragForce * Time.deltaTime;
            coreDistance = Vector2.Distance(pivot.position, transform.position);
        }


        lastMousePosition = Input.mousePosition;
    }

    public override void PhysicsTick()
    {
        base.PhysicsTick();
    }

    public override float GetChaos()
    {
        return base.GetChaos();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Earth>())
        {
            GameManager.instance.Die();
            Destroy(gameObject);
        }
    }

}
