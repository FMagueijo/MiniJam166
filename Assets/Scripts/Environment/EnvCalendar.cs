using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCalendar : MonoBehaviour
{
    public TMPro.TMP_Text TXTDay;
    
    void Start()
    {
        TXTDay.text = $"Day\n{GameManager.instance.DaysPassed}";    
    }

}
