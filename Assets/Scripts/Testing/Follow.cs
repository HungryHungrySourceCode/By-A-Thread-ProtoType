using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    public Transform target;
    public float speed = 1f;
    public float height = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 correctedPos = target.position + Vector3.up * height;
        transform.position = correctedPos;
	}
}
