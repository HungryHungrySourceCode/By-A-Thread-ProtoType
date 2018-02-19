using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Unit : MonoBehaviour {
    [Header("Start Variables")]
    public NavMeshAgent navAgent;
    public float startAcceleration;
    public Transform shootPosition;


    [Header("Attributes")]
    public float baseMovementSpeed = 3f;

    [Header("Movement")]
    public float dodgeSpeed;
    public float dodgeAcceleration;
    public float dodgeDistance;


    [Header("Runtime Variables")]
    public bool isDodging = false;


    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        startAcceleration = navAgent.acceleration;
    }

    //Movement Handeling and navigation
    public void Move(Vector3 position)
    {
        navAgent.SetDestination(position);
    }

    public float CheckDist()
    {
        return (transform.position - navAgent.destination).magnitude;
    }

    public float CheckDist(Transform _Target)
    {
        return (transform.position - _Target.position).magnitude;
    }

    public void StartDodge(Vector3 objToAvoid)
    {
        if (isDodging == true)
            return;

        Vector3 currentPos = transform.position;
        Vector3 angle = (currentPos - objToAvoid);
        StartCoroutine(Dodge(angle, dodgeDistance));
    }

    int dodgeChecks;
    public IEnumerator Dodge(Vector3 direction, float distance)
    {
        dodgeChecks = 0;
        Vector3 dodgeToPosition;
        navAgent.SetDestination((direction.normalized * distance) + transform.position);
        dodgeToPosition = navAgent.destination;
        navAgent.speed = dodgeSpeed;
        isDodging = true;
        navAgent.acceleration = dodgeAcceleration;

        while (true)
        {
            dodgeChecks++;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            if ((dodgeToPosition - transform.position).magnitude <= 3f | dodgeChecks >= 10f)
            {
                navAgent.acceleration = startAcceleration;
                navAgent.speed = baseMovementSpeed;
                dodgeToPosition = new Vector3(0f, 0f, 0f);
                isDodging = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public Vector3 GetPositionFromCenter(float Angle, float distance)
    {
        return Quaternion.AngleAxis(-Angle, Vector3.up) * Vector3.right * distance;
    }

    public Vector3 RushToDistanceFromPosition(float desiredDistance, Vector3 position)
    {
        Vector3 angle = transform.position - position;
        return position + (angle.normalized * desiredDistance);
    }
}
