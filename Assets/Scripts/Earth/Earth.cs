using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EarthState : int { Healthy = 0, Unhealthy = 1, Dying = 2, Dead = 3 }

public class Earth : MonoBehaviour
{

    [Header("Earth Stats")]
    [SerializeField] private EarthState state;

    //Components
    private Animator animator;
    private AudioSource audioSource;


    private void Awake() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {

        UpdateChaos();

    }

    private void UpdateChaos(){
        if (animator == null) return;

        float currChaos = GameManager.instance.Chaos;
        SetState(currChaos);
    }

    //Getters
    public EarthState State => state;


    //Setters
    public void SetState(float chaos) {
        if(state == EarthState.Dead) return;

        if(chaos >= 1) state = EarthState.Dead;
        else if (chaos > .66) state = EarthState.Dying;
        else if (chaos > .33) state = EarthState.Unhealthy;
        else state = EarthState.Healthy;


        animator.SetInteger("State", (int) state);
    }

    public void PlayDeathSFX(){
        audioSource.Play();
    }

    
}
