using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour {
    [Header("Initialization")]
    public GameMood gameMood;
    public Transform mainTransform;

    [Header("Attributes")]
    public float maxSanity = 100f;
    public float currentSanity = 100f;
    public float baseSanityRegen = 0.25f;
    public float tickRate = 1f;

    [Header("RuntimeVariables")]
    public bool canGainSanity = true;

    public bool CheckIfInsane(float loss, float sanity)
    {
        if (sanity - loss <= 0)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        OnSpawn();
    }

    private void Start()
    {
        gameMood = GameMood.instance;
    }

    public void OnSpawn()
    {
        currentSanity = maxSanity;
        StartCoroutine(Regen());
    }

    public void TakeSanityDamage(float amount)
    {
        if (CheckIfInsane(amount, currentSanity))
        {
            currentSanity -= amount;
            GoInsane();
        }

        currentSanity -= amount;
    }

    public void GainSanity(float amount)
    {
        if (currentSanity + amount >= maxSanity && canGainSanity == true)
            currentSanity = maxSanity;
        else
        {
            currentSanity += amount;
        }
    }

    public void GoInsane()
    {
    }

    public IEnumerator Regen()
    {
        while (true)
        {
            if (currentSanity <= maxSanity)
            {
                currentSanity += baseSanityRegen;
            }
            yield return new WaitForSeconds(tickRate);
        }
    }
}
