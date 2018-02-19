using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAnimator : MonoBehaviour {
    public Animator unitAnimator;
    public NavMeshAgent navmeshAgent;

    public void ChangeBool(string animation, bool value)
    {
        unitAnimator.SetBool(animation, value);
    }
}
