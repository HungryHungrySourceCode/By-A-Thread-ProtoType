using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_StaminaBar : MonoBehaviour
{
    public Stamina StaminaToDisplay;
    public Slider thisSlider;

    void Update()
    {
        thisSlider.maxValue = StaminaToDisplay.maxStamina;
        thisSlider.value = StaminaToDisplay.currentStamina;
    }
}