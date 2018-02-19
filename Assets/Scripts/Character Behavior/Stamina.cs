using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour {

    [Header("Initialization")]
    public GameMood gameMood;
    public Transform mainTransform;

    [Header("Attributes")]
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    public float StaminaRegenPerTick = 1f;
    public float tickRate = 0.1f;

    [Header("RuntimeVariables")]
    public bool canGainStamina = true;

    public bool CheckIfExausted(float loss, float stamina)
    {
        if (stamina - loss <= 0)
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
        currentStamina = maxStamina;
        StartCoroutine(Regen());
    }

    public void TakeStaminaDamage(float amount)
    {
        if (CheckIfExausted(amount, currentStamina))
        {
            currentStamina -= amount;
            Exaust();
        }

            currentStamina -= amount;
    }

    public void GainStamina(float amount)
    {
        if (currentStamina + amount >= maxStamina && canGainStamina == true)
            currentStamina = maxStamina;
        else
        {
            currentStamina += amount;
        }
    }

    public void Exaust()
    {
    }

    public IEnumerator Regen()
    {
        while (true)
        {
            if (currentStamina <= maxStamina)
            {
                currentStamina += StaminaRegenPerTick;
            }
            yield return new WaitForSeconds(tickRate);
        }
    }
}
