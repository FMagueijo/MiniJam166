using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Basic")]
    public GameObject obj;
    public bool loop = false;
    public float LoopInterval = 1f;

    [Header("Ammount")]
    public float minAmmount = 1;
    public bool minDiffDependent = false;
    public float maxAmmount = 1;
    public bool maxDiffDependent = false;

    [Header("Offset")]
    public Vector3 pos_offset;
    public Vector3 rot_offset;
    public Vector3 sca_offset;

    private void Awake() {
        if (loop) return;

        Spawn();
    }

    public void Spawn(){
        int _localAmmount = (int)Random.Range(minAmmount * (minDiffDependent ? GameManager.instance.Difficulty : 1), maxAmmount * (maxDiffDependent ? GameManager.instance.Difficulty : 1));

        for (int i = 0; i < _localAmmount; i++)
        {
            Transform _localTrans = transform;
            _localTrans.position += pos_offset;
            _localTrans.localEulerAngles += rot_offset;
            _localTrans.localScale += sca_offset;

            Instantiate(obj, _localTrans, true);
        }
    }

}
