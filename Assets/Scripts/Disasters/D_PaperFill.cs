using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_PaperFill : Disaster
{
    public List<GameObject> papers;

    public Vector2 minArea;
    public Vector2 maxArea;

    public override void Enter()
    {
        base.Enter();

        TargetTickIntervalTimer = Random.Range(.5f, Mathf.Max(2.4f - (.3f * GameManager.instance.Difficulty), .8f));
    }

    public override void Tick()
    {
        base.Tick();

        Spawn();
        TargetTickIntervalTimer = Random.Range(.5f, Mathf.Max(2.4f - (.3f * GameManager.instance.Difficulty), .8f));
    }

    public void Spawn(){
        Vector2 goPos = new Vector2(Random.Range(minArea.x, maxArea.x), Random.Range(minArea.y, maxArea.y));
        Vector3 goRot = new Vector3(0,0, Random.Range(-25f, 25f));

        Instantiate(papers[Random.Range(0, papers.Count)], goPos, Quaternion.Euler(goRot));
    }
}
