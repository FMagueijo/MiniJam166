using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public void Click(){
        GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        Destroy(gameObject, 3f);
    }
}
