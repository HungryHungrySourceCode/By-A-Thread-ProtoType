using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFade : MonoBehaviour {
    public MeshRenderer[] roomObjects;
    public Transform centerPoint;
    public float fadeDist;
    public Transform player;

    public bool visible = false;

    private void Start()
    {
        InvokeRepeating("CheckDist", 0f, 1.5f);
    }

    private void CheckDist()
    {
        if ((player.position - centerPoint.position).magnitude <= fadeDist)
        {
            foreach (MeshRenderer ceilingPiece in roomObjects)
            {
                ceilingPiece.enabled = false;
            }
            visible = false;
        }



        if ((player.position - centerPoint.position).magnitude > fadeDist && visible == false)
        {
            foreach (MeshRenderer ceilingPiece in roomObjects)
            {
                ceilingPiece.enabled = true;
            }
            visible = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centerPoint.position, fadeDist);
    }
}
