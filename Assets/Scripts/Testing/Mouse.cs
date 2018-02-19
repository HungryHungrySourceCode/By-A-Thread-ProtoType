using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Mouse : MonoBehaviour
{
    public float actionUpdateTime = 0.5f;
    public float moveSpeed = 4f;
    public float runSpeed = 8f;
    public float turnSpeed = 1f;

    public float detectDistance = 6f;
    public float wanderDistance = 20f;

    public float wanderUpdateTime = 2f;
    public Vector3 startPoint;
    private Transform player;

    public Vector3 randomPoint
    {
        get
        {
            Vector3 adjusted = new Vector3(startPoint.x + Random.Range(-wanderDistance, wanderDistance), startPoint.y, startPoint.x + Random.Range(-wanderDistance, wanderDistance));
            return adjusted;
        }
        set
        {
            startPoint = value;
        }
    }

    public NavMeshAgent navAgent;
    // Use this for initialization

    private void Awake()
    {
        randomPoint = transform.position;
    }
    void Start()
    {
        player = GameMood.instance.player;
        navAgent.speed = moveSpeed;
        navAgent.angularSpeed = turnSpeed;
        StartCoroutine(Wander());
    }

    public IEnumerator Wander()
    {
        navAgent.SetDestination(randomPoint);

        while (true)
        {
            Vector3 dir = (player.position - transform.position);
            if (dir.magnitude <= detectDistance)
            {
                StartCoroutine(Run(player));
                yield break;
            }

            if ((navAgent.destination - transform.position).magnitude <= 3f)
            {
                navAgent.SetDestination(randomPoint);
            }
            yield return new WaitForSeconds(wanderUpdateTime);
        }
    }

    public IEnumerator Run(Transform threat)
    {
        while (true)
        {
            Vector3 dir = (transform.position - threat.position);
            Vector3 newDir = new Vector3(transform.position.x - threat.position.x, transform.position.y, transform.position.z - threat.position.z);
            navAgent.SetDestination(newDir * 20f);
            navAgent.speed = runSpeed;

            if (dir.magnitude >= detectDistance * 3f)
            {
                navAgent.speed = moveSpeed;
                StartCoroutine(Wander());
                yield break;
            }
            yield return new WaitForSeconds(actionUpdateTime);
        }
    }
}
