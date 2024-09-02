using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidgetChaosController : MonoBehaviour
{
    private Slider chaosSlider;

    private void Awake() {
        chaosSlider = GetComponent<Slider>();
    }

    private void Update() {
        chaosSlider.value = GameManager.instance.Chaos;
    }
}
