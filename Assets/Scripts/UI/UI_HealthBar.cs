using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_HealthBar : MonoBehaviour
{
    public Health healthToDisplay;
    public Slider thisSlider;

    void Update()
    {
        thisSlider.maxValue = healthToDisplay.maxHealth;
        thisSlider.value = healthToDisplay.currentHealth;
    }
}