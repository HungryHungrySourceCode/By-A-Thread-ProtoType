using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SKellymime_TestAI : MonoBehaviour {

    public float actionUpdateTime = 0.5f;
    public float moveSpeed = 4f;
    public float runSpeed = 8f;
    public float turnSpeed = 1f;

    public float detectDistance = 6f;
    public float wanderDistance = 20f;

    public float wanderUpdateTime = 2f;
    public Vector3 startPoint;
    private Transform player;

    public UnitAnimator skellyAnimator;

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

    private void Update()
    {
    if (navAgent.speed >= 1f)
        {
            skellyAnimator.ChangeBool("Moving", true);
        }
    else
        {
            skellyAnimator.ChangeBool("Moving", false);
        }
    }

    public IEnumerator Wander()
    {
        navAgent.SetDestination(randomPoint);

        while (true)
        {
            Vector3 dir = (player.position - transform.position);
            if (dir.magnitude <= detectDistance)
            {
                StartCoroutine(Agro(player));
                yield break;
            }

            if ((navAgent.destination - transform.position).magnitude <= 3f)
            {
                navAgent.SetDestination(randomPoint);
            }
            yield return new WaitForSeconds(wanderUpdateTime);
        }
    }

    public IEnumerator Agro(Transform threat)
    {
        while (true)
        {
            Vector3 dir = (transform.position - threat.position);
            Vector3 newDir = new Vector3(threat.position.x - transform.position.x, transform.position.y, threat.position.z - transform.position.z);
            navAgent.SetDestination(player.position);
            navAgent.speed = runSpeed;

            if (dir.magnitude >= detectDistance * 3f)
            {
                navAgent.speed = moveSpeed;
                StartCoroutine(Wander());
                yield break;
            }

            if (dir.magnitude <= 1f)
            {
                player.GetComponent<Health>().TakeDamage(5f);
                skellyAnimator.unitAnimator.SetTrigger("Attack");
            }
            yield return new WaitForSeconds(actionUpdateTime);
        }
    }
}
