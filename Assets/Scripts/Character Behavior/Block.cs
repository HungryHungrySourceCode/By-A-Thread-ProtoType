using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public float blockDamageReduction = 0.75f;
    public bool isBlocking;
    public UnitAnimator playerAnimatorScript;
    public Health healthScript;

    public void Start()
    {
        healthScript = GetComponent<Health>();
    }

    public void StartBlock()
    {
        isBlocking = true;
        healthScript.damageRatio = 0.5f;
        playerAnimatorScript.ChangeBool("Blocking", true);    
    }
    public void StopBlock()
    {
        healthScript.damageRatio = 1f;
        isBlocking = false;
        playerAnimatorScript.ChangeBool("Blocking", false);
    }
}
