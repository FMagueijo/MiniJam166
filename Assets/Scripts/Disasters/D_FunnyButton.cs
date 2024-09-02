using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_FunnyButton : MonoBehaviour
{
    public void ClickButton(){
        if (!GameManager.instance.IsDisasterOn) return;

        GameManager.instance.Die();
    }
}
