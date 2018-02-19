using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String2 : MonoBehaviour {

    public float minDistance = 10f;
    public float maxDistance = 50f;
    public float perlinScale = 1f;
    public float speed;
    public AnimationCurve Xcurve;
    public AnimationCurve Ycurve;
    public AnimationCurve ZCurve;

    public AnimationCurve time;
    public Rigidbody thisRigidBody;

    public GameMood gameMoodScript;
    float startDrag;

    public float randomnessGen;
	// Use this for initialization
	void Start () {
        startDrag = thisRigidBody.drag;

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;

        thisRigidBody.drag = startDrag * GameMood.instance.RelaxedOrTense;

        Vector3 dist = (GameMood.instance.player.position - transform.position);
        float distMag = dist.magnitude;
        if (distMag >= maxDistance * GameMood.instance.CloseOrFar)
            thisRigidBody.AddForce(dist * 0.5f * GameMood.instance.RelaxedOrTense * GameMood.instance.SlowOrFast);
        else if (distMag <= minDistance * GameMood.instance.CloseOrFar)
            thisRigidBody.AddForce(-dist * GameMood.instance.RelaxedOrTense * GameMood.instance.SlowOrFast);

        //Vector3 randomForce = new Vector3(GameMood.instance.WideRandomCurvingValue, GameMood.instance.WideRandomCurving2Value, GameMood.instance.WideRandomCurving3Value);
        Vector3 randomForce = GameMood.instance.SampleNoiseFront(position);
        thisRigidBody.AddForce(randomForce * speed);
	}

    float currentRandomValue = 1f;
    float randomValue = 2f;

 //   private void OnDrawGizmos()
 //   {
//
 //       if (currentRandomValue > randomValue + 0.4f)
 //           currentRandomValue -= Time.deltaTime * gameMoodScript.RelaxedOrTense;
 //       else if (currentRandomValue < randomValue - 0.4f)
 //           currentRandomValue += Time.deltaTime * gameMoodScript.RelaxedOrTense;
 //       else
 //       {
 //           randomValue = Random.Range(-2f, 2f);
 //       }

//
//        Vector3 position = transform.position;
        // Vector3 ChanedPos = new Vector3(Mathf)

 //       Vector3 pos = gameMoodScript.ThreeDeeNoise(position);
        //Vector3 pos = new Vector3(Mathf.PerlinNoise(transform.position.z * perlinScale, transform.position.y * perlinScale), Mathf.PerlinNoise(transform.position.x * perlinScale, transform.position.z * perlinScale), Mathf.PerlinNoise(transform.position.x * perlinScale, transform.position.y * perlinScale));   //GameMood.instance.SampleNoiseFront(transform.position);
//       Gizmos.DrawWireSphere((pos * gameMoodScript.SlowOrFast + transform.position), 1f);

 //   }
}
