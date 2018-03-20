using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMood : MonoBehaviour {
    public static GameMood instance;

    [Header("Colors")]
    public Color MainColor = Color.white;
    public Color ContestingColor = Color.black;
    public Color ThirdColor = Color.yellow;

    [Header("Mood")]
    public float DarkOrBright = 1f;
    public float CloseOrFar = 1f;
    public float RelaxedOrTense = 1f;
    public float SlowOrFast = 1f;
    public float HecticOrSmooth = 1f;
    public float SmallOrLarge = 1f;

    [Header("Gameplay")]
    public float globalDamageMult = 1f;
    public float globalHealingMult = 1f;
    public float globalMaxHealthMult = 1f;
    public float healthRegenPerTick = 1f;
    public float regenTickTime = 1f;

    [Header("ProjectileList")]
    public GameObject[] lightFastEnemyProjectile;
    public GameObject[] mediumEnemyProjectile;
    public GameObject[] heavySlowProjectile;

    [Header("Noise")]
    public Texture2D noiseSource1;

    public Texture2D noiseFront;
    public Texture2D noiseSide;
    public Texture2D noiseTop;

    public float PerlinStrength = 1f;
    public float noiseScale = 0.2f;
    public float bigNoiseScale = 0.01f;

    [Header("Initialization")]
    public Transform player;

    //for text dialogue to aim at essentially
    public Transform downwardCameraLookTarget;


    [Header("Time")]
    public float LongWaitTime = 10f;
    public float MidWaitTime = 5f;
    public float ShortWaitTime = 1f;
    public float VeryShortWaitTime = 0.2f;

    [Header("RandomValues")]
    public Vector2[] fastValuesRange;
    public Vector2[] midValuesRange;
    public Vector2[] largeValuesRange;

    public float[] fastValue;
    public float[] midValue;
    public float[] largeValue;

    public void Awake()
    {
        instance = transform.GetComponent<GameMood>();
        Debug.Log(instance);
    }

    private void Start()
    {
        StartCoroutine(UpdateRandomValues());
    }

    public float[] currentFastValue;
    public float[] currentMidValue;
    public float[] currentLargeValue;

    public IEnumerator UpdateRandomValues()
    {
        while (true)
        {
            for (int i = 0; i < fastValuesRange.Length; i++)
            {
                if (currentFastValue[i] < fastValue[i] - 0.1f)
                    currentFastValue[i] += Time.deltaTime / HecticOrSmooth;
                else if (currentFastValue[i] > fastValue[i] + 0.1f)
                    currentFastValue[i] -= Time.deltaTime / HecticOrSmooth;
                else
                    fastValue[i] = Random.Range(fastValuesRange[i].x, fastValuesRange[i].y);
            }
            for (int i = 0; i < midValuesRange.Length; i++)
            {
                if (currentMidValue[i] < midValue[i] - 0.1f)
                    currentMidValue[i] += Time.deltaTime / HecticOrSmooth * 0.5f;
                else if (currentMidValue[i] > midValue[i] + 0.1f)
                    currentMidValue[i] -= Time.deltaTime / HecticOrSmooth * 0.5f;
                else
                    midValue[i] = Random.Range(midValuesRange[i].x, midValuesRange[i].y);
            }
            for (int i = 0; i < largeValuesRange.Length; i++)
            {
                if (currentLargeValue[i] < largeValue[i] - 0.1f)
                    currentLargeValue[i] += Time.deltaTime / HecticOrSmooth * 0.25f;
                else if (currentLargeValue[i] > largeValue[i] + 0.1f)
                    currentLargeValue[i] -= Time.deltaTime / HecticOrSmooth * 0.25f;
                else
                    largeValue[i] = Random.Range(largeValuesRange[i].x, largeValuesRange[i].y);
            }
            yield return new WaitForFixedUpdate();
        }
    }







    //Noise

    public Vector4 SampleNoiseTop(Vector3 position)
    {
        return noiseTop.GetPixelBilinear(
            position.x * noiseScale,
            position.z * noiseScale
        );
    }

    public Vector4 SampleNoiseSide(Vector3 position)
    {
        return noiseSide.GetPixelBilinear(
            position.z * noiseScale,
            position.y * noiseScale
        );
    }

    public Vector4 SampleNoiseFront(Vector3 position)
    {
        return noiseFront.GetPixelBilinear(
            position.y * noiseScale,
            position.x * noiseScale
        );
    }

    public Vector3 ThreeDeeNoise(Vector3 pos)
    {
        return new Vector3(SampleNoiseSide(pos).x, SampleNoiseTop(pos).y, SampleNoiseFront(pos).z);
    }


}
