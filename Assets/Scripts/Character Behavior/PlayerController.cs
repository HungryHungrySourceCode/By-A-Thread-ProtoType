using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    [Header("Initialization")]
    public static PlayerController player;
    public NavMeshAgent playerNavAgent;
    public Animator playerAnimator;
    public UnitAnimator playerAnimatorScript;

    public Camera currentCamera;
    public Block blockScript;
    public Health playerHealthScript;
    public Stamina playerStaminaScript;

    public Transform shootPosition;
    public float rotationOffset = 0f;

    public float startAcceleration;
    //this section ^ will hold the startup info that the script needs.



    [Header("Attributes")]
    //movement
    public float baseMovementSpeed = 6f;
    public float sprintMultiplier = 2f;

    //dodge handeling
    public float dodgeDistance = 5f;
    public float dodgeSpeed = 50f;
    public float dodgeAcceleration = 500f;


    public float currentSpeed;
    //this section ^ will hold stats and attributes



    [Header("RunTime Values")]
    public Vector3 currentDirection;
    public Vector3 dodgeDirection;
    public bool isDodging = false;
    public bool canDodge = true;
    public bool isBlocking = true;


    [Header("RangedCombat")]
    public GameObject Projectile;


    private void Awake()
    {
        playerNavAgent.speed = baseMovementSpeed;
        startAcceleration = playerNavAgent.acceleration;
        instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {
        LookAtCursor();

        DoMovement();

        CheckIfCanDodge();

        if (Input.GetMouseButtonDown(0))
        {
            BasicAttack();
        }


            Block();

    }

    public void BasicAttack()
    {
        playerAnimator.SetTrigger("Attack");
    }

    public void Block()
    {
        if (Input.GetMouseButton(1))
        {
            blockScript.StartBlock();
        }
        else
        {
            blockScript.StopBlock();
        }
    }
    
    //Player Control
    public float GetRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Vector3 difference = hit.point - transform.position;
           difference.Normalize();
            float rotZ = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg; //find the angle in degrees
            return rotZ;
          //  Debug.Log(difference);
        }
        else
        {
            return 90f;
        }

    }

    public void LookAtCursor()
    {
        transform.rotation = Quaternion.Euler(0f, GetRotation() + rotationOffset, 0f);
    }

    public void CheckIfCanDodge()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDodging == false && canDodge == true)
        {
            StartCoroutine(Dodge());
        }
    }

    //Movement
    public void DoMovement()
    {
        if (Input.GetAxis("Horizontal") != 0f)
        {
            currentDirection.x = Input.GetAxis("Horizontal") * baseMovementSpeed;
        }
        else
        {
            currentDirection.x = 0f;
        }
        if (Input.GetAxis("Vertical") != 0f)
        {
            currentDirection.z = Input.GetAxis("Vertical") * baseMovementSpeed;
        }
        else
        {
            currentDirection.z = 0f;
        }

        if (isDodging == true)
            return;
        playerNavAgent.velocity = currentDirection;
    }

    public Vector3 GetDodgeDirection()
    {
        Vector3 pos = transform.position;
        return dodgeDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    }

    int dodgeChecks;
    public IEnumerator Dodge()
    {
        dodgeChecks = 0;
        Vector3 dodgeToPosition;
       playerNavAgent.SetDestination((GetDodgeDirection().normalized * dodgeDistance) + transform.position);
        dodgeToPosition = playerNavAgent.destination;
        playerNavAgent.speed = dodgeSpeed;
        isDodging = true;
       playerNavAgent.acceleration = dodgeAcceleration;

        while (true)
        {
            dodgeChecks++;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            if ((dodgeToPosition - transform.position).magnitude <= 1f | dodgeChecks >= 400f)
            {
                playerNavAgent.acceleration = startAcceleration;
                playerNavAgent.speed = baseMovementSpeed;
                playerNavAgent.destination = transform.position;
                dodgeToPosition = new Vector3(0f, 0f, 0f);
                isDodging = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                yield return new WaitForEndOfFrame();
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    //Combat
    public void Shoot()
    {
        GameObject projectileClone = (GameObject)Instantiate(Projectile, shootPosition.position, shootPosition.rotation);
        BallisticProjectile projScript = projectileClone.GetComponent<BallisticProjectile>();
        if (isDodging == false)
            projScript.inheritedVelocity = playerNavAgent.velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(shootPosition.position, Vector3.forward);
    }
}
