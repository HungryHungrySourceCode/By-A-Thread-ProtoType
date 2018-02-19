using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_SanityBar : MonoBehaviour {
    public Sanity sanityToDisplay;
    public Slider thisSlider;

	void Update () {
        thisSlider.maxValue = sanityToDisplay.maxSanity;
        thisSlider.value = sanityToDisplay.currentSanity;
	}
}
