using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurricaneWind : MonoBehaviour {
    public static HurricaneWind instance1;
    public Vector3 centerPoint;
    public float hurricaneEyeSize;
    public float scale;
    public float baseWindSpeed;
    public float windSpeedTowardsCenterMultiplier;
    public float angleOffset;
    public float objectDragTest;
    public float angleOffsetTest = 150f;

    public GameObject[] testTargets;

    //note there's another way i might also be able to do this.
    //rotate and object at a constant rate and apply motion to objects based on the rotation, with a steady pull towards the center.

    private void Awake()
    {
        instance1 = this;
    }

    public Vector3 GetWindVelocity(Vector3 position)
    {
        Vector3 difference = position - centerPoint;
        Vector3 XZDifference = new Vector3(difference.x, 0, difference.y);
        float r = XZDifference.magnitude;
        float Theta = GetXZAngleFromCenter(position, difference);

        float spiral = r * Theta;



        //Vector3 newAngle = new Vector3(r * Mathf.Cos(Theta), 0, r * Mathf.Sin(Theta));

        return Quaternion.AngleAxis(-Theta + angleOffsetTest, Vector3.up) * Vector3.right;

        //return newAngle;
    }

    public float GetXZAngleFromCenter(Vector3 position, Vector3 difference)
    {
        difference.Normalize();

        float rotY = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg; //find the angle in degrees
       // Quaternion lookRotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.Euler(0, 0, rotZ + offset), Time.deltaTime * rotSpeed);
      //  partToRotate.rotation = lookRotation;


        return rotY;
    }

    public Vector3 GetAngle(Vector3 difference, Vector3 position)
    {
        float angle = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(-angle, Vector3.up) * Vector3.right;
    }

    private float AngleTo(Vector2 pos, Vector2 target)
    {
        Vector2 diference = target - pos;
        float sign = (target.y < pos.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    private void Update()
    {
       // Debug.Log(GetWindVelocity(testTarget.transform.position));
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawRay((centerPoint - testTarget.transform.position), centerPoint);
        foreach (var testTarget in testTargets)
        {
            //Gizmos.DrawRay(centerPoint, (testTarget.transform.position - centerPoint));
            Gizmos.color = Color.green;
           Gizmos.DrawRay(centerPoint, GetAngle((testTarget.transform.position - centerPoint), centerPoint) * (centerPoint - testTarget.transform.position).magnitude);

            Gizmos.color = Color.blue;
           Gizmos.DrawRay(testTarget.transform.position, GetWindVelocity(testTarget.transform.position));

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(testTarget.transform.position, 0.05f);
            Gizmos.DrawWireSphere(centerPoint, 0.05f);
        }

    }

}
