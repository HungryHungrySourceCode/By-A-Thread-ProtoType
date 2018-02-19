using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementTest : MonoBehaviour {
    public NavMeshAgent thisNavAgent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                thisNavAgent.SetDestination(hit.point);
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            thisNavAgent.destination = transform.position;
        }
	}
}
