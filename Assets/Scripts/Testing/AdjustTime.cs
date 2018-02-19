using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTime : MonoBehaviour {
    public float timeScale = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = timeScale;
	}
}
