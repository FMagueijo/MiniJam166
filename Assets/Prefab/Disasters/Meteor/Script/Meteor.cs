 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Transform pivot;
    public float speed = .5f;
    
    private void Update() {
        if (pivot == null || !GameManager.instance.IsDisasterOn) return;

        Vector3 direction = pivot.position - transform.position;
        transform.position += direction.normalized * (speed * GameManager.instance.Difficulty) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<Earth>()) {
            GameManager.instance.AddChaos(Mathf.Min(.06f * GameManager.instance.Difficulty, .15f));
            Destroy(gameObject);    
        }
    }

    private void OnMouseDown() {
        Destroy(gameObject);
    }
}
