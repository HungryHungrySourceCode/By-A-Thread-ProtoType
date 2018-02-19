using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bee : MonoBehaviour
{
    public float actionUpdateTime = 0.5f;
    public float pauseTime = 0.5f;
    public float moveSpeed = 4f;
    public float runSpeed = 8f;
    public float turnSpeed = 1f;

    public float detectDistance = 6f;
    public float wanderDistance = 20f;

    public float wanderUpdateTime = 2f;
    public Vector3 startPoint;
    private Transform player;
    public float shootPlayerDistance;
    public GameObject projectile;
    public Transform shootPosition;

    public bool engagedInCombat
    {
        get
        {
            return engagedInCombat;
        }
        set
        {
            engagedInCombat = value;
        }
    }

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
    void Start()
    {
        player = GameMood.instance.player;
        startPoint = transform.position;
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
                StartCoroutine(Combat());
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
            navAgent.SetDestination(dir * 2f);
            navAgent.speed = runSpeed;

            if (dir.magnitude >= detectDistance)
            {
                navAgent.speed = moveSpeed;
                StartCoroutine(Wander());
                yield break;
            }
            yield return new WaitForSeconds(actionUpdateTime);
        }
    }

    public IEnumerator Combat()
    {
        while (true)
        {
            Vector3 dir = (transform.position - player.position);

            transform.LookAt(player);
            int action = (int)Random.Range(0, 3);
            if (action == 0)
            {
                transform.LookAt(player);
                Shoot();
                yield return new WaitForSeconds(pauseTime);
            }
            else if (action == 1)
            {
                transform.LookAt(player);
                Shoot();
                yield return new WaitForSeconds(pauseTime);
            }
            else
            {
                transform.LookAt(player);
                Shoot();
                yield return new WaitForSeconds(pauseTime);
            }

            if (dir.magnitude >= detectDistance)
            {
                navAgent.speed = moveSpeed;
                StartCoroutine(Wander());
                yield break;
            }
            yield return new WaitForSeconds(actionUpdateTime);
        }
    }

    public void Shoot()
    {
        GameObject projectileClone = (GameObject)Instantiate(projectile, shootPosition.position, shootPosition.rotation);
        BallisticProjectile projScript = projectileClone.GetComponent<BallisticProjectile>();
    }
}
