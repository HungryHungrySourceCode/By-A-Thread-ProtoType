using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyWind : MonoBehaviour {
    public float power;
    public Rigidbody thisRigidBody;
    public HurricaneWind instance;

	// Use this for initialization
	void Start () {
        instance = HurricaneWind.instance1;
      //  power = Random.Range(power, 12f);
	}
	
	// Update is called once per frame
	void Update () {
        thisRigidBody.AddRelativeForce(instance.GetWindVelocity(transform.position) * power * HurricaneWind.instance1.baseWindSpeed);
        thisRigidBody.drag = HurricaneWind.instance1.objectDragTest;
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, instance.GetWindVelocity(transform.position) + instance.GetWindVelocity(transform.position) * (transform.position - instance.centerPoint).magnitude * 0.1f);
    }
}
