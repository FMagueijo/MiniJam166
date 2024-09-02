using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Meteor : Disaster
{
    public GameObject meteorPrefab;
    public float defaultDistance = 4f;
    public Transform pivot;

    public override void Enter()
    {
        base.Enter();
        TargetTickIntervalTimer = Random.Range(1.5f, 5 - 3 * Mathf.Clamp01(GameManager.instance.Difficulty));
    }

    public override void Tick()
    {
        base.Tick();

        for(int i = 0; i < Mathf.Min(Random.Range(1, 2.55f * GameManager.instance.Difficulty), 4); i++){
            SpawnMeteor();
        }

        TargetTickIntervalTimer = Random.Range(1.5f, 5-3*Mathf.Clamp01(GameManager.instance.Difficulty));
    }

    public override float GetChaos()
    {
        return 0;
    }

    private void SpawnMeteor(){
        if (meteorPrefab == null) return;

        if (pivot == null) return;

        float randomAngle = Random.Range(0, 360);
        Vector3 axis = new Vector3(0, 0, 1);

        GameObject go = Instantiate(meteorPrefab);
        go.GetComponent<Meteor>().pivot = pivot;

        go.transform.RotateAround(pivot.position, axis, randomAngle);

        go.transform.position = pivot.position + go.transform.up * defaultDistance;

    }
}
