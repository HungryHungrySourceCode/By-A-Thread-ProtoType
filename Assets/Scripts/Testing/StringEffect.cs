using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringEffect : MonoBehaviour {
    public Transform Player;
    public float DirChangePower = 1f;
    public float DirChangeTime = 1f;
    public float perlinNoiseScale = 0.1f;
    public float speed = 5f;
    float followPlayerPower = 0.2f;

    Vector3 startScale;

    public Rigidbody thisRigidBody;

    public float stayAwayDist = 30f;

    float time = 1;
    float startingTime;

    public void Start()
    {
        startScale = transform.localScale;
        startingTime = time;
        Player = GameMood.instance.player;
    }

    bool add = true;
    public void Update()
    {
        Vector3 position = transform.position;

        transform.localScale = startScale * GameMood.instance.SmallOrLarge;

        if (add == true)
            time += Time.deltaTime * DirChangePower * GameMood.instance.RelaxedOrTense;
        else
            time -= Time.deltaTime * DirChangePower * GameMood.instance.RelaxedOrTense;

        if (time >= DirChangeTime / GameMood.instance.RelaxedOrTense && add == true)
            add = false;
        else if (time <= -DirChangeTime / GameMood.instance.RelaxedOrTense && add == false)
            add = true;


        //Vector4 sampleFront = GameMood.instance.SampleNoiseFront(position);
        //Vector4 sampleSide = GameMood.instance.SampleNoiseSide(position);
        //Vector4 sampleTop = GameMood.instance.SampleNoiseTop(position);
        //position2.x += (sample.x * 2f - 1f) * GameMood.instance.PerlinStrength;
        //position2.z += (sample.z * 2f - 1f) * GameMood.instance.PerlinStrength;

        //float RandomNum = Mathf.PerlinNoise(position.x * perlinNoiseScale * GameMood.instance.PerlinScale, position.z * perlinNoiseScale * GameMood.instance.PerlinScale) * 20f * GameMood.instance.HecticOrSmooth;
        //Vector3 test = new Vector3(Mathf.Cos(RandomNum) * time, Mathf.Sin(RandomNum) * time, Mathf.Tan(RandomNum) * time);

        Vector4 randomNum = GameMood.instance.SampleNoiseTop(position) * GameMood.instance.HecticOrSmooth;
        //Vector3 randomNum = new Vector3(sampleSide.x + sampleSide.y + sampleSide.z, sampleTop.x + sampleTop.y + sampleTop.z, sampleFront.x + sampleFront.y + sampleFront.z);
        Vector3 test = new Vector3(Mathf.Cos(randomNum.x) * time, Mathf.Sin(randomNum.y) * time, Mathf.Tan(randomNum.z) * time);
             

        Vector3 toPlayer = Player.position - transform.position;

        if (toPlayer.magnitude >= stayAwayDist / GameMood.instance.CloseOrFar)
            thisRigidBody.AddForce(toPlayer * followPlayerPower * speed * GameMood.instance.SlowOrFast);
        else
            thisRigidBody.AddForce(-toPlayer * 2f * speed * GameMood.instance.SlowOrFast);

        thisRigidBody.AddForce((test.normalized * GameMood.instance.PerlinStrength * 1000) * GameMood.instance.SlowOrFast);
        //Debug.Log(randomNum);

        //transform.LookAt(Player);
    }
}
