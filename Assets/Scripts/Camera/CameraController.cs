using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("Initialization")]
    public Transform player;
    public PlayerController playerScript;
    public float followSpeed;
    public float height;
    public float z_offset;

    public float directionPredictStrength = 1f;
    public Vector3 targetPos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        targetPos = GetOffset();

        if ((transform.position - targetPos).magnitude >= 1f)
        {
            transform.position += ((targetPos - transform.position) * followSpeed);
        }
	}

    public Vector3 GetOffset()
    {
        return new Vector3(player.position.x + (playerScript.currentDirection * directionPredictStrength).x, player.position.y + height , player.position.z + z_offset + (playerScript.currentDirection * directionPredictStrength).z);
    }
}
