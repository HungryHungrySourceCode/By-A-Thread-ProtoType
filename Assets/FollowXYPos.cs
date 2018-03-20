using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowXYPos : MonoBehaviour {
    public Transform target;
    public float followSpeed;
    private float startheightdif;
	// Use this for initialization
	void Start () {
        startheightdif = target.position.y - transform.position.y;
	}

    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y - startheightdif, target.position.z);

        if ((transform.position - targetPos).magnitude >= 1f)
        {
            transform.position += ((targetPos - transform.position) * followSpeed);
        }
    }
}
