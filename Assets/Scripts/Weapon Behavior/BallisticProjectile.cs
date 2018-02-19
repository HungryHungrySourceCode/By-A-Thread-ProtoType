using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticProjectile : MonoBehaviour {
    [Header("Attributes")]
    public bool inheritVelocity = true;
    public float damage = 10f;

    public float constantPower = 0f;
    public float initialPower = 200f;
    public int richochetTimes = 0;

    public bool setHeight = true;
    public float projectileHeight = 1.5f;

    [Header("Initialization")]
    public Rigidbody thisRigidBody;
    public Collider hitBox;
    public GameObject model;

    [Header("RunTime Values")]
    public Vector3 inheritedVelocity;
    private int currentRichochetNumber = 0;
    public bool landed;








    public void Start()
    {
        thisRigidBody.AddRelativeForce(Vector3.forward * initialPower);
        Destroy(gameObject, 5f);
        if (inheritVelocity == false)
            return;
        thisRigidBody.velocity = inheritedVelocity;


    }

    public void Update()
    {
        //    thisRigidBody.AddRelativeForce(Vector3.forward * constantPower);
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        if (setHeight == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0f))
                transform.position = new Vector3 (transform.position.x, hit.point.y + projectileHeight, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ricochet(collision);

        try
        {
           collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        catch
        {
            landed = true;
        }
       // Destroy(gameObject);
    }

    public void Ricochet(Collision collision)
    {
        if (currentRichochetNumber <= richochetTimes)
       {

            currentRichochetNumber++;
            ContactPoint contact = collision.contacts[0];
            Vector3 newRot = Vector3.Reflect(thisRigidBody.velocity, collision.contacts[0].normal);
       thisRigidBody.AddRelativeForce(newRot * 3f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
