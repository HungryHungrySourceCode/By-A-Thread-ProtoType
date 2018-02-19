using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRandomForce : MonoBehaviour {
    public ConstantForce constantForce;
    public bool applyJitter = true;
    public bool applyMainForce = true;
    public float bigForceAmount = 2f;
    public float timerMin = 1f;
    public float timerMax = 3f;

    public float jitterTimeMin = 0.1f;
    public float jitterTimerMax = 1f;

    public float jitterAmount = 0.1f;

    public Vector3 currentRandomForce;
    public Vector3 currentJitterForce;

    public bool breakCoRoutines = false;

	// Use this for initialization
	void Start () {
        if (applyJitter == true)
            StartCoroutine(ApplyRandomForces());
        if (applyMainForce == true)
            StartCoroutine(ApplyJitter());

        if (applyJitter == false && applyMainForce == false)
        {
            Debug.Log("No Force Selected");
            breakCoRoutines = true;
        }
	}

    IEnumerator ApplyRandomForces()
    {
        while (true)
        {
            currentRandomForce = new Vector3(Random.Range(-bigForceAmount, bigForceAmount), Random.Range(-bigForceAmount, bigForceAmount), Random.Range(-bigForceAmount, bigForceAmount));
            constantForce.relativeForce = currentRandomForce;

            if (breakCoRoutines == true)
                yield break;
            yield return new WaitForSeconds(Random.Range(timerMin, timerMax));
        }
    }

    IEnumerator ApplyJitter()
    {
        while (true)
        {
            currentJitterForce = new Vector3(Random.Range(-jitterAmount, jitterAmount), Random.Range(-jitterAmount, jitterAmount), Random.Range(-jitterAmount, jitterAmount));
            constantForce.force = (currentJitterForce + currentJitterForce);
            if (breakCoRoutines == true)
                yield break;
            yield return new WaitForSeconds(Random.Range(jitterTimeMin, jitterTimerMax));
        }
    }
}
