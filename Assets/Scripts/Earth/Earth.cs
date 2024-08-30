using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EarthState { Healthy = 0, Unhealthy = 1, Dying = 2, Dead = 3 }

public class Earth : MonoBehaviour
{

    [Header("Earth Stats")]
    [SerializeField] private EarthState state;

    //Components
    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (animator != null) return;

        
    }

    //Getters
    public EarthState State => state;


    //Setters
    public void SetState(EarthState state) { 
        this.state = state; 
    }


    
}
